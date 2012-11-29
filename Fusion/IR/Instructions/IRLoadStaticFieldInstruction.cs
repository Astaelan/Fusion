using System;
using System.Collections.Generic;
using Fusion.CLI.Metadata;

namespace Fusion.IR.Instructions
{
    public class IRLoadStaticFieldInstruction : IRInstruction
    {
        public IRField Field { get; private set; }

        public IRLoadStaticFieldInstruction(IRField pField) : base(IROpcode.LoadStaticField)
        {
            Field = pField;
        }
    }
}
