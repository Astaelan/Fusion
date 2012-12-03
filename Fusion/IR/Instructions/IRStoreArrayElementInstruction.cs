using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public sealed class IRStoreArrayElementInstruction : IRInstruction
    {
        public IRType Type { get; private set; }

        public IRStoreArrayElementInstruction(IRType pType) : base(IROpcode.StoreArrayElement) { Type = pType; }

        public override void Linearize(Stack<IRStackObject> pStack)
        {
            Sources.Add(new IRLinearizedLocation(pStack.Pop().LinearizedTarget));

            Destination = new IRLinearizedLocation(IRLinearizedLocationType.ArrayElement);
            Destination.ArrayElement.ElementType = Type;
            Destination.ArrayElement.IndexLocation = new IRLinearizedLocation(pStack.Pop().LinearizedTarget);
            Destination.ArrayElement.ArrayLocation = new IRLinearizedLocation(pStack.Pop().LinearizedTarget);
        }
    }
}
