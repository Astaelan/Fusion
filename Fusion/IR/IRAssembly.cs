using Fusion.CLI;
using Fusion.CLI.Metadata;
using Fusion.CLI.Signature;
using System;
using System.Collections.Generic;

namespace Fusion.IR
{
    public sealed class IRAssembly
    {
        public IRAppDomain AppDomain = null;
        public CLIFile File = null;
        public bool CORLibrary = false;
        public List<IRType> Types = new List<IRType>();
        public List<IRField> Fields = new List<IRField>();
        public List<IRMethod> Methods = new List<IRMethod>();

        internal IRAssembly(IRAppDomain pAppDomain, CLIFile pCLIFile, bool pCORLibrary)
        {
            AppDomain = pAppDomain;
            File = pCLIFile;
            CORLibrary = pCORLibrary;
        }

        internal void LoadStage1()
        {
            foreach (TypeDefData typeDefData in File.TypeDefTable) Types.Add(new IRType(this));
            foreach (FieldData fieldData in File.FieldTable) Fields.Add(new IRField(this));
            foreach (MethodDefData methodDefData in File.MethodDefTable) Methods.Add(new IRMethod(this));

            for (int typeIndex = 0; typeIndex < Types.Count; ++typeIndex)
            {
                IRType type = Types[typeIndex];
                TypeDefData typeDefData = File.TypeDefTable[typeIndex];

                type.Namespace = typeDefData.TypeNamespace;
                type.Name = typeDefData.TypeName;

                foreach (FieldData fieldData in typeDefData.FieldList)
                {
                    IRField field = Fields[fieldData.TableIndex];
                    field.Name = fieldData.Name;
                    field.ParentType = type;
                    type.Fields.Add(field);
                }
                foreach (MethodDefData methodDefData in typeDefData.MethodList)
                {
                    IRMethod method = Methods[methodDefData.TableIndex];
                    method.Name = methodDefData.Name;
                    method.ParentType = type;
                    type.Methods.Add(method);

                    foreach (ParamData paramData in methodDefData.ParamList)
                    {
                        IRParameter parameter = new IRParameter(this);
                        parameter.ParentMethod = method;
                        method.Parameters.Add(parameter);
                    }

                    if (methodDefData.Body != null && methodDefData.Body.ExpandedLocalVarSignature != null)
                    {
                        foreach (SigLocalVar sigLocalVar in methodDefData.Body.ExpandedLocalVarSignature.LocalVars)
                        {
                            IRLocal local = new IRLocal(this);
                            local.ParentMethod = method;
                            method.Locals.Add(local);
                        }
                    }
                }
            }
            for (int typeIndex = 0; typeIndex < Types.Count; ++typeIndex)
            {
                IRType type = Types[typeIndex];
                TypeDefData typeDefData = File.TypeDefTable[typeIndex];

                foreach (TypeDefData nestedTypeDefData in typeDefData.NestedClassList)
                {
                    IRType nestedType = Types[nestedTypeDefData.TableIndex];
                    nestedType.Namespace = type.Namespace + "." + type.Name;
                    type.NestedTypes.Add(nestedType);
                }
            }
            if (CORLibrary) AppDomain.CacheCORTypes(this);
        }

        internal void LoadStage2()
        {
            for (int typeIndex = 0; typeIndex < Types.Count; ++typeIndex)
            {
                IRType type = Types[typeIndex];
                TypeDefData typeDefData = File.TypeDefTable[typeIndex];
                if (typeDefData.Extends.Type != TypeDefRefOrSpecIndex.TypeDefRefOrSpecType.TypeDef || typeDefData.Extends.TypeDef != null) type.BaseType = AppDomain.PresolveType(typeDefData.Extends);
                for (int fieldIndex = 0; fieldIndex < type.Fields.Count; ++fieldIndex)
                {
                    IRField field = type.Fields[fieldIndex];
                    field.Type = AppDomain.PresolveType(typeDefData.FieldList[fieldIndex].ExpandedSignature);
                }
                for (int methodIndex = 0; methodIndex < type.Methods.Count; ++methodIndex)
                {
                    IRMethod method = type.Methods[methodIndex];
                    method.ReturnType = AppDomain.PresolveType(typeDefData.MethodList[methodIndex].ExpandedSignature.RetType);
                    for (int parameterIndex = 0; parameterIndex < method.Parameters.Count; ++parameterIndex)
                    {
                        IRParameter parameter = method.Parameters[parameterIndex];
                        parameter.Type = AppDomain.PresolveType(typeDefData.MethodList[methodIndex].ExpandedSignature.Params[parameterIndex]);
                    }
                    for (int localIndex = 0; localIndex < method.Locals.Count; ++localIndex)
                    {
                        IRLocal local = method.Locals[localIndex];
                        local.Type = AppDomain.PresolveType(typeDefData.MethodList[methodIndex].Body.ExpandedLocalVarSignature.LocalVars[localIndex]);
                    }
                }
            }
        }

        internal void LoadStage3()
        {
            for (int typeIndex = 0; typeIndex < Types.Count; ++typeIndex)
            {
                IRType type = Types[typeIndex];
                for (int methodIndex = 0; methodIndex < type.Methods.Count; ++methodIndex)
                {
                    IRMethod method = type.Methods[methodIndex];
                    MethodDefData methodDefData = File.MethodDefTable[methodIndex];
                    method.ConvertInstructions(methodDefData);
                }
            }
        }
    }
}
