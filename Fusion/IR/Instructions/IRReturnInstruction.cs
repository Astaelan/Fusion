using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRReturnInstruction : IRInstruction
    {
        public IRReturnInstruction()
            : base(IROpcode.Return)
        {
        }
    }
}
