using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public sealed class IRLeaveInstruction : IRInstruction
    {
        public uint TargetILOffset { get; private set; }
        public IRInstruction TargetIRInstruction { get; set; }

        public IRLeaveInstruction(uint pTargetILOffset) : base(IROpcode.Leave) { TargetILOffset = pTargetILOffset; }

        public override void Linearize(Stack<IRStackObject> pStack)
        {
            pStack.Clear();
        }

        public override IRInstruction Clone(IRMethod newMethod)
        {
            throw new NotImplementedException();
        }
    }
}
