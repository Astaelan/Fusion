using Fusion.CLI.Metadata;
using Fusion.CLI.Signature;
using System;
using System.Collections.Generic;

namespace Fusion.IR
{
    public sealed class IRLocal
    {
        public IRAssembly Assembly;
        public uint LocalIndex = 0;
        public IRMethod ParentMethod;
        public IRType Type = null;

        public IRLocal(IRAssembly pAssembly, uint pLocalIndex, IRMethod pParentMethod)
        {
            Assembly = pAssembly;
            LocalIndex = pLocalIndex;
            ParentMethod = pParentMethod;
        }
    }
}
