using Fusion.CLI;
using Fusion.CLI.Metadata;
using Fusion.CLI.Signature;
using System;
using System.Collections.Generic;

namespace Fusion.IR
{
    public sealed class IRAssembly
    {
        public IRAppDomain AppDomain;
        public CLIFile File;
        public bool CORLibrary;
        public IRType[] Types;
        public IRMethod[] Methods;

        internal IRAssembly(IRAppDomain pAppDomain, CLIFile pCLIFile, bool pCORLibrary)
        {
            AppDomain = pAppDomain;
            File = pCLIFile;
            CORLibrary = pCORLibrary;
            Types = new IRType[File.TypeDefTable.Length];
            Methods = new IRMethod[File.MethodDefTable.Length];
        }

        internal void LoadStage1()
        {
            for (int index = 0; index < Types.Length; ++index) Types[index] = new IRType(this, File.TypeDefTable[index]);
            for (int index = 0; index < Methods.Length; ++index) Methods[index] = new IRMethod(this, File.MethodDefTable[index], Types[File.MethodDefTable[index].ParentTypeDef.TableIndex]);
            foreach (IRType type in Types)
            {
                foreach (FieldData fieldData in type.TypeDefData.FieldList) type.Fields.Add(new IRField(this, File.FieldTable[fieldData.TableIndex], type));
                foreach (TypeDefRefOrSpecIndex typeDefRefOrSpecIndex in type.TypeDefData.InterfaceList) type.InterfaceImplementations.Add(new IRInterfaceImplementation(this, typeDefRefOrSpecIndex, type));
                foreach (MethodDefData methodDefData in type.TypeDefData.MethodList)
                {
                    IRMethod method = Methods[methodDefData.TableIndex];
                    type.Methods.Add(method);
                    foreach (ParamData paramData in methodDefData.ParamList) method.Parameters.Add(new IRParameter(this, paramData, method));
                    if (methodDefData.Body != null && methodDefData.Body.ExpandedLocalVarSignature != null)
                    {
                        for (uint localIndex = 0; localIndex < methodDefData.Body.ExpandedLocalVarSignature.LocalVars.Count; ++localIndex) method.Locals.Add(new IRLocal(this, localIndex, method));
                    }
                }
                foreach (TypeDefData typeDefData in type.TypeDefData.NestedClassList) type.NestedTypes.Add(Types[typeDefData.TableIndex]);
            }
            if (CORLibrary) AppDomain.CacheCORTypes(this);
        }

        internal void LoadStage2()
        {
            foreach (IRType type in Types)
            {
                type.BaseType = AppDomain.ResolveType(type.TypeDefData.Extends);
                foreach (IRField field in type.Fields) field.Type = AppDomain.ResolveType(field.FieldData.ExpandedSignature.Type);
                foreach (IRInterfaceImplementation interfaceImplementation in type.InterfaceImplementations) interfaceImplementation.InterfaceType = AppDomain.ResolveType(interfaceImplementation.InterfaceTypeDefRefOrSpecIndex);
                foreach (IRMethod method in type.Methods)
                {
                    method.ReturnType = AppDomain.ResolveType(method.MethodDefData.ExpandedSignature.RetType);
                    for (int index = 0; index < method.Parameters.Count; ++index) method.Parameters[index].Type = AppDomain.ResolveType(method.MethodDefData.ExpandedSignature.Params[index]);
                    for (int index = 0; index < method.Locals.Count; ++index) method.Locals[index].Type = AppDomain.ResolveType(method.MethodDefData.Body.ExpandedLocalVarSignature.LocalVars[index]);
                }
            }
        }

        internal void LoadStage3()
        {
            foreach (IRType type in Types)
            {
                foreach (IRMethod method in type.Methods) method.ConvertInstructions();
            }
        }
    }
}
