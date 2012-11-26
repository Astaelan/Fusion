using Fusion.CLI;
using Fusion.CLI.Metadata;
using Fusion.CLI.Signature;
using System;
using System.Collections.Generic;
using System.IO;

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
                if (type.TypeDefData.TypeNamespace == "System")
                {
                    switch (type.TypeDefData.TypeName)
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
            return assembly;
        }

        public IRType CreatePointerType(IRType pPointerType)
        {
            IRType type = null;
            if (PointerTypes.TryGetValue(pPointerType, out type)) return type;
            type = System_IntPtr.Clone();
            type.PointerType = pPointerType;
            PointerTypes.Add(pPointerType, type);
            return type;
        }

        public IRType CreateArrayType(IRType pArrayType)
        {
            IRType type = null;
            if (ArrayTypes.TryGetValue(pArrayType, out type)) return type;
            type = System_Array.Clone();
            type.ArrayType = pArrayType;
            ArrayTypes.Add(pArrayType, type);
            return type;
        }

        public IRType Resolve(TypeDefData pTypeDefData)
        {
            if (pTypeDefData == null) return null;
            return AssemblyFileLookup[pTypeDefData.CLIFile].Types[pTypeDefData.TableIndex];
        }

        public IRType Resolve(TypeRefData pTypeRefData)
        {
            if (pTypeRefData.ExportedType != null)
            {
                switch (pTypeRefData.ExportedType.Implementation.Type)
                {
                    case ImplementationIndex.ImplementationType.File:
                        {
                            IRAssembly assembly = null;
                            if (AssemblyFileReferenceNameLookup.TryGetValue(pTypeRefData.ExportedType.Implementation.File.Name, out assembly))
                                return Array.Find(assembly.Types, t => t.TypeDefData.TypeNamespace == pTypeRefData.TypeNamespace && t.TypeDefData.TypeName == pTypeRefData.TypeName);
                            break;
                        }
                    case ImplementationIndex.ImplementationType.AssemblyRef:
                        {
                            IRAssembly assembly = null;
                            if (AssemblyFileReferenceNameLookup.TryGetValue(pTypeRefData.ExportedType.Implementation.AssemblyRef.Name, out assembly))
                                return Array.Find(assembly.Types, t => t.TypeDefData.TypeNamespace == pTypeRefData.TypeNamespace && t.TypeDefData.TypeName == pTypeRefData.TypeName);
                            break;
                        }
                    default: break;
                }
                return null;
            }
            switch (pTypeRefData.ResolutionScope.Type)
            {
                case ResolutionScopeIndex.ResolutionScopeType.AssemblyRef:
                    {
                        IRAssembly assembly = null;
                        if (AssemblyFileReferenceNameLookup.TryGetValue(pTypeRefData.ResolutionScope.AssemblyRef.Name, out assembly))
                            return Array.Find(assembly.Types, t => t.TypeDefData.TypeNamespace == pTypeRefData.TypeNamespace && t.TypeDefData.TypeName == pTypeRefData.TypeName);
                        break;
                    }
                case ResolutionScopeIndex.ResolutionScopeType.TypeRef:
                    {
                        IRType type = Resolve(pTypeRefData.ResolutionScope.TypeRef);
                        if (type != null)
                            return type.NestedTypes.Find(t => t.TypeDefData.TypeNamespace == pTypeRefData.TypeNamespace && t.TypeDefData.TypeName == pTypeRefData.TypeName);
                        break;
                    }
            }
            return null;
        }

        public IRType Resolve(TypeDefRefOrSpecIndex pTypeDefRefOrSpecIndex)
        {
            switch (pTypeDefRefOrSpecIndex.Type)
            {
                case TypeDefRefOrSpecIndex.TypeDefRefOrSpecType.TypeDef: return Resolve(pTypeDefRefOrSpecIndex.TypeDef);
                case TypeDefRefOrSpecIndex.TypeDefRefOrSpecType.TypeRef: return Resolve(pTypeDefRefOrSpecIndex.TypeRef);
                default: break;
            }
            return null;
        }

        public IRType Resolve(MetadataToken pMetadataToken)
        {
            switch (pMetadataToken.Table)
            {
                case CLIMetadataTables.TypeDef: return Resolve((TypeDefData)pMetadataToken.Data);
                case CLIMetadataTables.TypeRef: return Resolve((TypeRefData)pMetadataToken.Data);
                default: break;
            }
            return null;
        }

        public IRType Resolve(SigType pSigType)
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
                        else type = CreatePointerType(Resolve(pSigType.PtrType));
                        break;
                    }
                case SigElementType.ValueType: type = Resolve(pSigType.CLIFile.ExpandTypeDefRefOrSpecToken(pSigType.ValueTypeDefOrRefOrSpecToken)); break;
                case SigElementType.Class: type = Resolve(pSigType.CLIFile.ExpandTypeDefRefOrSpecToken(pSigType.ClassTypeDefOrRefOrSpecToken)); break;
                case SigElementType.Var:
                    type = IRType.TemporaryVarType.Clone();
                    type.TemporaryVarOrMVarIndex = pSigType.VarNumber;
                    break;
                case SigElementType.Array: type = CreateArrayType(Resolve(pSigType.ArrayType)); break;
                case SigElementType.GenericInstantiation: type = Resolve(pSigType.CLIFile.ExpandTypeDefRefOrSpecToken(pSigType.GenericInstTypeDefOrRefOrSpecToken)); break;
                case SigElementType.IPointer: type = System_IntPtr; break;
                case SigElementType.UPointer: type = System_UIntPtr; break;
                case SigElementType.Object: type = System_Object; break;
                case SigElementType.SingleDimensionArray: type = CreateArrayType(Resolve(pSigType.SZArrayType)); break;
                case SigElementType.MethodVar:
                    type = IRType.TemporaryMVarType.Clone();
                    type.TemporaryVarOrMVarIndex = pSigType.MVarNumber;
                    break;
                case SigElementType.Type: type = System_Type; break;
                default: break;
            }
            return type;
        }

        public IRType Resolve(SigRetType pSigRetType) { return pSigRetType.Void ? null : Resolve(pSigRetType.Type); }

        public IRType Resolve(SigParam pSigParam) { return Resolve(pSigParam.Type); }

        public IRType Resolve(SigLocalVar pSigLocalVar) { return Resolve(pSigLocalVar.Type); }
    }
}
