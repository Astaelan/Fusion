using System;
using System.Collections.Generic;

namespace Fusion.IR
{
    public sealed class IRType
    {
        public static readonly List<IRType> VarPlaceholders = new List<IRType>();
        public static readonly List<IRType> MVarPlaceholders = new List<IRType>();

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

        public readonly List<IRField> Fields = new List<IRField>();
        public readonly List<IRMethod> Methods = new List<IRMethod>();
        public readonly List<IRType> NestedTypes = new List<IRType>();
        public IRType BaseType = null;

        // We can cache this because the state
        // of the cache doesn't extend to any
        // derived type.
        private bool? mResolvedCache;
        /// <summary>
        /// True if this type and it's members are fully
        /// resolved, aka. this type has been fully instantiated.
        /// </summary>
        public bool Resolved
        {
            get
            {
                if (mResolvedCache != null)
                    return mResolvedCache.Value;

                bool ressed = false;
                if (IsGeneric)
                {
                    if (IsTemporaryVar) goto SetCache;
                    if (IsTemporaryMVar) goto SetCache;
                    if (PointerType != null && !PointerType.Resolved) goto SetCache;
                    if (ArrayType != null && !ArrayType.Resolved) goto SetCache;
                    if (BaseType != null && !BaseType.Resolved) goto SetCache;
                    if (!Fields.TrueForAll(f => f.Resolved)) goto SetCache;
                    if (!Methods.TrueForAll(m => m.Resolved)) goto SetCache;
                }
                ressed = true;

            SetCache:
                mResolvedCache = ressed;
                return mResolvedCache.Value;
            }
        }

        // Dynamic Types
        public bool IsTemporaryVar = false;
        public bool IsTemporaryMVar = false;
        public uint TemporaryVarOrMVarIndex = 0;

        private bool? mIsGenericCache;
        /// <summary>
        /// True if this type is generic, this is still true
        /// even after all generic parameters have been resolved.
        /// </summary>
        public bool IsGeneric
        {
            get 
            {
                if (mIsGenericCache != null)
                    return mIsGenericCache.Value;
                
                mIsGenericCache =
                    GenericParameters.Count > 0 ||
                    IsTemporaryVar ||
                    IsTemporaryMVar ||
                    (
                        ArrayType != null &&
                        ArrayType.IsGeneric
                    )
                ;

                return mIsGenericCache.Value; 
            }
        }
        public IRType GenericType = null;
        public readonly GenericParameterCollection GenericParameters = new GenericParameterCollection();
        public IRType PointerType = null;
        public IRType ArrayType = null;

        private IRType() { }

        public IRType(IRAssembly pAssembly) { Assembly = pAssembly; }

        /// <summary>
        /// This creates a shallow copy of this <see cref="IRType"/>.
        /// </summary>
        /// <returns>The new IRType.</returns>
        public IRType Clone()
        {
            IRType t = new IRType(this.Assembly);

            t.Fields.AddRange(this.Fields);
            t.Methods.AddRange(this.Methods);
            t.NestedTypes.AddRange(this.NestedTypes);
            t.GenericParameters.AddRange(this.GenericParameters);

            t.BaseType = this.BaseType;
            t.ArrayType = this.ArrayType;
            t.GenericType = this.GenericType;
            t.IsTemporaryMVar = this.IsTemporaryMVar;
            t.IsTemporaryVar = this.IsTemporaryVar;
            t.Name = this.Name;
            t.Namespace = this.Namespace;
            t.PointerType = this.PointerType;
            t.TemporaryVarOrMVarIndex = this.TemporaryVarOrMVarIndex;

            return t;
        }
    }
}
