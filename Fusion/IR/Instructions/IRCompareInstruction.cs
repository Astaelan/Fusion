using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public class IRCompareInstruction : IRInstruction
    {
        private IRCompareCondition mCompareCondition = IRCompareCondition.Equal;
        public IRCompareCondition CompareCondition
        {
            get { return mCompareCondition; }
            private set { mCompareCondition = value; }
        }

        public IRCompareInstruction(IRCompareCondition pCompareCondition) : base(IROpcode.Compare)
        {
            CompareCondition = pCompareCondition;
        }
    }
}
