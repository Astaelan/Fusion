using System;
using System.Collections.Generic;
using Fusion.CLI.Metadata;

namespace Fusion.IR.Instructions
{
    public class IRLoadFieldAddressInstruction : IRInstruction
    {
        public IRField Field { get; private set; }

        public IRLoadFieldAddressInstruction(IRField pField) : base(IROpcode.LoadFieldAddress)
        {
            Field = pField;
        }
    }
}
