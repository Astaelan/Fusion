using Fusion.CLI.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.CLI.Signature
{
    public sealed class SigArg
    {
        public SigArgType ArgType = SigArgType.Param;
        public string Name = null;
        public SigElementType PrimaryType = SigElementType.End;
        public SigElementType SecondaryType = SigElementType.End;
        public TypeDefData EnumType = null;
        public SigElementType EnumBaseType = SigElementType.End;
        public List<SigElem> Elems = null;
    }
}
