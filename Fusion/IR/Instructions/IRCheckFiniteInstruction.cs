using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public sealed class IRCheckFiniteInstruction : IRInstruction
    {
        public IRCheckFiniteInstruction() : base(IROpcode.CheckFinite) { }

        public override void Linearize(Stack<IRStackObject> pStack)
        {
            Sources.Add(new IRLinearizedLocation(pStack.Pop().LinearizedTarget));

            IRStackObject result = new IRStackObject();
            result.Type = Method.Assembly.AppDomain.System_Double;
            result.LinearizedTarget = new IRLinearizedLocation(IRLinearizedLocationType.Local);
            result.LinearizedTarget.Local.LocalIndex = AddLinearizedLocal(Method.Assembly.AppDomain.System_Double);
            Destination = new IRLinearizedLocation(result.LinearizedTarget);
            pStack.Push(result);
        }
    }
}
