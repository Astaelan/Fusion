using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public sealed class IRPopInstruction : IRInstruction
    {
        public IRPopInstruction() : base(IROpcode.Pop) { }

        public override void Linearize(Stack<IRStackObject> pStack)
        {
            Sources.Add(new IRLinearizedLocation(pStack.Pop().LinearizedTarget));
        }

        public override IRInstruction Clone(IRMethod newMethod)
        {
            throw new NotImplementedException();
        }
    }
}
