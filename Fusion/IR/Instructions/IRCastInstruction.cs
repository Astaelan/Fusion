using System;
using System.Collections.Generic;
using Fusion.CLI.Metadata;

namespace Fusion.IR.Instructions
{
    public class IRCastInstruction : IRInstruction
    {
        private IRType mType = null;
        public IRType Type
        {
            get { return mType; }
            private set { mType = value; }
        }
        
        private bool mThrowExceptionOnFailure = false;
        public bool ThrowExceptionOnFailure
        {
            get { return mThrowExceptionOnFailure; }
            private set { mThrowExceptionOnFailure = value; }
        }

        public IRCastInstruction(IRType pType, bool pThrowExceptionOnFailure) : base(IROpcode.Cast)
        {
            Type = pType;
            ThrowExceptionOnFailure = pThrowExceptionOnFailure;
        }
    }
}
