using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRLoadInteger32Instruction : IRInstruction
    {
        public int Value = 0;

        public IRLoadInteger32Instruction(int pValue)
            : base(IROpcode.LoadInteger32)
        {
            Value = pValue;
        }
    }
}
