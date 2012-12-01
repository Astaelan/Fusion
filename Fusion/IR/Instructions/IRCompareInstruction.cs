using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public sealed class IRCompareInstruction : IRInstruction
    {
        private IRCompareCondition mCompareCondition = IRCompareCondition.Equal;
        public IRCompareCondition CompareCondition
        {
            get { return mCompareCondition; }
            private set { mCompareCondition = value; }
        }

        public IRCompareInstruction(IRCompareCondition pCompareCondition) : base(IROpcode.Compare) { CompareCondition = pCompareCondition; }

        public override void Linearize(Stack<IRStackObject> pStack)
        {
            IRStackObject value2 = pStack.Pop();
            IRStackObject value1 = pStack.Pop();

            Sources.Add(new IRLinearizedTarget(value1.LinearizedTarget));
            Sources.Add(new IRLinearizedTarget(value2.LinearizedTarget));

            IRStackObject result = new IRStackObject();
            result.Type = Method.Assembly.AppDomain.System_Int32;
            result.LinearizedTarget = new IRLinearizedTarget(IRLinearizedTargetType.Local);
            result.LinearizedTarget.LocalVariable.LocalVariableIndex = AddLinearizedLocal(Method.Assembly.AppDomain.System_Int32);
            Destination = new IRLinearizedTarget(result.LinearizedTarget);
            pStack.Push(result);
        }
    }
}
