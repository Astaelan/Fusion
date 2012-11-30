using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public class IRNotInstruction : IRInstruction
    {
        public IRNotInstruction() : base(IROpcode.Not)
        {
        }
    }
}
