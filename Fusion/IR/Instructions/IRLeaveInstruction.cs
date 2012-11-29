using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public class IRLeaveInstruction : IRInstruction
    {
        public uint TargetILOffset { get; private set; }

        public IRLeaveInstruction(uint pTargetILOffset) : base(IROpcode.Leave)
        {
            TargetILOffset = pTargetILOffset;
        }
    }
}
