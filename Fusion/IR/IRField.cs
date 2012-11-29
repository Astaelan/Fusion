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
