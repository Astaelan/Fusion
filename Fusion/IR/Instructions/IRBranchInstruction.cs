using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRBranchInstruction : IRInstruction
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
