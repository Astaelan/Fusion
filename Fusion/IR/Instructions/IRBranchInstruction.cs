using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public class IRBranchInstruction : IRInstruction
    {
        private IRBranchCondition mBranchCondition = IRBranchCondition.Always;
        public IRBranchCondition BranchCondition
        {
            get { return mBranchCondition; }
            set { mBranchCondition = value; }
        }
        
        public uint TargetILOffset { get; set; }

        public IRBranchInstruction(IRBranchCondition pBranchCondition, uint pTargetILOffset) : base(IROpcode.Branch)
        {
            BranchCondition = pBranchCondition;
            TargetILOffset = pTargetILOffset;
        }
    }
}
