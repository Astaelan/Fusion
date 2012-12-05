using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public sealed class IRStoreIndirectInstruction : IRInstruction
    {
        public IRType Type { get; private set; }

        public IRStoreIndirectInstruction(IRType pType) : base(IROpcode.StoreIndirect) { Type = pType; }

        public override void Linearize(Stack<IRStackObject> pStack)
        {
            Sources.Add(new IRLinearizedLocation(pStack.Pop().LinearizedTarget));

            Destination = new IRLinearizedLocation(IRLinearizedLocationType.Indirect);
            Destination.Indirect.Type = Type;
            Destination.Indirect.AddressLocation = new IRLinearizedLocation(pStack.Pop().LinearizedTarget);
        }

        public override IRInstruction Clone(IRMethod newMethod)
        {
            throw new NotImplementedException();
        }

        public override IRInstruction Transform() { return new IRMoveInstruction(this); }
    }
}
