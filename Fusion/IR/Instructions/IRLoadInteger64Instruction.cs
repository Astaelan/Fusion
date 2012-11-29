using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRLoadInteger64Instruction : IRInstruction
    {
        public long Value = 0;

        public IRLoadInteger64Instruction(long pValue)
            : base(IROpcode.LoadInteger64)
        {
            Value = pValue;
        }
    }
}
