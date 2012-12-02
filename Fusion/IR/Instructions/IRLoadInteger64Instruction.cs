using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public sealed class IRLoadInteger64Instruction : IRInstruction
    {
        public long Value { get; private set; }

        public IRLoadInteger64Instruction(long pValue) : base(IROpcode.LoadInteger64) { Value = pValue; }

        public override void Linearize(Stack<IRStackObject> pStack)
        {
            IRLinearizedLocation value = new IRLinearizedLocation(IRLinearizedLocationType.ConstantI8);
            value.ConstantI8.Value = Value;
            Sources.Add(value);

            IRStackObject result = new IRStackObject();
            result.Type = Method.Assembly.AppDomain.System_Int64;
            result.LinearizedTarget = new IRLinearizedLocation(IRLinearizedLocationType.Local);
            result.LinearizedTarget.Local.LocalIndex = AddLinearizedLocal(Method.Assembly.AppDomain.System_Int64);
            Destination = new IRLinearizedLocation(result.LinearizedTarget);
            pStack.Push(result);
        }
    }
}
