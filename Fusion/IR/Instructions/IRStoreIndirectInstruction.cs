using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRStoreIndirectInstruction : IRInstruction
    {
        public IRType Type = null;

        public IRStoreIndirectInstruction(IRType pType)
            : base(IROpcode.StoreIndirect)
        {
            Type = pType;
        }
    }
}
