using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public sealed class IRAddInstruction : IRInstruction
    {
        private IROverflowType mOverflowType = IROverflowType.None;
        public IROverflowType OverflowType 
        {
            get { return mOverflowType; }
            private set { mOverflowType = value; }
        }

        public IRAddInstruction(IROverflowType pOverflowType) : base(IROpcode.Add) { OverflowType = pOverflowType; }

        public override void Linearize(Stack<IRStackObject> pStack)
        {
            IRStackObject value2 = pStack.Pop();
            IRStackObject value1 = pStack.Pop();

            Sources.Add(new IRLinearizedTarget(value1.LinearizedTarget));
            Sources.Add(new IRLinearizedTarget(value2.LinearizedTarget));

            IRType resultType = Method.Assembly.AppDomain.BinaryNumericResult(value1.Type, value2.Type);
            IRStackObject result = new IRStackObject();
            result.Type = resultType;
            result.LinearizedTarget = new IRLinearizedTarget(IRLinearizedTargetType.Local);
            result.LinearizedTarget.LocalVariable.LocalVariableIndex = AddLinearizedLocal(resultType);
            Destination = new IRLinearizedTarget(result.LinearizedTarget);
            pStack.Push(result);
        }

        public override IRInstruction Clone(IRMethod newMethod)
        {
            return CopyTo(new IRAddInstruction(this.OverflowType), newMethod);
        }
    }
}
