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
			Destination.ArrayElement.IndexLocation = new IRLinearizedLocation(pStack.Pop().LinearizedTarget);
			var arraySource = pStack.Pop();
			Destination.ArrayElement.ArrayLocation = new IRLinearizedLocation(arraySource.LinearizedTarget);
			if (Type == null)
			{
				Type = arraySource.Type.ArrayType;
			}
			Destination.ArrayElement.ElementType = Type;
        }

        public override IRInstruction Clone(IRMethod pNewMethod) { return CopyTo(new IRStoreArrayElementInstruction(Type), pNewMethod); }

        public override IRInstruction Transform() { return new IRMoveInstruction(this); }
    }
}
