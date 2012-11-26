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

        public void ConvertInstructions()
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
                    case ILOpcode.Nop: AddInstruction(startOfInstruction, IROpcode.Nop, true); break;
                    case ILOpcode.Break: AddInstruction(startOfInstruction, IROpcode.Break); break;
                    case ILOpcode.LdArg_0: AddInstruction(startOfInstruction, IROpcode.LoadParameter, (uint)0); break;
                    case ILOpcode.LdArg_1: AddInstruction(startOfInstruction, IROpcode.LoadParameter, (uint)1); break;
                    case ILOpcode.LdArg_2: AddInstruction(startOfInstruction, IROpcode.LoadParameter, (uint)2); break;
                    case ILOpcode.LdArg_3: AddInstruction(startOfInstruction, IROpcode.LoadParameter, (uint)3); break;
                    case ILOpcode.LdLoc_0: AddInstruction(startOfInstruction, IROpcode.LoadLocal, (uint)0); break;
                    case ILOpcode.LdLoc_1: AddInstruction(startOfInstruction, IROpcode.LoadLocal, (uint)1); break;
                    case ILOpcode.LdLoc_2: AddInstruction(startOfInstruction, IROpcode.LoadLocal, (uint)2); break;
                    case ILOpcode.LdLoc_3: AddInstruction(startOfInstruction, IROpcode.LoadLocal, (uint)3); break;
                    case ILOpcode.StLoc_0: AddInstruction(startOfInstruction, IROpcode.StoreLocal, (uint)0); break;
                    case ILOpcode.StLoc_1: AddInstruction(startOfInstruction, IROpcode.StoreLocal, (uint)1); break;
                    case ILOpcode.StLoc_2: AddInstruction(startOfInstruction, IROpcode.StoreLocal, (uint)2); break;
                    case ILOpcode.StLoc_3: AddInstruction(startOfInstruction, IROpcode.StoreLocal, (uint)3); break;
                    case ILOpcode.LdArg_S: AddInstruction(startOfInstruction, IROpcode.LoadParameter, (uint)reader.ReadByte()); break;
                    case ILOpcode.LdArgA_S: AddInstruction(startOfInstruction, IROpcode.LoadParameterAddress, (uint)reader.ReadByte()); break;
                    case ILOpcode.StArg_S: AddInstruction(startOfInstruction, IROpcode.StoreParameter, (uint)reader.ReadByte()); break;
                    case ILOpcode.LdLoc_S: AddInstruction(startOfInstruction, IROpcode.LoadLocal, (uint)reader.ReadByte()); break;
                    case ILOpcode.LdLocA_S: AddInstruction(startOfInstruction, IROpcode.LoadLocalAddress, (uint)reader.ReadByte()); break;
                    case ILOpcode.StLoc_S: AddInstruction(startOfInstruction, IROpcode.StoreLocal, (uint)reader.ReadByte()); break;
                    case ILOpcode.LdNull: AddInstruction(startOfInstruction, IROpcode.LoadNull); break;
                    case ILOpcode.Ldc_I4_M1: AddInstruction(startOfInstruction, IROpcode.LoadInteger32, (int)-1); break;
                    case ILOpcode.Ldc_I4_0: AddInstruction(startOfInstruction, IROpcode.LoadInteger32, (int)0); break;
                    case ILOpcode.Ldc_I4_1: AddInstruction(startOfInstruction, IROpcode.LoadInteger32, (int)1); break;
                    case ILOpcode.Ldc_I4_2: AddInstruction(startOfInstruction, IROpcode.LoadInteger32, (int)2); break;
                    case ILOpcode.Ldc_I4_3: AddInstruction(startOfInstruction, IROpcode.LoadInteger32, (int)3); break;
                    case ILOpcode.Ldc_I4_4: AddInstruction(startOfInstruction, IROpcode.LoadInteger32, (int)4); break;
                    case ILOpcode.Ldc_I4_5: AddInstruction(startOfInstruction, IROpcode.LoadInteger32, (int)5); break;
                    case ILOpcode.Ldc_I4_6: AddInstruction(startOfInstruction, IROpcode.LoadInteger32, (int)6); break;
                    case ILOpcode.Ldc_I4_7: AddInstruction(startOfInstruction, IROpcode.LoadInteger32, (int)7); break;
                    case ILOpcode.Ldc_I4_8: AddInstruction(startOfInstruction, IROpcode.LoadInteger32, (int)8); break;
                    case ILOpcode.Ldc_I4_S: AddInstruction(startOfInstruction, IROpcode.LoadInteger32, (int)reader.ReadByte()); break;
                    case ILOpcode.Ldc_I4: AddInstruction(startOfInstruction, IROpcode.LoadInteger32, (int)reader.ReadUInt32()); break;
                    case ILOpcode.Ldc_I8: AddInstruction(startOfInstruction, IROpcode.LoadInteger64, (long)reader.ReadUInt64()); break;
                    case ILOpcode.Ldc_R4: AddInstruction(startOfInstruction, IROpcode.LoadReal32, reader.ReadSingle()); break;
                    case ILOpcode.Ldc_R8: AddInstruction(startOfInstruction, IROpcode.LoadReal64, reader.ReadDouble()); break;
                    case ILOpcode.Dup: AddInstruction(startOfInstruction, IROpcode.Duplicate); break;
                    case ILOpcode.Pop: AddInstruction(startOfInstruction, IROpcode.Pop); break;
                    case ILOpcode.Jmp: AddInstruction(startOfInstruction, IROpcode.Jump, Assembly.File.ExpandMetadataToken(reader.ReadUInt32())); break;
                    case ILOpcode.Call: AddInstruction(startOfInstruction, IROpcode.CallAbsolute, Assembly.File.ExpandMetadataToken(reader.ReadUInt32())); break;
                    case ILOpcode.CallI: throw new NotImplementedException("CallI");
                    case ILOpcode.Ret: AddInstruction(startOfInstruction, IROpcode.Return); break;
                    //case ILOpcode.Br: AddInstruction(startOfInstruction, IROpcode.Branch, IRBranchCondition.Always, (uint)(reader.ReadUInt32() + reader.Offset)); break;
                    case ILOpcode.CallVirt: AddInstruction(startOfInstruction, IROpcode.CallVirtual, Assembly.File.ExpandMetadataToken(reader.ReadUInt32())); break;
                    case ILOpcode.LdStr: AddInstruction(startOfInstruction, IROpcode.LoadString, Assembly.File.ExpandMetadataToken(reader.ReadUInt32()).Data); break;

                    default: break;
                }
            }
        }
    }
}
