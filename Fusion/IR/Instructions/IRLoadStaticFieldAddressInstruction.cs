using Fusion.CLI.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRLoadStaticFieldAddressInstruction : IRInstruction
    {
        public IRField Field = null;

        public IRLoadStaticFieldAddressInstruction(IRField pField)
            : base(IROpcode.LoadStaticFieldAddress)
        {
            Field = pField;
        }
    }
}
