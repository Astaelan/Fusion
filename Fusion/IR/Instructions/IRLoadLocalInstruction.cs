using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public class IRLoadLocalInstruction : IRInstruction
    {
        public uint LocalIndex { get; private set; }

        public IRLoadLocalInstruction(uint pLocalIndex) : base(IROpcode.LoadLocal)
        {
            LocalIndex = pLocalIndex;
        }
    }
}
