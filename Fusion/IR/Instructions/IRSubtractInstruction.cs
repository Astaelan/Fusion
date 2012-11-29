using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRSubtractInstruction : IRInstruction
    {
        public IROverflowType OverflowType = IROverflowType.None;

        public IRSubtractInstruction(IROverflowType pOverflowType)
            : base(IROpcode.Subtract)
        {
            OverflowType = pOverflowType;
        }
    }
}
