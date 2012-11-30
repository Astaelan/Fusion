using System;
using System.Collections.Generic;
using Fusion.CLI.Metadata;

namespace Fusion.IR.Instructions
{
    public class IRNewObjectInstruction : IRInstruction
    {
        public IRMethod Constructor { get; private set; }

        public IRNewObjectInstruction(IRMethod pConstructor) : base(IROpcode.NewObject)
        {
            Constructor = pConstructor;
        }
    }
}
