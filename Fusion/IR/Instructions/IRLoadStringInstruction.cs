using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public class IRLoadStringInstruction : IRInstruction
    {
        public string Value { get; private set; }

        public IRLoadStringInstruction(string pValue) : base(IROpcode.LoadString)
        {
            Value = pValue;
        }
    }
}
