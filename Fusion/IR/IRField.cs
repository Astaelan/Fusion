using Fusion.CLI.Metadata;
using System;
using System.Collections.Generic;

namespace Fusion.IR
{
    public sealed class IRField
    {
        public IRAssembly Assembly;
        public FieldData FieldData;
        public IRType ParentType;
        public IRType Type;

        public IRField(IRAssembly pAssembly, FieldData pFieldData, IRType pParentType)
        {
            Assembly = pAssembly;
            FieldData = pFieldData;
            ParentType = pParentType;
        }
    }
}
