using Fusion.CLI.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRLoadFieldInstruction : IRInstruction
    {
        public IRField Field = null;

        public IRLoadFieldInstruction(IRField pField)
            : base(IROpcode.LoadField)
        {
            Field = pField;
        }
    }
}
