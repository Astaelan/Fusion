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

        public bool Resolved { get { return Type.Resolved; } }

        public IRParameter(IRAssembly pAssembly)
        {
            Assembly = pAssembly;
        }

        public IRParameter Clone(IRMethod newMethod)
        {
            IRParameter p = new IRParameter(this.Assembly);
            p.ParentMethod = newMethod;
            p.Type = this.Type;
            return p;
        }
    }
}
