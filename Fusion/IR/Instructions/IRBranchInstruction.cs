using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRBranchInstruction : IRInstruction
    {
        public IRBranchCondition BranchCondition = IRBranchCondition.Always;
        public uint TargetILOffset = 0;

        public IRBranchInstruction(IRBranchCondition pBranchCondition, uint pTargetILOffset)
            : base(IROpcode.Branch)
        {
            BranchCondition = pBranchCondition;
            TargetILOffset = pTargetILOffset;
        }
    }
}
