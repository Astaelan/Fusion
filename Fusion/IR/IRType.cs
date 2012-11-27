using Fusion.CLI.Metadata;
using System;
using System.Collections.Generic;

namespace Fusion.IR
{
    public sealed class IRType
    {
        public static IRType TemporaryVarOrMVarType = new IRType();

        public IRAssembly Assembly = null;
        public List<IRField> Fields = new List<IRField>();
        public List<IRInterfaceImplementation> InterfaceImplementations = new List<IRInterfaceImplementation>();
        public List<IRMethod> Methods = new List<IRMethod>();
        public List<IRType> NestedTypes = new List<IRType>();
        public IRType BaseType = null;

        // Dynamic Types
        public bool IsTemporaryVar = false;
        public bool IsTemporaryMVar = false;
        public uint TemporaryVarOrMVarIndex = 0;
        public IRType PointerType = null;
        public IRType ArrayType = null;

        private IRType() { }

        public IRType(IRAssembly pAssembly)
        {
            Assembly = pAssembly;
        }

        public IRType(IRType pOriginalType)
        {
            Assembly = pOriginalType.Assembly;
            Fields.AddRange(pOriginalType.Fields);
            InterfaceImplementations.AddRange(pOriginalType.InterfaceImplementations);
            Methods.AddRange(pOriginalType.Methods);
            NestedTypes.AddRange(pOriginalType.NestedTypes);
            BaseType = pOriginalType.BaseType;
        }
    }
}
