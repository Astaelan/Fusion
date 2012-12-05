using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public sealed class IRLoadInteger32Instruction : IRInstruction
    {
        public int Value { get; private set; }

        public IRLoadInteger32Instruction(int pValue) : base(IROpcode.LoadInteger32) { Value = pValue; }

        public override void Linearize(Stack<IRStackObject> pStack)
        {
            IRLinearizedLocation value = new IRLinearizedLocation(IRLinearizedLocationType.ConstantI4);
            value.ConstantI4.Value = Value;
            Sources.Add(value);

            IRStackObject result = new IRStackObject();
            result.Type = Method.Assembly.AppDomain.System_Int32;
            result.LinearizedTarget = new IRLinearizedLocation(IRLinearizedLocationType.Local);
            result.LinearizedTarget.Local.LocalIndex = AddLinearizedLocal(Method.Assembly.AppDomain.System_Int32);
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
