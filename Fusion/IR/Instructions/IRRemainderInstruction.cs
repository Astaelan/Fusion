using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRRemainderInstruction : IRInstruction
    {
        public IROverflowType OverflowType = IROverflowType.None;

        public IRRemainderInstruction(IROverflowType pOverflowType)
            : base(IROpcode.Remainder)
        {
            OverflowType = pOverflowType;
        }
    }
}
