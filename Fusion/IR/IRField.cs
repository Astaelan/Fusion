using Fusion.CLI.Metadata;
using System;
using System.Collections.Generic;

namespace Fusion.IR
{
    public sealed class IRField
    {
        public IRAssembly Assembly = null;
        public IRType ParentType = null;
        public IRType Type = null;

        public IRField(IRAssembly pAssembly)
        {
            Assembly = pAssembly;
        }
    }
}
