using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public class IRLoadInteger32Instruction : IRInstruction
    {
        public int Value { get; private set; }

        public IRLoadInteger32Instruction(int pValue) : base(IROpcode.LoadInteger32)
        {
            Value = pValue;
        }
    }
}
