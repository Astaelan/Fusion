using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public sealed class IRLoadLocalInstruction : IRInstruction
    {
        public uint LocalIndex { get; private set; }

        public IRLoadLocalInstruction(uint pLocalIndex) : base(IROpcode.LoadLocal) { LocalIndex = pLocalIndex; }

        public override void Linearize(Stack<IRStackObject> pStack)
        {
            IRLinearizedLocation source = new IRLinearizedLocation(IRLinearizedLocationType.Local);
            source.Local.LocalIndex = LocalIndex;
            Sources.Add(source);

            IRStackObject result = new IRStackObject();
            result.Type = Method.Locals[(int)LocalIndex].Type;
            result.LinearizedTarget = new IRLinearizedLocation(IRLinearizedLocationType.Local);
            result.LinearizedTarget.Local.LocalIndex = AddLinearizedLocal(Method.Locals[(int)LocalIndex].Type);
            Destination = new IRLinearizedLocation(result.LinearizedTarget);
            pStack.Push(result);
        }
    }
}
