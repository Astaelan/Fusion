using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public class IRStoreLocalInstruction : IRInstruction
    {
        public uint LocalIndex { get; private set; }

        public IRStoreLocalInstruction(uint pLocalIndex) : base(IROpcode.StoreLocal)
        {
            LocalIndex = pLocalIndex;
        }
    }
}
