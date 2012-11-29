using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
