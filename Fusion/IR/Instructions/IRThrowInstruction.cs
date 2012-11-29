using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRThrowInstruction : IRInstruction
    {
        public IRThrowInstruction()
            : base(IROpcode.Throw)
        {
        }
    }
}
