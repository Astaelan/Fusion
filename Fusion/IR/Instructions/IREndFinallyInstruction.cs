using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public sealed class IREndFinallyInstruction : IRInstruction
    {
        public IREndFinallyInstruction() : base(IROpcode.EndFinally) { }

        public override void Linearize(Stack<IRStackObject> pStack) { }

        public override IRInstruction Clone(IRMethod pNewMethod) { return CopyTo(new IREndFinallyInstruction(), pNewMethod); }
    }
}
