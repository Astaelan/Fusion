using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public class IROrInstruction : IRInstruction
    {
        public IROrInstruction() : base(IROpcode.Or)
        {
        }
    }
}
