using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public sealed class IRStoreObjectInstruction : IRInstruction
    {
        public IRType Type { get; private set; }

        public IRStoreObjectInstruction(IRType pType) : base(IROpcode.StoreObject) { Type = pType; }

        public override void Linearize(Stack<IRStackObject> pStack)
        {
            Sources.Add(new IRLinearizedLocation(pStack.Pop().LinearizedTarget));

            Destination = new IRLinearizedLocation(IRLinearizedLocationType.Indirect);
            Destination.Indirect.Type = Type;
            Destination.Indirect.AddressLocation = new IRLinearizedLocation(pStack.Pop().LinearizedTarget);
        }
    }
}
