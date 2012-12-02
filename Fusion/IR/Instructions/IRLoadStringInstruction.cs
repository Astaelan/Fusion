using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public sealed class IRLoadStringInstruction : IRInstruction
    {
        public string Value { get; private set; }

        public IRLoadStringInstruction(string pValue) : base(IROpcode.LoadString) { Value = pValue; }

        public override void Linearize(Stack<IRStackObject> pStack)
        {
            IRLinearizedLocation value = new IRLinearizedLocation(IRLinearizedLocationType.String);
            value.String.Value = Value;
            Sources.Add(value);

            IRStackObject result = new IRStackObject();
            result.Type = Method.Assembly.AppDomain.System_String;
            result.LinearizedTarget = new IRLinearizedLocation(IRLinearizedLocationType.Local);
            result.LinearizedTarget.Local.LocalIndex = AddLinearizedLocal(Method.Assembly.AppDomain.System_String);
            Destination = new IRLinearizedLocation(result.LinearizedTarget);
            pStack.Push(result);
        }
    }
}
