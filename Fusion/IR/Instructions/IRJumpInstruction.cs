using System;
using System.Collections.Generic;
using Fusion.CLI.Metadata;

namespace Fusion.IR.Instructions
{
    public class IRJumpInstruction : IRInstruction
    {
        public IRMethod Target { get; private set; }

        public IRJumpInstruction(IRMethod pTarget) : base(IROpcode.Jump)
        {
            Target = pTarget;
        }
    }
}
