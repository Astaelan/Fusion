using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public class IRLoadTypedReferenceTypeInstruction : IRInstruction
    {
        public IRLoadTypedReferenceTypeInstruction() : base(IROpcode.LoadTypedReferenceType)
        {
        }
    }
}
