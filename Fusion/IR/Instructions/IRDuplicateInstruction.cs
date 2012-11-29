using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public class IRDuplicateInstruction : IRInstruction
    {
        public IRDuplicateInstruction() : base(IROpcode.Duplicate)
        {
        }
    }
}
