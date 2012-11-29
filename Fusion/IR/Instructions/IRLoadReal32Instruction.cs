using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public class IRLoadReal32Instruction : IRInstruction
    {
        public float Value { get; private set; }

        public IRLoadReal32Instruction(float pValue) : base(IROpcode.LoadReal32)
        {
            Value = pValue;
        }
    }
}
