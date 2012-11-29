using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRAddInstruction : IRInstruction
    {
        public IROverflowType OverflowType = IROverflowType.None;

        public IRAddInstruction(IROverflowType pOverflowType)
            : base(IROpcode.Add)
        {
            OverflowType = pOverflowType;
        }
    }
}
