using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public sealed class IRStoreLocalInstruction : IRInstruction
    {
        public uint LocalIndex { get; private set; }

        public IRStoreLocalInstruction(uint pLocalIndex) : base(IROpcode.StoreLocal) { LocalIndex = pLocalIndex; }

        public override void Linearize(Stack<IRStackObject> pStack)
        {
            Sources.Add(new IRLinearizedLocation(pStack.Pop().LinearizedTarget));

            Destination = new IRLinearizedLocation(IRLinearizedLocationType.Local);
            Destination.Local.LocalIndex = LocalIndex;
        }

        public override IRInstruction Clone(IRMethod newMethod)
        {
            throw new NotImplementedException();
        }
    }
}
