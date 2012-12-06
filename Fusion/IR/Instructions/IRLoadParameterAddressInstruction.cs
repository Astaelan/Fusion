using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public sealed class IRLoadParameterAddressInstruction : IRInstruction
    {
        public uint ParameterIndex { get; private set; }

        public IRLoadParameterAddressInstruction(uint pParameterIndex) : base(IROpcode.LoadParameterAddress) { ParameterIndex = pParameterIndex; }

        public override void Linearize(Stack<IRStackObject> pStack)
        {
            IRLinearizedLocation source = new IRLinearizedLocation(IRLinearizedLocationType.ParameterAddress);
            source.ParameterAddress.ParameterIndex = ParameterIndex;
            Sources.Add(source);

            IRStackObject result = new IRStackObject();
            result.Type = Method.Assembly.AppDomain.System_IntPtr;
            result.LinearizedTarget = new IRLinearizedLocation(IRLinearizedLocationType.Local);
            result.LinearizedTarget.Local.LocalIndex = AddLinearizedLocal(pStack, Method.Assembly.AppDomain.System_IntPtr);
            Destination = new IRLinearizedLocation(result.LinearizedTarget);
            pStack.Push(result);
        }

        public override IRInstruction Clone(IRMethod pNewMethod) { return CopyTo(new IRLoadParameterAddressInstruction(ParameterIndex), pNewMethod); }

        public override IRInstruction Transform() { return new IRMoveInstruction(this); }
    }
}
