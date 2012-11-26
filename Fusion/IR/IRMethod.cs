using Fusion.CLI.Metadata;
using Fusion.CLI.Signature;
using Fusion.IL;
using System;
using System.Collections.Generic;

namespace Fusion.IR
{
    public sealed class IRMethod
    {
        public IRAssembly Assembly;
        public MethodDefData MethodDefData;
        public IRType ParentType;
        public IRType ReturnType = null;
        public List<IRParameter> Parameters = new List<IRParameter>();
        public List<IRLocal> Locals = new List<IRLocal>();
        public List<IRInstruction> Instructions = new List<IRInstruction>();

        public IRMethod(IRAssembly pAssembly, MethodDefData pMethodDefData, IRType pParentType)
        {
            Assembly = pAssembly;
            MethodDefData = pMethodDefData;
            ParentType = pParentType;
        }

        public IRInstruction AddInstruction(uint pILOffset, IROpcode pOpcode, params object[] pArguments)
        {
            IRInstruction instruction = new IRInstruction(pILOffset, (uint)Instructions.Count, pOpcode);
            instruction.Arguments.AddRange(pArguments);
            Instructions.Add(instruction);
            return instruction;
        }

        public void LoadInstructions()
        {
            if (MethodDefData.Body == null) return;

            ILReader reader = new ILReader(MethodDefData.CLIFile.Data, MethodDefData.Body.CodeRVA, MethodDefData.Body.CodeSize);
            ILOpcode opcode = ILOpcode.Nop;
            MethodSig methodSignature = MethodDefData.ExpandedSignature;

            while (!reader.EndOfCode)
            {
                uint startOfInstruction = reader.Offset;
                opcode = reader.ReadOpcode();
                switch (opcode)
                {
                    case ILOpcode.Nop:
                        {
                            AddInstruction(startOfInstruction, IROpcode.Nop, true);
                            break;
                        }
                    case ILOpcode.Break:
                        {
                            AddInstruction(startOfInstruction, IROpcode.Break);
                            break;
                        }
                    case ILOpcode.LdArg_0:
                        {
                            AddInstruction(startOfInstruction, IROpcode.LoadParameter, (uint)0);
                            break;
                        }
                    default: break;
                }
            }
        }
    }
}
