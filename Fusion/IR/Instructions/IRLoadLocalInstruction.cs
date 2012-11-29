using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRLoadLocalInstruction : IRInstruction
    {
        public uint LocalIndex = 0;

        public IRLoadLocalInstruction(uint pLocalIndex)
            : base(IROpcode.LoadLocal)
        {
            LocalIndex = pLocalIndex;
        }
    }
}
