using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public sealed class IRLoadArrayElementInstruction : IRInstruction
    {
        public IRType Type { get; private set; }

        public IRLoadArrayElementInstruction(IRType pType) : base(IROpcode.LoadArrayElement) { Type = pType; }

        public override void Linearize(Stack<IRStackObject> pStack)
        {
            IRLinearizedLocation source = new IRLinearizedLocation(IRLinearizedLocationType.ArrayElement);
            source.ArrayElement.IndexLocation = new IRLinearizedLocation(pStack.Pop().LinearizedTarget);
            source.ArrayElement.ArrayLocation = new IRLinearizedLocation(pStack.Pop().LinearizedTarget);
            source.ArrayElement.ElementType = Type;
            Sources.Add(source);

            IRStackObject result = new IRStackObject();
            result.Type = Type;
            result.LinearizedTarget = new IRLinearizedLocation(IRLinearizedLocationType.Local);
            result.LinearizedTarget.Local.LocalIndex = AddLinearizedLocal(Type);
            Destination = new IRLinearizedLocation(result.LinearizedTarget);
            pStack.Push(result);
        }

        public override IRInstruction Clone(IRMethod newMethod)
        {
            throw new NotImplementedException();
        }

        public override IRInstruction Transform() { return new IRMoveInstruction(this); }
    }
}
