using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRNotInstruction : IRInstruction
    {
        public IRNotInstruction()
            : base(IROpcode.Not)
        {
        }
    }
}
