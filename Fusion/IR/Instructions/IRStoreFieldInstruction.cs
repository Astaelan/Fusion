using System;
using System.Collections.Generic;
using Fusion.CLI.Metadata;

namespace Fusion.IR.Instructions
{
    public class IRStoreFieldInstruction : IRInstruction
    {
        public IRField Field { get; private set; }

        public IRStoreFieldInstruction(IRField pField) : base(IROpcode.StoreField)
        {
            Field = pField;
        }
    }
}
