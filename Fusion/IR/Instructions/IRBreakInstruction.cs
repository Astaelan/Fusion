using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRBreakInstruction : IRInstruction
    {
        public IRBreakInstruction()
            : base(IROpcode.Break)
        {
        }
    }
}
