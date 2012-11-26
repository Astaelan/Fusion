using Fusion.CLI.Metadata;
using System;
using System.Collections.Generic;

namespace Fusion.IR
{
    public sealed class IRParameter
    {
        public IRAssembly Assembly;
        public ParamData ParamData;
        public IRMethod ParentMethod;
        public IRType Type = null;

        public IRParameter(IRAssembly pAssembly, ParamData pParamData, IRMethod pParentMethod)
        {
            Assembly = pAssembly;
            ParamData = pParamData;
            ParentMethod = pParentMethod;
        }
    }
}
