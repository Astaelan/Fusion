using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public class IRNopInstruction : IRInstruction
    {
        public bool ForceEmit { get; private set; }

        public IRNopInstruction(bool pForceEmit) : base(IROpcode.Nop)
        {
            ForceEmit = pForceEmit;
        }
    }
}
