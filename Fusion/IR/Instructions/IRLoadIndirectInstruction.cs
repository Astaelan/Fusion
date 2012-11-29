using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public class IRLoadIndirectInstruction : IRInstruction
    {
        public IRType Type { get; private set; }

        public IRLoadIndirectInstruction(IRType pType) : base(IROpcode.LoadIndirect)
        {
            Type = pType;
        }
    }
}
