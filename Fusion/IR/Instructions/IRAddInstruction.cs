using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public sealed class IRAddInstruction : IRInstruction
    {
        private IROverflowType mOverflowType = IROverflowType.None;
        public IROverflowType OverflowType 
        {
            get { return mOverflowType; }
            private set { mOverflowType = value; }
        }

        public IRAddInstruction(IROverflowType pOverflowType) : base(IROpcode.Add)
        {
            OverflowType = pOverflowType;
        }
    }
}
