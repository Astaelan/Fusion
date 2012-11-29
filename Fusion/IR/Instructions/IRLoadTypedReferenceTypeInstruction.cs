using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRLoadTypedReferenceTypeInstruction : IRInstruction
    {
        public IRLoadTypedReferenceTypeInstruction()
            : base(IROpcode.LoadTypedReferenceType)
        {
        }
    }
}
