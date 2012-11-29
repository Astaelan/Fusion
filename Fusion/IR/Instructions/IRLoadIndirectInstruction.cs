using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRLoadIndirectInstruction : IRInstruction
    {
        public IRType Type = null;

        public IRLoadIndirectInstruction(IRType pType)
            : base(IROpcode.LoadIndirect)
        {
            Type = pType;
        }
    }
}
