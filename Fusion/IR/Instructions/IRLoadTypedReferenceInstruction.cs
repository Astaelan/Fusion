using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRLoadTypedReferenceInstruction : IRInstruction
    {
        public IRType Type = null;

        public IRLoadTypedReferenceInstruction(IRType pType)
            : base(IROpcode.LoadTypedReference)
        {
            Type = pType;
        }
    }
}
