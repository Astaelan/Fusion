using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public class IRReturnInstruction : IRInstruction
    {
        public IRReturnInstruction() : base(IROpcode.Return)
        {
        }
    }
}
