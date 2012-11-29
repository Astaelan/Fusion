using Fusion.CLI.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRLoadFieldAddressInstruction : IRInstruction
    {
        public IRField Field = null;

        public IRLoadFieldAddressInstruction(IRField pField)
            : base(IROpcode.LoadFieldAddress)
        {
            Field = pField;
        }
    }
}
