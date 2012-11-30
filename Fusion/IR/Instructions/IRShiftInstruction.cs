using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public class IRShiftInstruction : IRInstruction
    {
        private IRShiftType mShiftType = IRShiftType.Left;
        public IRShiftType ShiftType 
        {
            get { return mShiftType; }
            private set { mShiftType = value; }
        }

        public IRShiftInstruction(IRShiftType pShiftType) : base(IROpcode.Shift)
        {
            ShiftType = pShiftType;
        }
    }
}
