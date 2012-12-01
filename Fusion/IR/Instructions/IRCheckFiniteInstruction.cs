using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public sealed class IRCheckFiniteInstruction : IRInstruction
    {
        public IRCheckFiniteInstruction() : base(IROpcode.CheckFinite) { }

        public override void Linearize(Stack<IRStackObject> pStack)
        {
            Sources.Add(new IRLinearizedTarget(pStack.Pop().LinearizedTarget));

            IRStackObject result = new IRStackObject();
            result.Type = Method.Assembly.AppDomain.System_Double;
            result.LinearizedTarget = new IRLinearizedTarget(IRLinearizedTargetType.Local);
            result.LinearizedTarget.LocalVariable.LocalVariableIndex = AddLinearizedLocal(Method.Assembly.AppDomain.System_Double);
            Destination = new IRLinearizedTarget(result.LinearizedTarget);
            pStack.Push(result);
        }
    }
}
