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

        public bool Resolved { get { return Type.Resolved; } }

        public IRLocal(IRAssembly pAssembly)
        {
            Assembly = pAssembly;
        }

        public IRLocal Clone(IRMethod newMethod)
        {
            IRLocal l = new IRLocal(this.Assembly);
            l.ParentMethod = newMethod;
            l.Type = this.Type;
            return l;
        }
    }
}
