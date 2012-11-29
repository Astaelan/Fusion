using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRXorInstruction : IRInstruction
    {
        public IRXorInstruction()
            : base(IROpcode.Xor)
        {
        }
    }
}
