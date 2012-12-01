using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public sealed class IRJumpInstruction : IRInstruction
    {
        public IRMethod Target { get; private set; }

        public IRJumpInstruction(IRMethod pTarget) : base(IROpcode.Jump) { Target = pTarget; }

        public override void Linearize(Stack<IRStackObject> pStack)
        {
            if (pStack.Count > 0) throw new OverflowException();
        }
    }
}
