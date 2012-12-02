using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public sealed class IRCallInstruction : IRInstruction
    {
        public IRMethod Target { get; private set; }
        public bool Virtual { get; private set; }

        public IRCallInstruction(IRMethod pTarget, bool pVirtual) : base(IROpcode.Call)
        {
            Target = pTarget;
            Virtual = pVirtual;
        }

        public override void Linearize(Stack<IRStackObject> pStack)
        {
            for (int count = 0; count < Target.Parameters.Count; ++count) Sources.Add(new IRLinearizedLocation(pStack.Pop().LinearizedTarget));

            if (Target.ReturnType != null)
            {
                IRStackObject returned = new IRStackObject();
                returned.Type = Target.ReturnType;
                returned.LinearizedTarget = new IRLinearizedLocation(IRLinearizedLocationType.Local);
                returned.LinearizedTarget.Local.LocalIndex = AddLinearizedLocal(Target.ReturnType);
                Destination = new IRLinearizedLocation(returned.LinearizedTarget);
                pStack.Push(returned);
            }
        }
    }
}
