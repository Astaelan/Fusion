using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRConvertUncheckedInstruction : IRInstruction
    {
        public IRType Type = null;

        public IRConvertUncheckedInstruction(IRType pType)
            : base(IROpcode.ConvertUnchecked)
        {
            Type = pType;
        }
    }
}
