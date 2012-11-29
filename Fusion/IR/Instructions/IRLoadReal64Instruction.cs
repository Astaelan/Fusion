using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public class IRLoadReal64Instruction : IRInstruction
    {
        public double Value { get; private set; }

        public IRLoadReal64Instruction(double pValue) : base(IROpcode.LoadReal64)
        {
            Value = pValue;
        }
    }
}
