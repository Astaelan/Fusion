using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public sealed class IRReturnInstruction : IRInstruction
    {
        public IRReturnInstruction() : base(IROpcode.Return) { }

        public override void Linearize(Stack<IRStackObject> pStack)
        {
            if (Method.ReturnType != null) Sources.Add(new IRLinearizedLocation(pStack.Pop().LinearizedTarget));
        }

        public override IRInstruction Clone(IRMethod newMethod)
        {
            throw new NotImplementedException();
        }
    }
}
