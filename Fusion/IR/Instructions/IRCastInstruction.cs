using System;
using System.Collections.Generic;
using Fusion.CLI.Metadata;

namespace Fusion.IR.Instructions
{
    public class IRCastInstruction : IRInstruction
    {
        public IRType Type { get; private set; }
        public bool ThrowExceptionOnFailure { get; private set; }

        public IRCastInstruction(IRType pType, bool pThrowExceptionOnFailure) : base(IROpcode.Cast)
        {
            Type = pType;
            ThrowExceptionOnFailure = pThrowExceptionOnFailure;
        }
    }
}
