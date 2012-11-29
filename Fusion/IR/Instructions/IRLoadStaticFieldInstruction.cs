using Fusion.CLI.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRLoadStaticFieldInstruction : IRInstruction
    {
        public IRField Field = null;

        public IRLoadStaticFieldInstruction(IRField pField)
            : base(IROpcode.LoadStaticField)
        {
            Field = pField;
        }
    }
}
