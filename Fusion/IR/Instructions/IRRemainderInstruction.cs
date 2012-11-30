using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRRemainderInstruction : IRInstruction
    {
        private IROverflowType mOverflowType = IROverflowType.None;
        public IROverflowType OverflowType
        {
            get { return mOverflowType; }
            private set { mOverflowType = value; }
        }

        public IRRemainderInstruction(IROverflowType pOverflowType) : base(IROpcode.Remainder)
        {
            OverflowType = pOverflowType;
        }
    }
}
