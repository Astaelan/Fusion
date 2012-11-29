using System;
using System.Collections.Generic;
using Fusion.CLI.Metadata;

namespace Fusion.IR.Instructions
{
    public class IRLoadStaticFieldAddressInstruction : IRInstruction
    {
        public IRField Field { get; private set; }

        public IRLoadStaticFieldAddressInstruction(IRField pField) : base(IROpcode.LoadStaticFieldAddress)
        {
            Field = pField;
        }
    }
}
