using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRLoadReal32Instruction : IRInstruction
    {
        public float Value = 0;

        public IRLoadReal32Instruction(float pValue)
            : base(IROpcode.LoadReal32)
        {
            Value = pValue;
        }
    }
}
