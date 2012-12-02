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
            Sources.Add(new IRLinearizedLocation(pStack.Pop().LinearizedTarget));

            IRStackObject result = new IRStackObject();
            result.Type = Method.Assembly.AppDomain.System_Object;
            result.BoxedType = Type;
            result.LinearizedTarget = new IRLinearizedLocation(IRLinearizedLocationType.Local);
            result.LinearizedTarget.Local.LocalIndex = AddLinearizedLocal(Method.Assembly.AppDomain.System_Object);
            Destination = new IRLinearizedLocation(result.LinearizedTarget);
            pStack.Push(result);
        }
    }
}
