using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRLoadReal64Instruction : IRInstruction
    {
        public double Value = 0;

        public IRLoadReal64Instruction(double pValue)
            : base(IROpcode.LoadReal64)
        {
            Value = pValue;
        }
    }
}
