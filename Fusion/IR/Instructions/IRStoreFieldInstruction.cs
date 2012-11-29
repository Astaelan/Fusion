using Fusion.CLI.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRStoreFieldInstruction : IRInstruction
    {
        public IRField Field = null;

        public IRStoreFieldInstruction(IRField pField)
            : base(IROpcode.StoreField)
        {
            Field = pField;
        }
    }
}
