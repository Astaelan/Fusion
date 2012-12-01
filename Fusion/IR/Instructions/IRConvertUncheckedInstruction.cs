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
            Sources.Add(new IRLinearizedTarget(pStack.Pop().LinearizedTarget));

            IRStackObject result = new IRStackObject();
            result.Type = Type;
            result.LinearizedTarget = new IRLinearizedTarget(IRLinearizedTargetType.Local);
            result.LinearizedTarget.LocalVariable.LocalVariableIndex = AddLinearizedLocal(Type);
            Destination = new IRLinearizedTarget(result.LinearizedTarget);
            pStack.Push(result);
        }
    }
}
