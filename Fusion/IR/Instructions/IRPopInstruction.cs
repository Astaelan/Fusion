using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public class IRPopInstruction : IRInstruction
    {
        public IRPopInstruction() : base(IROpcode.Pop)
        {
        }
    }
}
