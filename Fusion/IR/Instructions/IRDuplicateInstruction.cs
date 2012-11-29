using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRDuplicateInstruction : IRInstruction
    {
        public IRDuplicateInstruction()
            : base(IROpcode.Duplicate)
        {
        }
    }
}
