﻿using System;
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
        public List<IRLinearizedLocation> Sources = new List<IRLinearizedLocation>();
        public IRLinearizedLocation Destination = null;

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


        /// <summary>
        /// This does a shallow copy of all of the members of the
        /// abstract IRInstruction class to the specified instruction.
        /// </summary>
        /// <param name="i">The instruction to copy to.</param>
        /// <param name="newMethod">The method this instruction will be added to.</param>
        /// <returns><see cref="i"/></returns>
        protected IRInstruction CopyTo(IRInstruction i, IRMethod newMethod)
        {
            i.ILOffset = this.ILOffset;
            i.IRIndex = this.IRIndex;
            i.Opcode = this.Opcode;
            i.Method = newMethod;
            i.Destination = this.Destination.Clone();
            foreach (IRLinearizedLocation t in this.Sources)
            {
                i.Sources.Add(t.Clone());
            }
            return i;
        }

        public abstract IRInstruction Clone(IRMethod newMethod);
        public virtual bool Resolved { get { return true; } }

        public virtual void Resolve(GenericParameterCollection typeParams, GenericParameterCollection methodParams)
        {
            if (Destination != null)
                Destination.Resolve(typeParams, methodParams);
            Sources.ForEach(s => s.Resolve(typeParams, methodParams));
        }

        public virtual IRInstruction Transform() { return this; }
    }
}
