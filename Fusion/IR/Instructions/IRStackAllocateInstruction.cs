using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public class IRStackAllocateInstruction : IRInstruction
    {
        public IRStackAllocateInstruction() : base(IROpcode.StackAllocate)
        {
        }
    }
}
