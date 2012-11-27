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
                foreach (FieldData fieldData in typeDefData.FieldList)
                {
                    IRField field = Fields[fieldData.TableIndex];
                    field.ParentType = type;
                    type.Fields.Add(field);
                }
                foreach (MethodDefData methodDefData in typeDefData.MethodList)
                {
                    IRMethod method = Methods[methodDefData.TableIndex];
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
                foreach (TypeDefData nestedTypeDefData in typeDefData.NestedClassList) type.NestedTypes.Add(Types[nestedTypeDefData.TableIndex]);
            }
            if (CORLibrary) AppDomain.CacheCORTypes(this);
        }

        internal void LoadStage2()
        {
            foreach (IRType type in Types)
            {
                if (type.TypeDefData.Extends.Type != TypeDefRefOrSpecIndex.TypeDefRefOrSpecType.TypeDef || type.TypeDefData.Extends.TypeDef != null) type.BaseType = AppDomain.ResolveType(type.TypeDefData.Extends);
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
