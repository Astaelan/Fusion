using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public sealed class IRDuplicateInstruction : IRInstruction
    {
        public IRDuplicateInstruction() : base(IROpcode.Duplicate) { }

        public override void Linearize(Stack<IRStackObject> pStack)
        {
            IRStackObject value = pStack.Peek();
            Sources.Add(new IRLinearizedTarget(value.LinearizedTarget));

            IRStackObject result = new IRStackObject();
            result.Type = value.Type;
            result.BoxedType = value.BoxedType;
            result.LinearizedTarget = new IRLinearizedTarget(IRLinearizedTargetType.Local);
            result.LinearizedTarget.LocalVariable.LocalVariableIndex = AddLinearizedLocal(value.Type);
            Destination = new IRLinearizedTarget(result.LinearizedTarget);
            pStack.Push(result);
        }
    }
}
