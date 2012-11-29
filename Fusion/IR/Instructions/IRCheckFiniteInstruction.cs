using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public class IRCheckFiniteInstruction : IRInstruction
    {
        public IRCheckFiniteInstruction() : base(IROpcode.CheckFinite)
        {
        }
    }
}
