using Fusion.CLI.Metadata;
using System;
using System.Collections.Generic;

namespace Fusion.IR
{
    public sealed class IRParameter
    {
        public IRAssembly Assembly = null;
        public IRMethod ParentMethod = null;
        public IRType Type = null;

        public IRParameter(IRAssembly pAssembly)
        {
            Assembly = pAssembly;
        }
    }
}
