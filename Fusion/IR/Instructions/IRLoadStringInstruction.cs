using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRLoadStringInstruction : IRInstruction
    {
        public string Value = null;

        public IRLoadStringInstruction(string pValue)
            : base(IROpcode.LoadString)
        {
            Value = pValue;
        }
    }
}
