using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public class IRLoadArrayLengthInstruction : IRInstruction
    {
        public IRLoadArrayLengthInstruction() : base(IROpcode.LoadArrayLength)
        {
        }
    }
}
