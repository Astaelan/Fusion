using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRMultiplyInstruction : IRInstruction
    {
        public IROverflowType OverflowType = IROverflowType.None;

        public IRMultiplyInstruction(IROverflowType pOverflowType)
            : base(IROpcode.Multiply)
        {
            OverflowType = pOverflowType;
        }
    }
}
