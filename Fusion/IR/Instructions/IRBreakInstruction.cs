using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public sealed class IRBreakInstruction : IRInstruction
    {
        public IRBreakInstruction() : base(IROpcode.Break) { }

        public override void Linearize(Stack<IRStackObject> pStack) { }

        public override IRInstruction Clone(IRMethod newMethod)
        {
            throw new NotImplementedException();
        }
    }
}
