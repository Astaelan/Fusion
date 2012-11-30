using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public class IRConvertUncheckedInstruction : IRInstruction
    {
        public IRType Type { get; private set; }

        public IRConvertUncheckedInstruction(IRType pType) : base(IROpcode.ConvertUnchecked)
        {
            Type = pType;
        }
    }
}
