using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRShiftInstruction : IRInstruction
    {
        public IRShiftType ShiftType = IRShiftType.Left;

        public IRShiftInstruction(IRShiftType pShiftType)
            : base(IROpcode.Shift)
        {
            ShiftType = pShiftType;
        }
    }
}
