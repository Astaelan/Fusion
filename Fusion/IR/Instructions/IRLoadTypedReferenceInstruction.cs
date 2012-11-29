using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public class IRLoadTypedReferenceInstruction : IRInstruction
    {
        public IRType Type { get; private set; }

        public IRLoadTypedReferenceInstruction(IRType pType) : base(IROpcode.LoadTypedReference)
        {
            Type = pType;
        }
    }
}
