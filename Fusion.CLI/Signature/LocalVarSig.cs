﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.CLI.Signature
{
    public sealed class LocalVarSig
    {
        public CLIFile CLIFile = null;

        public List<SigLocalVar> LocalVars = null;

        public LocalVarSig(CLIFile pCLIFile, byte[] pSignature, ref int pCursor)
        {
            CLIFile = pCLIFile;

            ++pCursor;
            uint localVarCount = CLIFile.ReadCompressedUnsigned(pSignature, ref pCursor);
            LocalVars = new List<SigLocalVar>((int)localVarCount);
            for (uint localVarIndex = 0; localVarIndex < localVarCount; ++localVarIndex) LocalVars.Add(new SigLocalVar(CLIFile, pSignature, ref pCursor));
        }
    }
}
