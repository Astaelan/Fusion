using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public sealed class IRBoxInstruction : IRInstruction
    {
        public IRType Type { get; private set; }

        public IRBoxInstruction(IRType pType) : base(IROpcode.Box) { Type = pType; }

        public override void Linearize(Stack<IRStackObject> pStack)
        {
            Sources.Add(new IRLinearizedTarget(pStack.Pop().LinearizedTarget));

            IRStackObject result = new IRStackObject();
            result.Type = Method.Assembly.AppDomain.System_Object;
            result.BoxedType = Type;
            result.LinearizedTarget = new IRLinearizedTarget(IRLinearizedTargetType.Local);
            result.LinearizedTarget.LocalVariable.LocalVariableIndex = AddLinearizedLocal(Method.Assembly.AppDomain.System_Object);
            Destination = new IRLinearizedTarget(result.LinearizedTarget);
            pStack.Push(result);
        }
    }
}
