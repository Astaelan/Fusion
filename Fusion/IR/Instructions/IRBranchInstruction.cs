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
        
        private uint mTargetILOffset = 0;
        public uint TargetILOffset
        {
            get { return mTargetILOffset; }
            set { mTargetILOffset = value; }
        }

        public IRBranchInstruction(IRBranchCondition pBranchCondition, uint pTargetILOffset) : base(IROpcode.Branch)
        {
            BranchCondition = pBranchCondition;
            TargetILOffset = pTargetILOffset;
        }
    }
}
