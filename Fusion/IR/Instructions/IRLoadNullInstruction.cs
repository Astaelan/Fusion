using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public class IRLoadNullInstruction : IRInstruction
    {
        public IRLoadNullInstruction() : base(IROpcode.LoadNull)
        {
        }
    }
}
