using System;
using System.Collections.Generic;
using Fusion.CLI.Metadata;

namespace Fusion.IR.Instructions
{
    public sealed class IRCastInstruction : IRInstruction
    {
        public IRType Type { get; private set; }
        public bool ThrowExceptionOnFailure { get; private set; }

        public IRCastInstruction(IRType pType, bool pThrowExceptionOnFailure) : base(IROpcode.Cast)
        {
            Type = pType;
            ThrowExceptionOnFailure = pThrowExceptionOnFailure;
        }

        public override void Linearize(Stack<IRStackObject> pStack)
        {
            Sources.Add(new IRLinearizedTarget(pStack.Pop().LinearizedTarget));

            IRStackObject result = new IRStackObject();
            result.Type = Type;
            result.LinearizedTarget = new IRLinearizedTarget(IRLinearizedTargetType.Local);
            result.LinearizedTarget.LocalVariable.LocalVariableIndex = AddLinearizedLocal(Type);
            Destination = new IRLinearizedTarget(result.LinearizedTarget);
            pStack.Push(result);
        }
    }
}
