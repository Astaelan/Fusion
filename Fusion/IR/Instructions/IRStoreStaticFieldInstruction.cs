using Fusion.CLI.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRStoreStaticFieldInstruction : IRInstruction
    {
        public IRField Field = null;

        public IRStoreStaticFieldInstruction(IRField pField)
            : base(IROpcode.StoreStaticField)
        {
            Field = pField;
        }
    }
}
