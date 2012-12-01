using System;
using System.Collections.Generic;

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
        
        public uint TargetILOffset { get; set; }

        public IRBranchInstruction(IRBranchCondition pBranchCondition, uint pTargetILOffset) : base(IROpcode.Branch)
        {
            BranchCondition = pBranchCondition;
            TargetILOffset = pTargetILOffset;
        }

        public override void Linearize(Stack<IRStackObject> pStack)
        {
            switch (BranchCondition)
            {
                case IRBranchCondition.Always: break;
                case IRBranchCondition.False:
                case IRBranchCondition.True: Sources.Add(new IRLinearizedTarget(pStack.Pop().LinearizedTarget)); break;
                case IRBranchCondition.Equal:
                case IRBranchCondition.GreaterOrEqual:
                case IRBranchCondition.GreaterOrEqualUnsigned:
                case IRBranchCondition.Greater:
                case IRBranchCondition.GreaterUnsigned:
                case IRBranchCondition.LessOrEqual:
                case IRBranchCondition.LessOrEqualUnsigned:
                case IRBranchCondition.Less:
                case IRBranchCondition.LessUnsigned:
                case IRBranchCondition.NotEqualUnsigned:
                    {
                        IRStackObject value2 = pStack.Pop();
                        IRStackObject value1 = pStack.Pop();
                        Sources.Add(new IRLinearizedTarget(value1.LinearizedTarget));
                        Sources.Add(new IRLinearizedTarget(value2.LinearizedTarget));
                        break;
                    }
            }
        }
    }
}
