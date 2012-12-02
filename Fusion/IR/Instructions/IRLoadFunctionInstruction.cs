using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public sealed class IRLoadFunctionInstruction : IRInstruction
    {
        public IRMethod Target { get; private set; }
        public bool Virtual { get; protected set; }

        public IRLoadFunctionInstruction(IRMethod pTarget, bool pVirtual) : base(IROpcode.LoadFunction)
        {
            Target = pTarget;
            Virtual = pVirtual;
        }

        public override void Linearize(Stack<IRStackObject> pStack)
        {
            IRStackObject result = new IRStackObject();
            result.Type = Method.Assembly.AppDomain.System_IntPtr;
            result.LinearizedTarget = new IRLinearizedLocation(IRLinearizedLocationType.Local);
            result.LinearizedTarget.Local.LocalIndex = AddLinearizedLocal(Method.Assembly.AppDomain.System_IntPtr);
            Destination = new IRLinearizedLocation(result.LinearizedTarget);
            pStack.Push(result);
        }
    }
}
