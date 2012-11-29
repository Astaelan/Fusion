using Fusion.CLI.Metadata;
using System;
using System.Collections.Generic;

namespace Fusion.IR
{
    public sealed class IRType
    {
        public static List<IRType> VarPlaceholders = new List<IRType>();
        public static List<IRType> MVarPlaceholders = new List<IRType>();

        public static IRType GetVarPlaceholder(uint pIndex)
        {
            while (pIndex + 1 >= VarPlaceholders.Count) VarPlaceholders.Add(new IRType() { IsTemporaryVar = true, TemporaryVarOrMVarIndex = (uint)VarPlaceholders.Count });
            return VarPlaceholders[(int)pIndex];
        }

        public static IRType GetMVarPlaceholder(uint pIndex)
        {
            while (pIndex + 1 >= MVarPlaceholders.Count) MVarPlaceholders.Add(new IRType() { IsTemporaryMVar = true, TemporaryVarOrMVarIndex = (uint)MVarPlaceholders.Count });
            return MVarPlaceholders[(int)pIndex];
        }

        public IRAssembly Assembly = null;

        public string Namespace = null;
        public string Name = null;

        public List<IRField> Fields = new List<IRField>();
        public List<IRMethod> Methods = new List<IRMethod>();
        public List<IRType> NestedTypes = new List<IRType>();
        public IRType BaseType = null;

        // Dynamic Types
        public bool IsTemporaryVar = false;
        public bool IsTemporaryMVar = false;
        public uint TemporaryVarOrMVarIndex = 0;
        public bool IsGeneric = false;
        public string GenericHash = null;
        public bool GenericParametersResolved = false;
        public List<IRType> GenericParameters = new List<IRType>();
        public IRType PointerType = null;
        public IRType ArrayType = null;

        private IRType() { }

        public IRType(IRAssembly pAssembly) { Assembly = pAssembly; }

        public IRType(IRType pOriginalType)
        {
            Assembly = pOriginalType.Assembly;

            Namespace = pOriginalType.Namespace;
            Name = pOriginalType.Name;

            Fields.AddRange(pOriginalType.Fields);
            Methods.AddRange(pOriginalType.Methods);
            NestedTypes.AddRange(pOriginalType.NestedTypes);
            BaseType = pOriginalType.BaseType;

            IsTemporaryVar = pOriginalType.IsTemporaryVar;
            IsTemporaryMVar = pOriginalType.IsTemporaryMVar;
            TemporaryVarOrMVarIndex = pOriginalType.TemporaryVarOrMVarIndex;
            IsGeneric = pOriginalType.IsGeneric;
            GenericHash = pOriginalType.GenericHash;
            GenericParameters.AddRange(pOriginalType.GenericParameters);
            PointerType = pOriginalType.PointerType;
            ArrayType = pOriginalType.ArrayType;
        }
    }
}
