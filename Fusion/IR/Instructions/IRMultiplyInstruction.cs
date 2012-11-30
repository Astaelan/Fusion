using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public class IRMultiplyInstruction : IRInstruction
    {
        private IROverflowType mOverflowType = IROverflowType.None;
        public IROverflowType OverflowType
        {
            get { return mOverflowType; }
            private set { mOverflowType = value; }
        }

        public IRMultiplyInstruction(IROverflowType pOverflowType) : base(IROpcode.Multiply)
        {
            OverflowType = pOverflowType;
        }
    }
}
