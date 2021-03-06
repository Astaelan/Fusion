using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public sealed class IRLoadReal64Instruction : IRInstruction
    {
        public double Value { get; private set; }

        public IRLoadReal64Instruction(double pValue) : base(IROpcode.LoadReal64) { Value = pValue; }

        public override void Linearize(Stack<IRStackObject> pStack)
        {
            IRLinearizedLocation value = new IRLinearizedLocation(IRLinearizedLocationType.ConstantR8);
            value.ConstantR8.Value = Value;
            Sources.Add(value);

            IRStackObject result = new IRStackObject();
            result.Type = ParentMethod.Assembly.AppDomain.System_Double;
            result.LinearizedTarget = new IRLinearizedLocation(IRLinearizedLocationType.Local);
            result.LinearizedTarget.Local.LocalIndex = AddLinearizedLocal(pStack, ParentMethod.Assembly.AppDomain.System_Double);
            Destination = new IRLinearizedLocation(result.LinearizedTarget);
            pStack.Push(result);
        }

        public override IRInstruction Clone(IRMethod pNewMethod) { return CopyTo(new IRLoadReal64Instruction(Value), pNewMethod); }

        public override IRInstruction Transform() { return new IRMoveInstruction(this); }
    }
}
