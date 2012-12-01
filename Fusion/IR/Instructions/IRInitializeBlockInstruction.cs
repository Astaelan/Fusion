using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public sealed class IRInitializeBlockInstruction : IRInstruction
    {
        public IRInitializeBlockInstruction() : base(IROpcode.InitializeBlock) { }

        public override void Linearize(Stack<IRStackObject> pStack)
        {
            IRStackObject size = pStack.Pop();
            IRStackObject srcAddr = pStack.Pop();
            IRStackObject destAddr = pStack.Pop();
            Sources.Add(new IRLinearizedTarget(destAddr.LinearizedTarget));
            Sources.Add(new IRLinearizedTarget(srcAddr.LinearizedTarget));
            Sources.Add(new IRLinearizedTarget(size.LinearizedTarget));
        }
    }
}
