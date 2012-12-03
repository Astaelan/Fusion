using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public sealed class IRSwitchInstruction : IRInstruction
    {
        public uint[] TargetILOffsets = null;

        public IRSwitchInstruction(uint[] pTargetILOffsets) : base(IROpcode.Switch) { TargetILOffsets = pTargetILOffsets; }

        public override void Linearize(Stack<IRStackObject> pStack)
        {
            Sources.Add(new IRLinearizedLocation(pStack.Pop().LinearizedTarget));
        }

        public override IRInstruction Clone(IRMethod newMethod)
        {
            throw new NotImplementedException();
        }
    }
}
