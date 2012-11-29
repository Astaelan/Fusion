using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRLoadArrayLengthInstruction : IRInstruction
    {
        public IRLoadArrayLengthInstruction()
            : base(IROpcode.LoadArrayLength)
        {
        }
    }
}
