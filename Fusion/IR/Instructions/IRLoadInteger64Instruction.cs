using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public class IRLoadInteger64Instruction : IRInstruction
    {
        public long Value { get; private set; }

        public IRLoadInteger64Instruction(long pValue) : base(IROpcode.LoadInteger64)
        {
            Value = pValue;
        }
    }
}
