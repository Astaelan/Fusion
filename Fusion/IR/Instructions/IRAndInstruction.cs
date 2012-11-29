using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public class IRAndInstruction : IRInstruction
    {
        public IRAndInstruction() : base(IROpcode.And)
        {
        }
    }
}
