using System;
using System.Collections.Generic;
using Fusion.CLI.Metadata;

namespace Fusion.IR.Instructions
{
    public class IRCallInstruction : IRInstruction
    {
        private IRMethod mTarget = null;
        public IRMethod Target
        {
            get { return mTarget; }
            private set { mTarget = value; }
        }
        
        private bool mVirtual = false;
        public bool Virtual
        {
            get { return mVirtual; }
            protected set { mVirtual = value; }
        }

        public IRCallInstruction(IRMethod pTarget, bool pVirtual) : base(IROpcode.Call)
        {
            Target = pTarget;
            Virtual = pVirtual;
        }
    }
}
