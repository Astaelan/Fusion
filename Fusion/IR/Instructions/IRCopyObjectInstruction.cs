using System;
using System.Collections.Generic;
using Fusion.CLI.Metadata;

namespace Fusion.IR.Instructions
{
    public sealed class IRCopyObjectInstruction : IRInstruction
    {
        public IRType Type { get; private set; }

        public IRCopyObjectInstruction(IRType pType) : base(IROpcode.CopyObject) { Type = pType; }

        public override void Linearize(Stack<IRStackObject> pStack)
        {
            IRStackObject srcAddr = pStack.Pop();
            IRStackObject destAddr = pStack.Pop();
            Sources.Add(new IRLinearizedTarget(destAddr.LinearizedTarget));
            Sources.Add(new IRLinearizedTarget(srcAddr.LinearizedTarget));
        }
    }
}
