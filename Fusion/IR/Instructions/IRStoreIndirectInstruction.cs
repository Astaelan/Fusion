using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public class IRStoreIndirectInstruction : IRInstruction
    {
        public IRType Type { get; private set; }

        public IRStoreIndirectInstruction(IRType pType) : base(IROpcode.StoreIndirect)
        {
            Type = pType;
        }
    }
}
