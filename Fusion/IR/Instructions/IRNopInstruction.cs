using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public sealed class IRNopInstruction : IRInstruction
    {
        public bool ForceEmit { get; private set; }

        public IRNopInstruction(bool pForceEmit) : base(IROpcode.Nop) { ForceEmit = pForceEmit; }

        public override void Linearize(Stack<IRStackObject> pStack) { }

        public override IRInstruction Clone(IRMethod newMethod)
        {
            throw new NotImplementedException();
        }
    }
}
