using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRLoadTypedReferenceAddressInstruction : IRInstruction
    {
        public IRType Type = null;

        public IRLoadTypedReferenceAddressInstruction(IRType pType)
            : base(IROpcode.LoadTypedReferenceAddress)
        {
            Type = pType;
        }
    }
}
