using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public sealed class IRLoadLocalAddressInstruction : IRInstruction
    {
        public uint LocalIndex { get; private set; }

        public IRLoadLocalAddressInstruction(uint pLocalIndex) : base(IROpcode.LoadLocalAddress) { LocalIndex = pLocalIndex; }

        public override void Linearize(Stack<IRStackObject> pStack)
        {
            IRLinearizedLocation source = new IRLinearizedLocation(IRLinearizedLocationType.LocalAddress);
            source.LocalAddress.LocalIndex = LocalIndex;
            Sources.Add(source);

            IRStackObject result = new IRStackObject();
            result.Type = Method.Assembly.AppDomain.System_IntPtr;
            result.LinearizedTarget = new IRLinearizedLocation(IRLinearizedLocationType.Local);
            result.LinearizedTarget.Local.LocalIndex = AddLinearizedLocal(Method.Assembly.AppDomain.System_IntPtr);
            Destination = new IRLinearizedLocation(result.LinearizedTarget);
            pStack.Push(result);
        }

        public override IRInstruction Clone(IRMethod newMethod)
        {
            throw new NotImplementedException();
        }

        public override IRInstruction Transform() { return new IRMoveInstruction(this); }
    }
}
