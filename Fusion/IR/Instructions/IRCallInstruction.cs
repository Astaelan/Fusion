using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public sealed class IRCallInstruction : IRInstruction
    {
        public IRMethod Target { get; private set; }
        public bool Virtual { get; protected set; }

        public IRCallInstruction(IRMethod pTarget, bool pVirtual) : base(IROpcode.Call)
        {
            Target = pTarget;
            Virtual = pVirtual;
        }

        public override void Linearize(Stack<IRStackObject> pStack)
        {
            for (int count = 0; count < Target.Parameters.Count; ++count) Sources.Add(new IRLinearizedTarget(pStack.Pop().LinearizedTarget));

            if (Target.ReturnType != null)
            {
                IRStackObject returned = new IRStackObject();
                returned.Type = Target.ReturnType;
                returned.LinearizedTarget = new IRLinearizedTarget(IRLinearizedTargetType.Local);
                returned.LinearizedTarget.LocalVariable.LocalVariableIndex = AddLinearizedLocal(Target.ReturnType);
                Destination = new IRLinearizedTarget(returned.LinearizedTarget);
                pStack.Push(returned);
            }
        }
    }
}
