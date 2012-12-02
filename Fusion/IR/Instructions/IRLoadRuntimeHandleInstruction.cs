using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public sealed class IRLoadRuntimeHandleInstruction : IRInstruction
    {
        public IRType TargetType { get; private set; }
        public IRMethod TargetMethod { get; private set; }
        public IRField TargetField { get; private set; }

        public IRLoadRuntimeHandleInstruction(IRType pTargetType, IRMethod pTargetMethod, IRField pTargetField) : base(IROpcode.LoadRuntimeHandle)
        {
            TargetType = pTargetType;
            TargetMethod = pTargetMethod;
            TargetField = pTargetField;
        }

        public override void Linearize(Stack<IRStackObject> pStack)
        {
            IRType handleType = null;
            if (TargetType != null) handleType = Method.Assembly.AppDomain.System_RuntimeTypeHandle;
            else if (TargetMethod != null) handleType = Method.Assembly.AppDomain.System_RuntimeMethodHandle;
            else if (TargetField != null) handleType = Method.Assembly.AppDomain.System_RuntimeFieldHandle;
            else throw new NullReferenceException();

            IRStackObject result = new IRStackObject();
            result.Type = handleType;
            result.LinearizedTarget = new IRLinearizedLocation(IRLinearizedLocationType.Local);
            result.LinearizedTarget.Local.LocalIndex = AddLinearizedLocal(handleType);
            Destination = new IRLinearizedLocation(result.LinearizedTarget);
            pStack.Push(result);
        }
    }
}
