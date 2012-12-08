using Fusion.CLI.Metadata;
using Fusion.CLI.Signature;
using System;
using System.Collections.Generic;

namespace Fusion.IR
{
    public sealed class IRField
    {
        public IRAssembly Assembly = null;

        public string Name = null;

        public IRType ParentType = null;
        public IRType Type = null;

        /// <summary>
        /// True if all the types that this field
        /// uses are fully resolved, aka. if they
        /// are generic, they are fully instantiated.
        /// </summary>
        public bool Resolved { get { return Type.Resolved; } }

        /// <summary>
        /// Resolve any generic types in this field.
        /// </summary>
        /// <param name="typeParams">The type parameters to use to resolve with.</param>
        public void Substitute(GenericParameterCollection typeParams)
        {
            Type.Resolve(ref Type, typeParams, GenericParameterCollection.Empty);
        }

        /// <summary>
        /// Creates a shallow copy of this field.
        /// </summary>
        /// <returns>The shallow copy.</returns>
        public IRField Clone(IRType newParent)
        {
            IRField f = new IRField(this.Assembly);

            f.Name = this.Name;
            f.ParentType = newParent;
            f.Type = this.Type;

            return f;
        }

        public IRField(IRAssembly pAssembly)
        {
            Assembly = pAssembly;
        }

        public bool CompareSignature(FieldSig pFieldSig)
        {
            if (Type != Assembly.AppDomain.PresolveType(pFieldSig.Type)) return false;
            return true;
        }

        public bool CompareSignature(MemberRefData pMemberRefData)
        {
            if (Name != pMemberRefData.Name) return false;
            return CompareSignature(pMemberRefData.ExpandedFieldSignature);
        }
    }
}
