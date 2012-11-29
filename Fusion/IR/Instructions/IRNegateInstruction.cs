using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public class IRNegateInstruction : IRInstruction
    {
        public IRNegateInstruction() : base(IROpcode.Negate)
        {
        }
    }
}
