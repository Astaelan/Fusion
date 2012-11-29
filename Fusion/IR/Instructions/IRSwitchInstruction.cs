using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRSwitchInstruction : IRInstruction
    {
        public uint[] TargetILOffsets = null;

        public IRSwitchInstruction(uint[] pTargetILOffsets)
            : base(IROpcode.Switch)
        {
            TargetILOffsets = pTargetILOffsets;
        }
    }
}
