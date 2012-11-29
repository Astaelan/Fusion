using System;
using System.Collections.Generic;
using Fusion.CLI.Metadata;

namespace Fusion.IR.Instructions
{
    public class IRLoadFieldInstruction : IRInstruction
    {
        public IRField Field { get; private set; }

        public IRLoadFieldInstruction(IRField pField) : base(IROpcode.LoadField)
        {
            Field = pField;
        }
    }
}
