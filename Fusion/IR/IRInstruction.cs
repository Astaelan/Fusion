using System;
using System.Collections.Generic;

namespace Fusion.IR
{
    public abstract class IRInstruction
    {
        public uint ILOffset = 0;
        public uint IRIndex = 0;
        public IROpcode Opcode = IROpcode.Nop;
        public IRMethod Method = null;

        // Instruction Linearization
        public List<IRLinearizedTarget> Sources = new List<IRLinearizedTarget>();
        public IRLinearizedTarget Destination = null;

        protected IRInstruction(IROpcode pOpcode)
        {
            Opcode = pOpcode;
        }

        public uint AddLinearizedLocal(IRType pType)
        {
            IRLocal local = new IRLocal(Method.Assembly);
            local.ParentMethod = Method;
            local.Type = pType;
            Method.Locals.Add(local);
            return (uint)(Method.Locals.Count - 1);
        }

        public abstract void Linearize(Stack<IRStackObject> pStack);
    }
}
