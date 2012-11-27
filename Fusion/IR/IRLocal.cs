using Fusion.CLI.Metadata;
using Fusion.CLI.Signature;
using System;
using System.Collections.Generic;

namespace Fusion.IR
{
    public sealed class IRLocal
    {
        public IRAssembly Assembly = null;
        public IRMethod ParentMethod = null;
        public IRType Type = null;

        public IRLocal(IRAssembly pAssembly)
        {
            Assembly = pAssembly;
        }
    }
}
