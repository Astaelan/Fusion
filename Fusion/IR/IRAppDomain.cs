﻿using Fusion.CLI;
using Fusion.CLI.Metadata;
using Fusion.CLI.Signature;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Fusion.IR
{
    public sealed class IRAppDomain
    {
        public List<IRAssembly> Assemblies = new List<IRAssembly>();
        public Dictionary<CLIFile, IRAssembly> AssemblyFileLookup = new Dictionary<CLIFile, IRAssembly>();
        public Dictionary<string, IRAssembly> AssemblyFileReferenceNameLookup = new Dictionary<string, IRAssembly>();

        public IRType System_Array = null;
        public IRType System_Boolean = null;
        public IRType System_Byte = null;
        public IRType System_Char = null;
        public IRType System_Double = null;
        public IRType System_Enum = null;
        public IRType System_Exception = null;
        public IRType System_Int16 = null;
        public IRType System_Int32 = null;
        public IRType System_Int64 = null;
        public IRType System_IntPtr = null;
        public IRType System_Object = null;
        public IRType System_RuntimeFieldHandle = null;
        public IRType System_RuntimeMethodHandle = null;
        public IRType System_RuntimeTypeHandle = null;
        public IRType System_SByte = null;
        public IRType System_Single = null;
        public IRType System_String = null;
        public IRType System_Type = null;
        public IRType System_TypedReference = null;
        public IRType System_UInt16 = null;
        public IRType System_UInt32 = null;
        public IRType System_UInt64 = null;
        public IRType System_UIntPtr = null;
        public IRType System_ValueType = null;
        public IRType System_Void = null;

        // Dynamic Types
        public Dictionary<IRType, IRType> PointerTypes = new Dictionary<IRType, IRType>();
        public Dictionary<IRType, IRType> ArrayTypes = new Dictionary<IRType, IRType>();

        public IRAppDomain()
        {
            AddAssembly(new IRAssembly(this, new CLIFile("mscorlib", File.ReadAllBytes("mscorlib.dll")), true));
        }

        private void AddAssembly(IRAssembly pAssembly)
        {
            Assemblies.Add(pAssembly);
            AssemblyFileLookup.Add(pAssembly.File, pAssembly);
            AssemblyFileReferenceNameLookup.Add(pAssembly.File.ReferenceName, pAssembly);
        }

        public IRAssembly CreateAssembly(CLIFile pFile)
        {
            IRAssembly assembly = null;
            if (AssemblyFileReferenceNameLookup.TryGetValue(pFile.ReferenceName, out assembly)) return assembly;
            assembly = new IRAssembly(this, pFile, false);
            AddAssembly(assembly);
            foreach (AssemblyRefData assemblyRefData in pFile.AssemblyRefTable) CreateAssembly(new CLIFile(assemblyRefData.Name, File.ReadAllBytes(assemblyRefData.Name + ".dll")));
            return assembly;
        }

        internal void CacheCORTypes(IRAssembly pAssembly)
        {
            foreach (IRType type in pAssembly.Types)
            {
                if (type.Namespace == "System")
                {
                    switch (type.Name)
                    {
                        case "Array": System_Array = type; break;
                        case "Boolean": System_Boolean = type; break;
                        case "Byte": System_Byte = type; break;
                        case "Char": System_Char = type; break;
                        case "Double": System_Double = type; break;
                        case "Enum": System_Enum = type; break;
                        case "Exception": System_Exception = type; break;
                        case "Int16": System_Int16 = type; break;
                        case "Int32": System_Int32 = type; break;
                        case "Int64": System_Int64 = type; break;
                        case "IntPtr": System_IntPtr = type; break;
                        case "Object": System_Object = type; break;
                        case "RuntimeFieldHandle": System_RuntimeFieldHandle = type; break;
                        case "RuntimeMethodHandle": System_RuntimeMethodHandle = type; break;
                        case "RuntimeTypeHandle": System_RuntimeTypeHandle = type; break;
                        case "SByte": System_SByte = type; break;
                        case "Single": System_Single = type; break;
                        case "String": System_String = type; break;
                        case "Type": System_Type = type; break;
                        case "TypedReference": System_TypedReference = type; break;
                        case "UInt16": System_UInt16 = type; break;
                        case "UInt32": System_UInt32 = type; break;
                        case "UInt64": System_UInt64 = type; break;
                        case "UIntPtr": System_UIntPtr = type; break;
                        case "ValueType": System_ValueType = type; break;
                        case "Void": System_Void = type; break;
                        default: break;
                    }
                }
            }
        }

        public IRAssembly LoadEntryAssembly(CLIFile pFile)
        {
            IRAssembly assembly = CreateAssembly(pFile);
            Assemblies.ForEach(a => a.LoadStage1());
            Assemblies.ForEach(a => a.LoadStage2());
            Assemblies.ForEach(a => a.LoadStage3());
            Assemblies.ForEach(a => a.LoadStage4());
            return assembly;
        }

        public IRType CreatePointerType(IRType pPointerType)
        {
            IRType type = null;
            if (!PointerTypes.TryGetValue(pPointerType, out type))
            {
                type = new IRType(System_IntPtr);
                type.PointerType = pPointerType;
                PointerTypes.Add(pPointerType, type);
            }
            return type;
        }

        public IRType CreateArrayType(IRType pArrayType)
        {
            IRType type = null;
            if (!ArrayTypes.TryGetValue(pArrayType, out type))
            {
                type = new IRType(System_Array);
                type.ArrayType = pArrayType;
                ArrayTypes.Add(pArrayType, type);
            }
            return type;
        }

        public string CreateGenericTypeHash(IRType pGenericType, ref bool pGenericParameterTypesResolved, List<IRType> pGenericParameterTypes)
        {
            StringBuilder hashInput = new StringBuilder();
            hashInput.AppendFormat("{0}.{1}<", pGenericType.Namespace, pGenericType.Name);
            bool firstParam = true;
            foreach (IRType paramType in pGenericParameterTypes)
            {
                if (!firstParam) hashInput.Append(", ");
                if (paramType.PointerType != null) hashInput.AppendFormat("{0}.{1}*", paramType.PointerType.Namespace, paramType.PointerType.Name);
                else if (paramType.ArrayType != null) hashInput.AppendFormat("{0}.{1}[]", paramType.ArrayType.Namespace, paramType.ArrayType.Name);
                else if (paramType.IsTemporaryVar)
                {
                    pGenericParameterTypesResolved = false;
                    hashInput.AppendFormat("VAR({0})", paramType.TemporaryVarOrMVarIndex);
                }
                else if (paramType.IsTemporaryMVar)
                {
                    pGenericParameterTypesResolved = false;
                    hashInput.AppendFormat("MVAR({0})", paramType.TemporaryVarOrMVarIndex);
                }
                else if (paramType.IsGeneric)
                {
                    bool genericParameterTypesResolved = true;
                    hashInput.Append(CreateGenericTypeHash(paramType, ref genericParameterTypesResolved, paramType.GenericParameters));
                    if (!genericParameterTypesResolved) pGenericParameterTypesResolved = false;
                }
                else hashInput.AppendFormat("{0}.{1}", paramType.Namespace, paramType.Name);
                firstParam = false;
            }
            hashInput.Append(">");
            return hashInput.ToString();
        }

        public string CreateGenericMethodHash(IRMethod pGenericMethod, ref bool pGenericParameterTypesResolved, List<IRType> pGenericParameterTypes)
        {
            StringBuilder hashInput = new StringBuilder();
            hashInput.AppendFormat("{0}<", pGenericMethod.Name);
            bool firstParam = true;
            foreach (IRType paramType in pGenericParameterTypes)
            {
                if (!firstParam) hashInput.Append(", ");
                if (paramType.PointerType != null) hashInput.AppendFormat("{0}.{1}*", paramType.PointerType.Namespace, paramType.PointerType.Name);
                else if (paramType.ArrayType != null) hashInput.AppendFormat("{0}.{1}[]", paramType.ArrayType.Namespace, paramType.ArrayType.Name);
                else if (paramType.IsTemporaryVar)
                {
                    pGenericParameterTypesResolved = false;
                    hashInput.AppendFormat("VAR({0})", paramType.TemporaryVarOrMVarIndex);
                }
                else if (paramType.IsTemporaryMVar)
                {
                    pGenericParameterTypesResolved = false;
                    hashInput.AppendFormat("MVAR({0})", paramType.TemporaryVarOrMVarIndex);
                }
                else if (paramType.IsGeneric)
                {
                    bool genericParameterTypesResolved = true;
                    hashInput.Append(CreateGenericTypeHash(paramType, ref genericParameterTypesResolved, paramType.GenericParameters));
                    if (!genericParameterTypesResolved) pGenericParameterTypesResolved = false;
                }
                else hashInput.AppendFormat("{0}.{1}", paramType.Namespace, paramType.Name);
                firstParam = false;
            }
            hashInput.Append(">");
            return hashInput.ToString();
        }

        public IRType CreateGenericType(IRType pGenericType, List<IRType> pGenericParameterTypes)
        {
            bool genericParameterTypesResolved = true;
            string genericTypeHash = CreateGenericTypeHash(pGenericType, ref genericParameterTypesResolved, pGenericParameterTypes);
            IRType type = new IRType(pGenericType);
            type.IsGeneric = true;
            type.GenericType = pGenericType;
            type.GenericParameters.AddRange(pGenericParameterTypes);
            //type.GenericParametersResolved = genericParameterTypesResolved;
            Console.WriteLine("Generic Type: {0}", genericTypeHash);
            return type;
        }

        public IRMethod CreateGenericMethod(IRMethod pGenericMethod, List<IRType> pGenericParameterTypes)
        {
            bool genericParameterTypesResolved = true;
            string genericMethodHash = CreateGenericMethodHash(pGenericMethod, ref genericParameterTypesResolved, pGenericParameterTypes);
            IRMethod method = new IRMethod(pGenericMethod);
            method.IsGeneric = true;
            method.GenericMethod = pGenericMethod;
            method.GenericParameters.AddRange(pGenericParameterTypes);
            //method.GenericParametersResolved = genericParameterTypesResolved;
            Console.WriteLine("Generic Method: {0}", genericMethodHash);
            return method;
        }

        public bool CompareSignatures(SigType pSigTypeA, SigType pSigTypeB)
        {
            IRType typeA = PresolveType(pSigTypeA);
            IRType typeB = PresolveType(pSigTypeB);
            if (typeA.ArrayType != null)
            {
                if (typeB.ArrayType == null) return false;
                return typeA.ArrayType == typeB.ArrayType;
            }
            if (typeA.PointerType != null)
            {
                if (typeB.PointerType == null) return false;
                return typeA.PointerType == typeB.PointerType;
            }
            if (typeA.IsTemporaryVar || typeB.IsTemporaryVar)
            {
                if (!typeA.IsTemporaryVar || !typeB.IsTemporaryVar) return false;
                return typeA.TemporaryVarOrMVarIndex == typeB.TemporaryVarOrMVarIndex;
            }
            if (typeA.IsTemporaryMVar || typeB.IsTemporaryMVar)
            {
                if (!typeA.IsTemporaryMVar || !typeB.IsTemporaryMVar) return false;
                return typeA.TemporaryVarOrMVarIndex == typeB.TemporaryVarOrMVarIndex;
            }
            return typeA == typeB;
        }

        public bool CompareSignatures(MethodSig pMethodSigA, MethodSig pMethodSigB)
        {
            if (pMethodSigA.HasThis != pMethodSigB.HasThis) return false;
            if (pMethodSigA.ExplicitThis != pMethodSigB.ExplicitThis) return false;
            if (pMethodSigA.Default != pMethodSigB.Default) return false;
            if (pMethodSigA.VarArg != pMethodSigB.VarArg) return false;
            if (pMethodSigA.CCall != pMethodSigB.CCall) return false;
            if (pMethodSigA.STDCall != pMethodSigB.STDCall) return false;
            if (pMethodSigA.ThisCall != pMethodSigB.ThisCall) return false;
            if (pMethodSigA.FastCall != pMethodSigB.FastCall) return false;
            if (pMethodSigA.GenParamCount != pMethodSigB.GenParamCount) return false;
            if (pMethodSigA.Params.Count != pMethodSigB.Params.Count) return false;
            if (pMethodSigA.HasSentinel != pMethodSigB.HasSentinel) return false;
            if (pMethodSigA.SentinelIndex != pMethodSigB.SentinelIndex) return false;
            if (pMethodSigA.RetType.Void != pMethodSigB.RetType.Void) return false;
            if (!pMethodSigA.RetType.Void)
            {
                if (pMethodSigA.RetType.ByRef != pMethodSigB.RetType.ByRef) return false;
                if (pMethodSigA.RetType.TypedByRef != pMethodSigB.RetType.TypedByRef) return false;
                if (!pMethodSigA.RetType.TypedByRef && !CompareSignatures(pMethodSigA.RetType.Type, pMethodSigB.RetType.Type)) return false;
            }
            for (int index = 0; index < pMethodSigA.Params.Count; ++index)
            {
                if (pMethodSigA.Params[index].ByRef != pMethodSigB.Params[index].ByRef) return false;
                if (pMethodSigA.Params[index].TypedByRef != pMethodSigB.Params[index].TypedByRef) return false;
                if (!pMethodSigA.Params[index].TypedByRef && !CompareSignatures(pMethodSigA.Params[index].Type, pMethodSigB.Params[index].Type)) return false;
            }
            return true;
        }

        public IRType PresolveType(TypeDefData pTypeDefData)
        {
            if (pTypeDefData == null) throw new ArgumentNullException("pTypeDefData");
            return AssemblyFileLookup[pTypeDefData.CLIFile].Types[pTypeDefData.TableIndex];
        }

        public IRType PresolveType(TypeRefData pTypeRefData)
        {
            if (pTypeRefData.ExportedType != null)
            {
                switch (pTypeRefData.ExportedType.Implementation.Type)
                {
                    case ImplementationIndex.ImplementationType.File:
                        {
                            IRAssembly assembly = null;
                            if (!AssemblyFileReferenceNameLookup.TryGetValue(pTypeRefData.ExportedType.Implementation.File.Name, out assembly)) throw new KeyNotFoundException();
                            IRType type = assembly.Types.Find(t => t.Namespace == pTypeRefData.TypeNamespace && t.Name == pTypeRefData.TypeName);
                            if (type == null) throw new NullReferenceException();
                            return type;
                        }
                    case ImplementationIndex.ImplementationType.AssemblyRef:
                        {
                            IRAssembly assembly = null;
                            if (!AssemblyFileReferenceNameLookup.TryGetValue(pTypeRefData.ExportedType.Implementation.AssemblyRef.Name, out assembly)) throw new KeyNotFoundException();
                            IRType type = assembly.Types.Find(t => t.Namespace == pTypeRefData.TypeNamespace && t.Name == pTypeRefData.TypeName);
                            if (type == null) throw new NullReferenceException();
                            return type;
                        }
                    default: break;
                }
                throw new NullReferenceException();
            }
            switch (pTypeRefData.ResolutionScope.Type)
            {
                case ResolutionScopeIndex.ResolutionScopeType.AssemblyRef:
                    {
                        IRAssembly assembly = null;
                        if (!AssemblyFileReferenceNameLookup.TryGetValue(pTypeRefData.ResolutionScope.AssemblyRef.Name, out assembly)) throw new KeyNotFoundException();
                        IRType type = assembly.Types.Find(t => t.Namespace == pTypeRefData.TypeNamespace && t.Name == pTypeRefData.TypeName);
                        if (type == null) throw new NullReferenceException();
                        return type;
                    }
                case ResolutionScopeIndex.ResolutionScopeType.TypeRef:
                    {
                        IRType type = PresolveType(pTypeRefData.ResolutionScope.TypeRef);
                        if (type == null) throw new NullReferenceException();
                        type = type.NestedTypes.Find(t => t.Namespace == pTypeRefData.TypeNamespace && t.Name == pTypeRefData.TypeName);
                        if (type == null) throw new NullReferenceException();
                        return type;
                    }
            }
            throw new NullReferenceException();
        }

        public IRType PresolveType(TypeSpecData pTypeSpecData) { return PresolveType(pTypeSpecData.ExpandedSignature); }

        public IRType PresolveType(TypeDefRefOrSpecIndex pTypeDefRefOrSpecIndex)
        {
            switch (pTypeDefRefOrSpecIndex.Type)
            {
                case TypeDefRefOrSpecIndex.TypeDefRefOrSpecType.TypeDef: return PresolveType(pTypeDefRefOrSpecIndex.TypeDef);
                case TypeDefRefOrSpecIndex.TypeDefRefOrSpecType.TypeRef: return PresolveType(pTypeDefRefOrSpecIndex.TypeRef);
                case TypeDefRefOrSpecIndex.TypeDefRefOrSpecType.TypeSpec: return PresolveType(pTypeDefRefOrSpecIndex.TypeSpec.ExpandedSignature);
                default: break;
            }
            throw new NullReferenceException();
        }

        public IRType PresolveType(MetadataToken pMetadataToken)
        {
            switch (pMetadataToken.Table)
            {
                case CLIMetadataTables.TypeDef: return PresolveType((TypeDefData)pMetadataToken.Data);
                case CLIMetadataTables.TypeRef: return PresolveType((TypeRefData)pMetadataToken.Data);
                case CLIMetadataTables.TypeSpec: return PresolveType(((TypeSpecData)pMetadataToken.Data).ExpandedSignature);
                default: break;
            }
            throw new NullReferenceException();
        }

        public IRType PresolveType(SigType pSigType)
        {
            IRType type = null;
            switch (pSigType.ElementType)
            {
                case SigElementType.Void: type = System_Void; break;
                case SigElementType.Boolean: type = System_Boolean; break;
                case SigElementType.Char: type = System_Char; break;
                case SigElementType.I1: type = System_SByte; break;
                case SigElementType.U1: type = System_Byte; break;
                case SigElementType.I2: type = System_Int16; break;
                case SigElementType.U2: type = System_UInt16; break;
                case SigElementType.I4: type = System_Int32; break;
                case SigElementType.U4: type = System_UInt32; break;
                case SigElementType.I8: type = System_Int64; break;
                case SigElementType.U8: type = System_UInt64; break;
                case SigElementType.R4: type = System_Single; break;
                case SigElementType.R8: type = System_Double; break;
                case SigElementType.String: type = System_String; break;
                case SigElementType.Pointer:
                    {
                        if (pSigType.PtrVoid) type = CreatePointerType(System_Void);
                        else type = CreatePointerType(PresolveType(pSigType.PtrType));
                        break;
                    }
                case SigElementType.ValueType: type = PresolveType(pSigType.CLIFile.ExpandTypeDefRefOrSpecToken(pSigType.ValueTypeDefOrRefOrSpecToken)); break;
                case SigElementType.Class: type = PresolveType(pSigType.CLIFile.ExpandTypeDefRefOrSpecToken(pSigType.ClassTypeDefOrRefOrSpecToken)); break;
                case SigElementType.Var: type = IRType.GetVarPlaceholder(pSigType.VarNumber); break;
                case SigElementType.Array: type = CreateArrayType(PresolveType(pSigType.ArrayType)); break;
                case SigElementType.GenericInstantiation:
                    {
                        IRType genericType = PresolveType(pSigType.CLIFile.ExpandTypeDefRefOrSpecToken(pSigType.GenericInstTypeDefOrRefOrSpecToken));
                        List<IRType> genericTypeParameters = new List<IRType>();
                        foreach (SigType paramType in pSigType.GenericInstGenArgs) genericTypeParameters.Add(PresolveType(paramType));
                        type = CreateGenericType(genericType, genericTypeParameters);
                        break;
                    }
                case SigElementType.IPointer: type = System_IntPtr; break;
                case SigElementType.UPointer: type = System_UIntPtr; break;
                case SigElementType.Object: type = System_Object; break;
                case SigElementType.SingleDimensionArray: type = CreateArrayType(PresolveType(pSigType.SZArrayType)); break;
                case SigElementType.MethodVar: type = IRType.GetMVarPlaceholder(pSigType.MVarNumber); break;
                case SigElementType.Type: type = System_Type; break;
                default: break;
            }
            if (type == null) throw new NullReferenceException();
            return type;
        }

        public IRType PresolveType(FieldSig pFieldSig) { return PresolveType(pFieldSig.Type); }

        public IRType PresolveType(SigRetType pSigRetType) { return pSigRetType.Void ? null : PresolveType(pSigRetType.Type); }

        public IRType PresolveType(SigParam pSigParam) { return PresolveType(pSigParam.Type); }

        public IRType PresolveType(SigLocalVar pSigLocalVar) { return PresolveType(pSigLocalVar.Type); }

        public IRMethod PresolveMethod(MethodDefData pMethodDefData)
        {
            if (pMethodDefData == null) throw new ArgumentNullException("pMethodDefData");
            return AssemblyFileLookup[pMethodDefData.CLIFile].Methods[pMethodDefData.TableIndex];
        }

        public IRMethod PresolveMethod(MemberRefData pMemberRefData)
        {
            if (!pMemberRefData.IsMethodRef) throw new ArgumentException();
            switch (pMemberRefData.Class.Type)
            {
                case MemberRefParentIndex.MemberRefParentType.TypeDef:
                    {
                        IRType type = PresolveType(pMemberRefData.Class.TypeDef);
                        foreach (IRMethod method in type.Methods)
                        {
                            if (method.CompareSignature(pMemberRefData)) return method;
                        }
                        throw new NullReferenceException();
                    }
                case MemberRefParentIndex.MemberRefParentType.TypeRef:
                    {
                        IRType type = PresolveType(pMemberRefData.Class.TypeRef);
                        foreach (IRMethod method in type.Methods)
                        {
                            if (method.CompareSignature(pMemberRefData)) return method;
                        }
                        throw new NullReferenceException();
                    }
                case MemberRefParentIndex.MemberRefParentType.TypeSpec:
                    {
                        IRType type = PresolveType(pMemberRefData.Class.TypeSpec);
                        foreach (IRMethod method in type.GenericType.Methods)
                        {
                            if (method.CompareSignature(pMemberRefData)) return method;
                        }
                        throw new NullReferenceException();
                    }
            }
            throw new NullReferenceException();
        }

        public IRMethod PresolveMethod(MethodSpecData pMethodSpecData)
        {
            IRMethod genericMethod = PresolveMethod(pMethodSpecData.Method);
            List<IRType> genericMethodParameters = new List<IRType>();
            foreach (SigType paramType in pMethodSpecData.ExpandedInstantiation.GenArgs) genericMethodParameters.Add(PresolveType(paramType));
            return CreateGenericMethod(genericMethod, genericMethodParameters);
        }

        public IRMethod PresolveMethod(MethodDefOrRefIndex pMethodDefOrRefIndex)
        {
            switch (pMethodDefOrRefIndex.Type)
            {
                case MethodDefOrRefIndex.MethodDefOrRefType.MethodDef: return PresolveMethod(pMethodDefOrRefIndex.MethodDef);
                case MethodDefOrRefIndex.MethodDefOrRefType.MemberRef: return PresolveMethod(pMethodDefOrRefIndex.MemberRef);
            }
            throw new NullReferenceException();
        }

        public IRMethod PresolveMethod(MetadataToken pMetadataToken)
        {
            switch (pMetadataToken.Table)
            {
                case CLIMetadataTables.MethodDef: return PresolveMethod((MethodDefData)pMetadataToken.Data);
                case CLIMetadataTables.MemberRef: return PresolveMethod((MemberRefData)pMetadataToken.Data);
                case CLIMetadataTables.MethodSpec: return PresolveMethod((MethodSpecData)pMetadataToken.Data);
                default: break;
            }
            throw new NullReferenceException();
        }

        public IRField PresolveField(FieldData pFieldData)
        {
            if (pFieldData == null) throw new ArgumentNullException("pFieldData");
            return AssemblyFileLookup[pFieldData.CLIFile].Fields[pFieldData.TableIndex];
        }

        public IRField PresolveField(MemberRefData pMemberRefData)
        {
            if (!pMemberRefData.IsFieldRef) throw new ArgumentException();
            switch (pMemberRefData.Class.Type)
            {
                case MemberRefParentIndex.MemberRefParentType.TypeDef:
                    {
                        IRType type = PresolveType(pMemberRefData.Class.TypeDef);
                        foreach (IRField field in type.Fields)
                        {
                            if (field.CompareSignature(pMemberRefData)) return field;
                        }
                        throw new NullReferenceException();
                    }
                case MemberRefParentIndex.MemberRefParentType.TypeRef:
                    {
                        IRType type = PresolveType(pMemberRefData.Class.TypeRef);
                        foreach (IRField field in type.Fields)
                        {
                            if (field.CompareSignature(pMemberRefData)) return field;
                        }
                        throw new NullReferenceException();
                    }
            }
            throw new NullReferenceException();
        }

        public IRField PresolveField(MetadataToken pMetadataToken)
        {
            switch (pMetadataToken.Table)
            {
                case CLIMetadataTables.Field: return PresolveField((FieldData)pMetadataToken.Data);
                case CLIMetadataTables.MemberRef: return PresolveField((MemberRefData)pMetadataToken.Data);
                default: break;
            }
            throw new NullReferenceException();
        }


        public IRType BinaryNumericResult(IRType pValue1Type, IRType pValue2Type)
        {
            IRType resultType = null;
            if (pValue1Type == System_SByte ||
                pValue1Type == System_Byte ||
                pValue1Type == System_Int16 ||
                pValue1Type == System_UInt16 ||
                pValue1Type == System_Int32 ||
                pValue1Type == System_UInt32)
            {
                if (pValue2Type == System_SByte ||
                    pValue2Type == System_Byte ||
                    pValue2Type == System_Int16 ||
                    pValue2Type == System_UInt16 ||
                    pValue2Type == System_Int32 ||
                    pValue2Type == System_UInt32)
                {
                    resultType = System_Int32;
                }
                else if (pValue2Type == System_IntPtr ||
                         pValue2Type == System_UIntPtr)
                {
                    resultType = System_IntPtr;
                }
                else throw new ArgumentException();
            }
            else if (pValue1Type == System_Int64 ||
                     pValue1Type == System_UInt64)
            {
                if (pValue2Type == System_Int64 ||
                    pValue2Type == System_UInt64)
                {
                    resultType = System_Int64;
                }
                else throw new ArgumentException();
            }
            else if (pValue1Type == System_IntPtr ||
                     pValue1Type == System_UIntPtr)
            {
                if (pValue2Type == System_SByte ||
                    pValue2Type == System_Byte ||
                    pValue2Type == System_Int16 ||
                    pValue2Type == System_UInt16 ||
                    pValue2Type == System_Int32 ||
                    pValue2Type == System_UInt32 ||
                    pValue2Type == System_IntPtr ||
                    pValue2Type == System_UIntPtr)
                {
                    resultType = System_IntPtr;
                }
                else throw new ArgumentException();
            }
            else if (pValue1Type == System_Single ||
                     pValue1Type == System_Double)
            {
                if (pValue2Type == System_Single ||
                    pValue2Type == System_Double)
                {
                    resultType = System_Double;
                }
                else throw new ArgumentException();
            }
            else throw new ArgumentException();
            return resultType;
        }
    }
}
