using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRCompareInstruction : IRInstruction
    {
        public IRCompareCondition CompareCondition = IRCompareCondition.Equal;

        public IRCompareInstruction(IRCompareCondition pCompareCondition)
            : base(IROpcode.Compare)
        {
            CompareCondition = pCompareCondition;
        }
    }
}
