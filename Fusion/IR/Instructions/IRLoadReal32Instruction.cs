using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public sealed class IRLoadReal32Instruction : IRInstruction
    {
        public float Value { get; private set; }

        public IRLoadReal32Instruction(float pValue) : base(IROpcode.LoadReal32) { Value = pValue; }

        public override void Linearize(Stack<IRStackObject> pStack)
        {
            IRLinearizedLocation value = new IRLinearizedLocation(IRLinearizedLocationType.ConstantR4);
            value.ConstantR4.Value = Value;
            Sources.Add(value);

            IRStackObject result = new IRStackObject();
            result.Type = Method.Assembly.AppDomain.System_Single;
            result.LinearizedTarget = new IRLinearizedLocation(IRLinearizedLocationType.Local);
            result.LinearizedTarget.Local.LocalIndex = AddLinearizedLocal(Method.Assembly.AppDomain.System_Single);
            Destination = new IRLinearizedLocation(result.LinearizedTarget);
            pStack.Push(result);
        }
    }
}
