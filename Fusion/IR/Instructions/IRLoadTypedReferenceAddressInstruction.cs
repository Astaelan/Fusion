using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public class IRLoadTypedReferenceAddressInstruction : IRInstruction
    {
        public IRType Type { get; private set; }

        public IRLoadTypedReferenceAddressInstruction(IRType pType) : base(IROpcode.LoadTypedReferenceAddress)
        {
            Type = pType;
        }
    }
}
