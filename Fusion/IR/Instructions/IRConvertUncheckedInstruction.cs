using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public sealed class IRConvertUncheckedInstruction : IRInstruction
    {
        public IRType Type { get; private set; }

        public IRConvertUncheckedInstruction(IRType pType) : base(IROpcode.ConvertUnchecked) { Type = pType; }

        public override void Linearize(Stack<IRStackObject> pStack)
        {
            Sources.Add(new IRLinearizedLocation(pStack.Pop().LinearizedTarget));

            IRStackObject result = new IRStackObject();
            result.Type = Type;
            result.LinearizedTarget = new IRLinearizedLocation(IRLinearizedLocationType.Local);
            result.LinearizedTarget.Local.LocalIndex = AddLinearizedLocal(pStack, Type);
            Destination = new IRLinearizedLocation(result.LinearizedTarget);
            pStack.Push(result);
        }

        public override IRInstruction Clone(IRMethod pNewMethod) { return CopyTo(new IRConvertUncheckedInstruction(Type), pNewMethod); }
    }
}
