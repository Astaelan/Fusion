using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRStoreLocalInstruction : IRInstruction
    {
        public uint LocalIndex = 0;

        public IRStoreLocalInstruction(uint pLocalIndex)
            : base(IROpcode.StoreLocal)
        {
            LocalIndex = pLocalIndex;
        }
    }
}
