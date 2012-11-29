using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRLoadNullInstruction : IRInstruction
    {
        public IRLoadNullInstruction()
            : base(IROpcode.LoadNull)
        {
        }
    }
}
