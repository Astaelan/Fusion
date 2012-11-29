using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRConvertCheckedInstruction : IRInstruction
    {
        public IRType Type = null;
        public IROverflowType OverflowType = IROverflowType.None;

        public IRConvertCheckedInstruction(IRType pType, IROverflowType pOverflowType)
            : base(IROpcode.ConvertChecked)
        {
            Type = pType;
            OverflowType = pOverflowType;
        }
    }
}
