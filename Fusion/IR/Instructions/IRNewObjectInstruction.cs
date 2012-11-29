using Fusion.CLI.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.IR.Instructions
{
    public sealed class IRNewObjectInstruction : IRInstruction
    {
        public IRMethod Constructor = null;

        public IRNewObjectInstruction(IRMethod pConstructor)
            : base(IROpcode.NewObject)
        {
            Constructor = pConstructor;
        }
    }
}
