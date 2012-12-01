using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public sealed class IRCopyBlockInstruction : IRInstruction
    {
        public IRCopyBlockInstruction() : base(IROpcode.CopyBlock) { }

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
