using Fusion.CLI.Metadata;
using Fusion.CLI.Signature;
using Fusion.IL;
using System;
using System.Collections.Generic;

namespace Fusion.IR
{
    public sealed class IRInterfaceImplementation
    {
        public IRAssembly Assembly = null;
        public IRType ParentType = null;
        public IRType InterfaceType = null;

        public IRInterfaceImplementation(IRAssembly pAssembly)
        {
            Assembly = pAssembly;
        }
    }
}
