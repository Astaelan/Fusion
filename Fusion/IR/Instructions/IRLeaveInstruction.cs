using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRLeaveInstruction : IRInstruction
    {
        public uint TargetILOffset = 0;

        public IRLeaveInstruction(uint pTargetILOffset)
            : base(IROpcode.Leave)
        {
            TargetILOffset = pTargetILOffset;
        }
    }
}
