using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public sealed class IRSizeOfInstruction : IRInstruction
    {
        public IRType Type { get; private set; }

        public IRSizeOfInstruction(IRType pType) : base(IROpcode.SizeOf) { Type = pType; }

        public override void Linearize(Stack<IRStackObject> pStack)
        {
            IRLinearizedLocation value = new IRLinearizedLocation(IRLinearizedLocationType.SizeOf);
            value.SizeOf.Type = Type;
            Sources.Add(value);

            IRStackObject result = new IRStackObject();
            result.Type = Method.Assembly.AppDomain.System_UInt32;
            result.LinearizedTarget = new IRLinearizedLocation(IRLinearizedLocationType.Local);
            result.LinearizedTarget.Local.LocalIndex = AddLinearizedLocal(Method.Assembly.AppDomain.System_UInt32);
            Destination = new IRLinearizedLocation(result.LinearizedTarget);
            pStack.Push(result);
        }
    }
}
