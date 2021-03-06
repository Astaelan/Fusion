﻿using System;
using System.Collections.Generic;

namespace Fusion.IR.Instructions
{
    public sealed class IRStoreStaticFieldInstruction : IRInstruction
    {
        public IRField Field = null;

        public IRStoreStaticFieldInstruction(IRField pField) : base(IROpcode.StoreStaticField) { Field = pField; }

        public override void Linearize(Stack<IRStackObject> pStack)
        {
            Sources.Add(new IRLinearizedLocation(pStack.Pop().LinearizedTarget));

            Destination = new IRLinearizedLocation(IRLinearizedLocationType.StaticField);
            Destination.StaticField.Field = Field;
        }

        public override IRInstruction Clone(IRMethod pNewMethod) { return CopyTo(new IRStoreStaticFieldInstruction(Field), pNewMethod); }

        public override IRInstruction Transform() { return new IRMoveInstruction(this); }
    }
}
