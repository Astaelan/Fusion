using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public class IRBreakInstruction : IRInstruction
    {
        public IRBreakInstruction() : base(IROpcode.Break)
        {
        }
    }
}
