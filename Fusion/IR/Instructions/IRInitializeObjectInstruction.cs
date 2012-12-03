using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public sealed class IRInitializeObjectInstruction : IRInstruction
    {
        public IRType Type { get; private set; }

        public IRInitializeObjectInstruction(IRType pType) : base(IROpcode.InitializeObject) { Type = pType; }

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
