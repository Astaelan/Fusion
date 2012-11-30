using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public class IRDivideInstruction : IRInstruction
    {
        private IROverflowType mOverflowType = IROverflowType.None;
        public IROverflowType OverflowType
        {
            get { return mOverflowType; }
            private set { mOverflowType = value; }
        }

        public IRDivideInstruction(IROverflowType pOverflowType) : base(IROpcode.Divide)
        {
            OverflowType = pOverflowType;
        }
    }
}
