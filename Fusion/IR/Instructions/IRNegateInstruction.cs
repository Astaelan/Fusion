using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public sealed class IRNegateInstruction : IRInstruction
    {
        public IRNegateInstruction() : base(IROpcode.Negate) { }

        public override void Linearize(Stack<IRStackObject> pStack)
        {
            IRStackObject value = pStack.Pop();
            Sources.Add(new IRLinearizedLocation(value.LinearizedTarget));

            IRStackObject result = new IRStackObject();
            result.Type = value.Type;
            result.LinearizedTarget = new IRLinearizedLocation(IRLinearizedLocationType.Local);
            result.LinearizedTarget.Local.LocalIndex = AddLinearizedLocal(value.Type);
            Destination = new IRLinearizedLocation(result.LinearizedTarget);
            pStack.Push(result);
        }
    }
}
