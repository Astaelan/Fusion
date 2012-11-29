using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRDivideInstruction : IRInstruction
    {
        public IROverflowType OverflowType = IROverflowType.None;

        public IRDivideInstruction(IROverflowType pOverflowType)
            : base(IROpcode.Divide)
        {
            OverflowType = pOverflowType;
        }
    }
}
