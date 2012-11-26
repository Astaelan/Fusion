using Fusion.CLI.Metadata;
using Fusion.CLI.Signature;
using Fusion.IL;
using System;
using System.Collections.Generic;

namespace Fusion.IR
{
    public sealed class IRInterfaceImplementation
    {
        public readonly IRAssembly Assembly;
        public readonly TypeDefRefOrSpecIndex InterfaceTypeDefRefOrSpecIndex;
        public readonly IRType ParentType;
        public IRType InterfaceType;

        public IRInterfaceImplementation(IRAssembly pAssembly, TypeDefRefOrSpecIndex pInterfaceTypeDefRefOrSpecIndex, IRType pParentType)
        {
            Assembly = pAssembly;
            InterfaceTypeDefRefOrSpecIndex = pInterfaceTypeDefRefOrSpecIndex;
            ParentType = pParentType;
        }
    }
}
