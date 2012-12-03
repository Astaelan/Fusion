using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public sealed class IRStoreParameterInstruction : IRInstruction
    {
        public uint ParameterIndex = 0;

        public IRStoreParameterInstruction(uint pParameterIndex) : base(IROpcode.StoreParameter) { ParameterIndex = pParameterIndex; }

        public override void Linearize(Stack<IRStackObject> pStack)
        {
            Sources.Add(new IRLinearizedLocation(pStack.Pop().LinearizedTarget));

            Destination = new IRLinearizedLocation(IRLinearizedLocationType.Parameter);
            Destination.Parameter.ParameterIndex = ParameterIndex;
        }

        public override IRInstruction Clone(IRMethod newMethod)
        {
            throw new NotImplementedException();
        }
    }
}
