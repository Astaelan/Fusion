using Fusion.CLI.Metadata;
using System;
using System.Collections.Generic;

namespace Fusion.IR
{
    public sealed class IRType
    {
        public static IRType TemporaryVarType = new IRType();
        public static IRType TemporaryMVarType = new IRType();

        public IRAssembly Assembly = null;
        public TypeDefData TypeDefData = null;
        public List<IRField> Fields = new List<IRField>();
        public List<IRInterfaceImplementation> InterfaceImplementations = new List<IRInterfaceImplementation>();
        public List<IRMethod> Methods = new List<IRMethod>();
        public List<IRType> NestedTypes = new List<IRType>();
        public IRType BaseType = null;

        // Dynamic Types
        public uint TemporaryVarOrMVarIndex = 0;
        public IRType PointerType = null;
        public IRType ArrayType = null;

        private IRType() { }

        public IRType(IRAssembly pAssembly, TypeDefData pTypeDefData)
        {
            Assembly = pAssembly;
            TypeDefData = pTypeDefData;
        }

        public IRType Clone()
        {
            IRType type = new IRType();
            type.Assembly = Assembly;
            type.TypeDefData = TypeDefData;
            type.Fields.AddRange(Fields);
            type.InterfaceImplementations.AddRange(InterfaceImplementations);
            type.Methods.AddRange(Methods);
            type.NestedTypes.AddRange(NestedTypes);
            type.BaseType = BaseType;
            return type;
        }
    }
}
