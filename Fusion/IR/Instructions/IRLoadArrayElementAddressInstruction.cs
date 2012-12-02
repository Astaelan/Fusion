using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public sealed class IRLoadArrayElementAddressInstruction : IRInstruction
    {
        public IRType Type { get; private set; }

        public IRLoadArrayElementAddressInstruction(IRType pType) : base(IROpcode.LoadArrayElementAddress) { Type = pType; }

        public override void Linearize(Stack<IRStackObject> pStack)
        {
            IRLinearizedLocation source = new IRLinearizedLocation(IRLinearizedLocationType.ArrayElementAddress);
            source.ArrayElementAddress.IndexLocation = new IRLinearizedLocation(pStack.Pop().LinearizedTarget);
            source.ArrayElementAddress.ArrayLocation = new IRLinearizedLocation(pStack.Pop().LinearizedTarget);
            source.ArrayElementAddress.ElementType = Type;
            Sources.Add(source);

            IRStackObject result = new IRStackObject();
            result.Type = Method.Assembly.AppDomain.System_IntPtr;
            result.LinearizedTarget = new IRLinearizedLocation(IRLinearizedLocationType.Local);
            result.LinearizedTarget.Local.LocalIndex = AddLinearizedLocal(Method.Assembly.AppDomain.System_IntPtr);
            Destination = new IRLinearizedLocation(result.LinearizedTarget);
            pStack.Push(result);
        }
    }
}
