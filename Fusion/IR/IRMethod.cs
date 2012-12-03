using Fusion.CLI.Metadata;
using Fusion.CLI.Signature;
using Fusion.IL;
using Fusion.IR.Instructions;
using System;
using System.Collections.Generic;
using Fusion.CLI;

namespace Fusion.IR
{
    public sealed class IRMethod
    {
        public IRAssembly Assembly = null;

        public string Name = null;

        public IRType ParentType = null;
        public IRType ReturnType = null;
        public readonly List<IRParameter> Parameters = new List<IRParameter>();
        public readonly List<IRLocal> Locals = new List<IRLocal>();
        public readonly List<IRInstruction> Instructions = new List<IRInstruction>();

        private bool? mResolvedCache;
        public bool Resolved
        {
            get
            {
                if (mResolvedCache != null)
                    return mResolvedCache.Value;
                
                bool ressed = false;
                if (IsGeneric)
                {
                    if (!ReturnType.Resolved) goto SetCache;
                    if (!Parameters.TrueForAll(p => p.Resolved)) goto SetCache;
                    if (!Locals.TrueForAll(l => l.Resolved)) goto SetCache;
                    if (!Instructions.TrueForAll(i => i.Resolved)) goto SetCache;
                }
                ressed = true;

            SetCache:
                mResolvedCache = ressed;
                return mResolvedCache.Value;
            }
        }

        // Dynamic Methods
        public bool IsGeneric
        {
            get { return GenericParameters.Count > 0; }
        }
        public IRMethod GenericMethod = null;
        public readonly GenericParameterCollection GenericParameters = new GenericParameterCollection();

        // Temporary
        public ushort MaximumStackDepth = 0;
        public IRControlFlowGraph ControlFlowGraph = null;

        public IRMethod(IRAssembly pAssembly)
        {
            Assembly = pAssembly;
        }

        /// <summary>
        /// This creates a shallow clone of this method, but
        /// does a deep clone of it's instructions, parameters, and locals.
        /// </summary>
        /// <param name="newParent">The parent for the new method.</param>
        /// <returns>The clone of this method.</returns>
        public IRMethod Clone(IRType newParent)
        {
            IRMethod m = new IRMethod(this.Assembly);

            m.GenericMethod = this.GenericMethod;
            m.GenericParameters.AddRange(this.GenericParameters);
            this.Instructions.ForEach(i => m.Instructions.Add(i.Clone(m)));
            this.Locals.ForEach(l => m.Locals.Add(l.Clone(m)));
            this.Parameters.ForEach(p => m.Parameters.Add(p.Clone(m)));
            m.MaximumStackDepth = this.MaximumStackDepth;
            m.Name = this.Name;
            m.ParentType = newParent;
            m.ReturnType = this.ReturnType;

            return m;
        }

        public bool CompareSignature(MethodSig pMethodSig)
        {
            if (Parameters.Count != pMethodSig.Params.Count) return false;
            if (ReturnType != Assembly.AppDomain.PresolveType(pMethodSig.RetType)) return false;
            for (int index = 0; index < Parameters.Count; ++index)
            {
                if (Parameters[index].Type != Assembly.AppDomain.PresolveType(pMethodSig.Params[index])) return false;
            }
            return true;
        }

        public bool CompareSignature(MemberRefData pMemberRefData)
        {
            if (Name != pMemberRefData.Name) return false;
            return CompareSignature(pMemberRefData.ExpandedMethodSignature);
        }

        public void AddInstruction(uint pILOffset, IRInstruction pInstruction)
        {
            pInstruction.ILOffset = pILOffset;
            pInstruction.IRIndex = (uint)Instructions.Count;
            pInstruction.Method = this;
            Instructions.Add(pInstruction);
        }

        public void ConvertInstructions(MethodDefData pMethodDefData)
        {
            if (pMethodDefData.Body == null) return;

            ILReader reader = new ILReader(pMethodDefData.CLIFile.Data, pMethodDefData.Body.CodeRVA, pMethodDefData.Body.CodeSize);
            ILOpcode opcode = ILOpcode.Nop;
            ILExtendedOpcode extendedOpcode = ILExtendedOpcode.ArgList;
            MethodSig methodSignature = pMethodDefData.ExpandedSignature;
            IRPrefixFlags prefixFlags = IRPrefixFlags.None;
            uint prefixConstrainedToken = 0;

            Console.WriteLine("Converting {0}.{1}.{2}", ParentType.Namespace, ParentType.Name, Name);
            while (!reader.EndOfCode)
            {
                bool clearFlags = true;
                uint startOfInstruction = reader.Offset;
                opcode = reader.ReadOpcode();
                switch (opcode)
                {
                    case ILOpcode.Nop: AddInstruction(startOfInstruction, new IRNopInstruction(true)); break;
                    case ILOpcode.Break: AddInstruction(startOfInstruction, new IRBreakInstruction()); break;
                    case ILOpcode.LdArg_0: AddInstruction(startOfInstruction, new IRLoadParameterInstruction(0)); break;
                    case ILOpcode.LdArg_1: AddInstruction(startOfInstruction, new IRLoadParameterInstruction(1)); break;
                    case ILOpcode.LdArg_2: AddInstruction(startOfInstruction, new IRLoadParameterInstruction(2)); break;
                    case ILOpcode.LdArg_3: AddInstruction(startOfInstruction, new IRLoadParameterInstruction(3)); break;
                    case ILOpcode.LdLoc_0: AddInstruction(startOfInstruction, new IRLoadLocalInstruction(0)); break;
                    case ILOpcode.LdLoc_1: AddInstruction(startOfInstruction, new IRLoadLocalInstruction(1)); break;
                    case ILOpcode.LdLoc_2: AddInstruction(startOfInstruction, new IRLoadLocalInstruction(2)); break;
                    case ILOpcode.LdLoc_3: AddInstruction(startOfInstruction, new IRLoadLocalInstruction(3)); break;
                    case ILOpcode.StLoc_0: AddInstruction(startOfInstruction, new IRStoreLocalInstruction(0)); break;
                    case ILOpcode.StLoc_1: AddInstruction(startOfInstruction, new IRStoreLocalInstruction(1)); break;
                    case ILOpcode.StLoc_2: AddInstruction(startOfInstruction, new IRStoreLocalInstruction(2)); break;
                    case ILOpcode.StLoc_3: AddInstruction(startOfInstruction, new IRStoreLocalInstruction(3)); break;
                    case ILOpcode.LdArg_S: AddInstruction(startOfInstruction, new IRLoadParameterInstruction(reader.ReadByte())); break;
                    case ILOpcode.LdArgA_S: AddInstruction(startOfInstruction, new IRLoadParameterAddressInstruction(reader.ReadByte())); break;
                    case ILOpcode.StArg_S: AddInstruction(startOfInstruction, new IRStoreParameterInstruction(reader.ReadByte())); break;
                    case ILOpcode.LdLoc_S: AddInstruction(startOfInstruction, new IRLoadLocalInstruction(reader.ReadByte())); break;
                    case ILOpcode.LdLocA_S: AddInstruction(startOfInstruction, new IRLoadLocalAddressInstruction(reader.ReadByte())); break;
                    case ILOpcode.StLoc_S: AddInstruction(startOfInstruction, new IRStoreLocalInstruction(reader.ReadByte())); break;
                    case ILOpcode.LdNull: AddInstruction(startOfInstruction, new IRLoadNullInstruction()); break;
                    case ILOpcode.Ldc_I4_M1: AddInstruction(startOfInstruction, new IRLoadInteger32Instruction(-1)); break;
                    case ILOpcode.Ldc_I4_0: AddInstruction(startOfInstruction, new IRLoadInteger32Instruction(0)); break;
                    case ILOpcode.Ldc_I4_1: AddInstruction(startOfInstruction, new IRLoadInteger32Instruction(1)); break;
                    case ILOpcode.Ldc_I4_2: AddInstruction(startOfInstruction, new IRLoadInteger32Instruction(2)); break;
                    case ILOpcode.Ldc_I4_3: AddInstruction(startOfInstruction, new IRLoadInteger32Instruction(3)); break;
                    case ILOpcode.Ldc_I4_4: AddInstruction(startOfInstruction, new IRLoadInteger32Instruction(4)); break;
                    case ILOpcode.Ldc_I4_5: AddInstruction(startOfInstruction, new IRLoadInteger32Instruction(5)); break;
                    case ILOpcode.Ldc_I4_6: AddInstruction(startOfInstruction, new IRLoadInteger32Instruction(6)); break;
                    case ILOpcode.Ldc_I4_7: AddInstruction(startOfInstruction, new IRLoadInteger32Instruction(7)); break;
                    case ILOpcode.Ldc_I4_8: AddInstruction(startOfInstruction, new IRLoadInteger32Instruction(8)); break;
                    case ILOpcode.Ldc_I4_S: AddInstruction(startOfInstruction, new IRLoadInteger32Instruction(reader.ReadByte())); break;
                    case ILOpcode.Ldc_I4: AddInstruction(startOfInstruction, new IRLoadInteger32Instruction((int)reader.ReadUInt32())); break;
                    case ILOpcode.Ldc_I8: AddInstruction(startOfInstruction, new IRLoadInteger64Instruction((long)reader.ReadUInt64())); break;
                    case ILOpcode.Ldc_R4: AddInstruction(startOfInstruction, new IRLoadReal32Instruction(reader.ReadSingle())); break;
                    case ILOpcode.Ldc_R8: AddInstruction(startOfInstruction, new IRLoadReal64Instruction(reader.ReadDouble())); break;
                    case ILOpcode.Dup: AddInstruction(startOfInstruction, new IRDuplicateInstruction()); break;
                    case ILOpcode.Pop: AddInstruction(startOfInstruction, new IRPopInstruction()); break;
                    case ILOpcode.Jmp: AddInstruction(startOfInstruction, new IRJumpInstruction(Assembly.AppDomain.PresolveMethod(Assembly.File.ExpandMetadataToken(reader.ReadUInt32())))); break;
                    case ILOpcode.Call: AddInstruction(startOfInstruction, new IRCallInstruction(Assembly.AppDomain.PresolveMethod(Assembly.File.ExpandMetadataToken(reader.ReadUInt32())), false)); break;
                    case ILOpcode.CallI: throw new NotImplementedException("CallI");
                    case ILOpcode.Ret: AddInstruction(startOfInstruction, new IRReturnInstruction()); break;
                    case ILOpcode.Br_S: AddInstruction(startOfInstruction, new IRBranchInstruction(IRBranchCondition.Always, (uint)(reader.ReadByte() + reader.Offset))); break;
                    case ILOpcode.BrFalse_S: AddInstruction(startOfInstruction, new IRBranchInstruction(IRBranchCondition.False, (uint)(reader.ReadByte() + reader.Offset))); break;
                    case ILOpcode.BrTrue_S: AddInstruction(startOfInstruction, new IRBranchInstruction(IRBranchCondition.True, (uint)(reader.ReadByte() + reader.Offset))); break;
                    case ILOpcode.Beq_S: AddInstruction(startOfInstruction, new IRBranchInstruction(IRBranchCondition.Equal, (uint)(reader.ReadByte() + reader.Offset))); break;
                    case ILOpcode.Bge_S: AddInstruction(startOfInstruction, new IRBranchInstruction(IRBranchCondition.GreaterOrEqual, (uint)(reader.ReadByte() + reader.Offset))); break;
                    case ILOpcode.Bgt_S: AddInstruction(startOfInstruction, new IRBranchInstruction(IRBranchCondition.Greater, (uint)(reader.ReadByte() + reader.Offset))); break;
                    case ILOpcode.Ble_S: AddInstruction(startOfInstruction, new IRBranchInstruction(IRBranchCondition.LessOrEqual, (uint)(reader.ReadByte() + reader.Offset))); break;
                    case ILOpcode.Blt_S: AddInstruction(startOfInstruction, new IRBranchInstruction(IRBranchCondition.Less, (uint)(reader.ReadByte() + reader.Offset))); break;
                    case ILOpcode.Bne_Un_S: AddInstruction(startOfInstruction, new IRBranchInstruction(IRBranchCondition.NotEqualUnsigned, (uint)(reader.ReadByte() + reader.Offset))); break;
                    case ILOpcode.Bge_Un_S: AddInstruction(startOfInstruction, new IRBranchInstruction(IRBranchCondition.GreaterOrEqualUnsigned, (uint)(reader.ReadByte() + reader.Offset))); break;
                    case ILOpcode.Bgt_Un_S: AddInstruction(startOfInstruction, new IRBranchInstruction(IRBranchCondition.GreaterUnsigned, (uint)(reader.ReadByte() + reader.Offset))); break;
                    case ILOpcode.Ble_Un_S: AddInstruction(startOfInstruction, new IRBranchInstruction(IRBranchCondition.LessOrEqualUnsigned, (uint)(reader.ReadByte() + reader.Offset))); break;
                    case ILOpcode.Blt_Un_S: AddInstruction(startOfInstruction, new IRBranchInstruction(IRBranchCondition.LessUnsigned, (uint)(reader.ReadByte() + reader.Offset))); break;
                    case ILOpcode.Br: AddInstruction(startOfInstruction, new IRBranchInstruction(IRBranchCondition.Always, (uint)(reader.ReadUInt32() + reader.Offset))); break;
                    case ILOpcode.BrFalse: AddInstruction(startOfInstruction, new IRBranchInstruction(IRBranchCondition.False, (uint)(reader.ReadUInt32() + reader.Offset))); break;
                    case ILOpcode.BrTrue: AddInstruction(startOfInstruction, new IRBranchInstruction(IRBranchCondition.True, (uint)(reader.ReadUInt32() + reader.Offset))); break;
                    case ILOpcode.Beq: AddInstruction(startOfInstruction, new IRBranchInstruction(IRBranchCondition.Equal, (uint)(reader.ReadUInt32() + reader.Offset))); break;
                    case ILOpcode.Bge: AddInstruction(startOfInstruction, new IRBranchInstruction(IRBranchCondition.GreaterOrEqual, (uint)(reader.ReadUInt32() + reader.Offset))); break;
                    case ILOpcode.Bgt: AddInstruction(startOfInstruction, new IRBranchInstruction(IRBranchCondition.Greater, (uint)(reader.ReadUInt32() + reader.Offset))); break;
                    case ILOpcode.Ble: AddInstruction(startOfInstruction, new IRBranchInstruction(IRBranchCondition.LessOrEqual, (uint)(reader.ReadUInt32() + reader.Offset))); break;
                    case ILOpcode.Blt: AddInstruction(startOfInstruction, new IRBranchInstruction(IRBranchCondition.Less, (uint)(reader.ReadUInt32() + reader.Offset))); break;
                    case ILOpcode.Bne_Un: AddInstruction(startOfInstruction, new IRBranchInstruction(IRBranchCondition.NotEqualUnsigned, (uint)(reader.ReadUInt32() + reader.Offset))); break;
                    case ILOpcode.Bge_Un: AddInstruction(startOfInstruction, new IRBranchInstruction(IRBranchCondition.GreaterOrEqualUnsigned, (uint)(reader.ReadUInt32() + reader.Offset))); break;
                    case ILOpcode.Bgt_Un: AddInstruction(startOfInstruction, new IRBranchInstruction(IRBranchCondition.GreaterUnsigned, (uint)(reader.ReadUInt32() + reader.Offset))); break;
                    case ILOpcode.Ble_Un: AddInstruction(startOfInstruction, new IRBranchInstruction(IRBranchCondition.LessOrEqualUnsigned, (uint)(reader.ReadUInt32() + reader.Offset))); break;
                    case ILOpcode.Blt_Un: AddInstruction(startOfInstruction, new IRBranchInstruction(IRBranchCondition.LessUnsigned, (uint)(reader.ReadUInt32() + reader.Offset))); break;
                    case ILOpcode.Switch:
                        {
                            uint targetCount = reader.ReadUInt32();
                            uint[] targetILOffsets = new uint[targetCount];
                            for (int index = 0; index < targetCount; ++index) targetILOffsets[index] = reader.ReadUInt32();
                            for (int index = 0; index < targetCount; ++index) targetILOffsets[index] += reader.Offset;
                            AddInstruction(startOfInstruction, new IRSwitchInstruction(targetILOffsets));
                            break;
                        }
                    case ILOpcode.LdInd_I1: AddInstruction(startOfInstruction, new IRLoadIndirectInstruction(Assembly.AppDomain.System_SByte)); break;
                    case ILOpcode.LdInd_U1: AddInstruction(startOfInstruction, new IRLoadIndirectInstruction(Assembly.AppDomain.System_Byte)); break;
                    case ILOpcode.LdInd_I2: AddInstruction(startOfInstruction, new IRLoadIndirectInstruction(Assembly.AppDomain.System_Int16)); break;
                    case ILOpcode.LdInd_U2: AddInstruction(startOfInstruction, new IRLoadIndirectInstruction(Assembly.AppDomain.System_UInt16)); break;
                    case ILOpcode.LdInd_I4: AddInstruction(startOfInstruction, new IRLoadIndirectInstruction(Assembly.AppDomain.System_Int32)); break;
                    case ILOpcode.LdInd_U4: AddInstruction(startOfInstruction, new IRLoadIndirectInstruction(Assembly.AppDomain.System_UInt32)); break;
                    case ILOpcode.LdInd_I8: AddInstruction(startOfInstruction, new IRLoadIndirectInstruction(Assembly.AppDomain.System_Int64)); break;
                    case ILOpcode.LdInd_I: AddInstruction(startOfInstruction, new IRLoadIndirectInstruction(Assembly.AppDomain.System_IntPtr)); break;
                    case ILOpcode.LdInd_R4: AddInstruction(startOfInstruction, new IRLoadIndirectInstruction(Assembly.AppDomain.System_Single)); break;
                    case ILOpcode.LdInd_R8: AddInstruction(startOfInstruction, new IRLoadIndirectInstruction(Assembly.AppDomain.System_Double)); break;
                    case ILOpcode.LdInd_Ref: AddInstruction(startOfInstruction, new IRLoadIndirectInstruction(Assembly.AppDomain.System_Object)); break;
                    case ILOpcode.StInd_Ref: AddInstruction(startOfInstruction, new IRStoreIndirectInstruction(Assembly.AppDomain.System_Object)); break;
                    case ILOpcode.StInd_I1: AddInstruction(startOfInstruction, new IRStoreIndirectInstruction(Assembly.AppDomain.System_SByte)); break;
                    case ILOpcode.StInd_I2: AddInstruction(startOfInstruction, new IRStoreIndirectInstruction(Assembly.AppDomain.System_Int16)); break;
                    case ILOpcode.StInd_I4: AddInstruction(startOfInstruction, new IRStoreIndirectInstruction(Assembly.AppDomain.System_Int32)); break;
                    case ILOpcode.StInd_I8: AddInstruction(startOfInstruction, new IRStoreIndirectInstruction(Assembly.AppDomain.System_Int64)); break;
                    case ILOpcode.StInd_R4: AddInstruction(startOfInstruction, new IRStoreIndirectInstruction(Assembly.AppDomain.System_Single)); break;
                    case ILOpcode.StInd_R8: AddInstruction(startOfInstruction, new IRStoreIndirectInstruction(Assembly.AppDomain.System_Double)); break;
                    case ILOpcode.Add: AddInstruction(startOfInstruction, new IRAddInstruction(IROverflowType.None)); break;
                    case ILOpcode.Sub: AddInstruction(startOfInstruction, new IRSubtractInstruction(IROverflowType.None)); break;
                    case ILOpcode.Mul: AddInstruction(startOfInstruction, new IRMultiplyInstruction(IROverflowType.None)); break;
                    case ILOpcode.Div: AddInstruction(startOfInstruction, new IRDivideInstruction(IROverflowType.Signed)); break;
                    case ILOpcode.Div_Un: AddInstruction(startOfInstruction, new IRDivideInstruction(IROverflowType.Unsigned)); break;
                    case ILOpcode.Rem: AddInstruction(startOfInstruction, new IRRemainderInstruction(IROverflowType.Signed)); break;
                    case ILOpcode.Rem_Un: AddInstruction(startOfInstruction, new IRRemainderInstruction(IROverflowType.Unsigned)); break;
                    case ILOpcode.And: AddInstruction(startOfInstruction, new IRAndInstruction()); break;
                    case ILOpcode.Or: AddInstruction(startOfInstruction, new IROrInstruction()); break;
                    case ILOpcode.Xor: AddInstruction(startOfInstruction, new IRXorInstruction()); break;
                    case ILOpcode.Shl: AddInstruction(startOfInstruction, new IRShiftInstruction(IRShiftType.Left)); break;
                    case ILOpcode.Shr: AddInstruction(startOfInstruction, new IRShiftInstruction(IRShiftType.RightSignExtended)); break;
                    case ILOpcode.Shr_Un: AddInstruction(startOfInstruction, new IRShiftInstruction(IRShiftType.Right)); break;
                    case ILOpcode.Neg: AddInstruction(startOfInstruction, new IRNegateInstruction()); break;
                    case ILOpcode.Not: AddInstruction(startOfInstruction, new IRNotInstruction()); break;
                    case ILOpcode.Conv_I1: AddInstruction(startOfInstruction, new IRConvertUncheckedInstruction(Assembly.AppDomain.System_SByte)); break;
                    case ILOpcode.Conv_I2: AddInstruction(startOfInstruction, new IRConvertUncheckedInstruction(Assembly.AppDomain.System_Int16)); break;
                    case ILOpcode.Conv_I4: AddInstruction(startOfInstruction, new IRConvertUncheckedInstruction(Assembly.AppDomain.System_Int32)); break;
                    case ILOpcode.Conv_I8: AddInstruction(startOfInstruction, new IRConvertUncheckedInstruction(Assembly.AppDomain.System_Int64)); break;
                    case ILOpcode.Conv_R4: AddInstruction(startOfInstruction, new IRConvertUncheckedInstruction(Assembly.AppDomain.System_Single)); break;
                    case ILOpcode.Conv_R8: AddInstruction(startOfInstruction, new IRConvertUncheckedInstruction(Assembly.AppDomain.System_Double)); break;
                    case ILOpcode.Conv_U4: AddInstruction(startOfInstruction, new IRConvertUncheckedInstruction(Assembly.AppDomain.System_UInt32)); break;
                    case ILOpcode.Conv_U8: AddInstruction(startOfInstruction, new IRConvertUncheckedInstruction(Assembly.AppDomain.System_UInt64)); break;
                    case ILOpcode.CallVirt: AddInstruction(startOfInstruction, new IRCallInstruction(Assembly.AppDomain.PresolveMethod(Assembly.File.ExpandMetadataToken(reader.ReadUInt32())), true)); break;

                    case ILOpcode.CpObj: AddInstruction(startOfInstruction, new IRCopyObjectInstruction(Assembly.AppDomain.PresolveType(Assembly.File.ExpandMetadataToken(reader.ReadUInt32())))); break;
                    case ILOpcode.LdObj: AddInstruction(startOfInstruction, new IRLoadObjectInstruction(Assembly.AppDomain.PresolveType(Assembly.File.ExpandMetadataToken(reader.ReadUInt32())))); break;
                    case ILOpcode.LdStr: AddInstruction(startOfInstruction, new IRLoadStringInstruction((string)Assembly.File.ExpandMetadataToken(reader.ReadUInt32()).Data)); break;
                    case ILOpcode.NewObj: AddInstruction(startOfInstruction, new IRNewObjectInstruction(Assembly.AppDomain.PresolveMethod(Assembly.File.ExpandMetadataToken(reader.ReadUInt32())))); break;
                    case ILOpcode.CastClass: AddInstruction(startOfInstruction, new IRCastInstruction(Assembly.AppDomain.PresolveType(Assembly.File.ExpandMetadataToken(reader.ReadUInt32())), true)); break;
                    case ILOpcode.IsInst: AddInstruction(startOfInstruction, new IRCastInstruction(Assembly.AppDomain.PresolveType(Assembly.File.ExpandMetadataToken(reader.ReadUInt32())), false)); break;

                    case ILOpcode.Conv_R_Un: throw new NotImplementedException("Conv_R_Un");
                    case ILOpcode.Unbox: AddInstruction(startOfInstruction, new IRUnboxInstruction(Assembly.AppDomain.PresolveType(Assembly.File.ExpandMetadataToken(reader.ReadUInt32())), false)); break;
                    case ILOpcode.Throw: AddInstruction(startOfInstruction, new IRThrowInstruction()); break;
                    case ILOpcode.LdFld: AddInstruction(startOfInstruction, new IRLoadFieldInstruction(Assembly.AppDomain.PresolveField(Assembly.File.ExpandMetadataToken(reader.ReadUInt32())))); break;
                    case ILOpcode.LdFldA: AddInstruction(startOfInstruction, new IRLoadFieldAddressInstruction(Assembly.AppDomain.PresolveField(Assembly.File.ExpandMetadataToken(reader.ReadUInt32())))); break;
                    case ILOpcode.StFld: AddInstruction(startOfInstruction, new IRStoreFieldInstruction(Assembly.AppDomain.PresolveField(Assembly.File.ExpandMetadataToken(reader.ReadUInt32())))); break;
                    case ILOpcode.LdSFld: AddInstruction(startOfInstruction, new IRLoadStaticFieldInstruction(Assembly.AppDomain.PresolveField(Assembly.File.ExpandMetadataToken(reader.ReadUInt32())))); break;
                    case ILOpcode.LdSFldA: AddInstruction(startOfInstruction, new IRLoadStaticFieldAddressInstruction(Assembly.AppDomain.PresolveField(Assembly.File.ExpandMetadataToken(reader.ReadUInt32())))); break;
                    case ILOpcode.StSFld: AddInstruction(startOfInstruction, new IRStoreStaticFieldInstruction(Assembly.AppDomain.PresolveField(Assembly.File.ExpandMetadataToken(reader.ReadUInt32())))); break;
                    case ILOpcode.StObj: AddInstruction(startOfInstruction, new IRStoreObjectInstruction(Assembly.AppDomain.PresolveType(Assembly.File.ExpandMetadataToken(reader.ReadUInt32())))); break;
                    case ILOpcode.Conv_Ovf_I1_Un: AddInstruction(startOfInstruction, new IRConvertCheckedInstruction(Assembly.AppDomain.System_SByte, IROverflowType.Unsigned)); break;
                    case ILOpcode.Conv_Ovf_I2_Un: AddInstruction(startOfInstruction, new IRConvertCheckedInstruction(Assembly.AppDomain.System_Int16, IROverflowType.Unsigned)); break;
                    case ILOpcode.Conv_Ovf_I4_Un: AddInstruction(startOfInstruction, new IRConvertCheckedInstruction(Assembly.AppDomain.System_Int32, IROverflowType.Unsigned)); break;
                    case ILOpcode.Conv_Ovf_I8_Un: AddInstruction(startOfInstruction, new IRConvertCheckedInstruction(Assembly.AppDomain.System_Int64, IROverflowType.Unsigned)); break;
                    case ILOpcode.Conv_Ovf_U1_Un: AddInstruction(startOfInstruction, new IRConvertCheckedInstruction(Assembly.AppDomain.System_SByte, IROverflowType.Unsigned)); break;
                    case ILOpcode.Conv_Ovf_U2_Un: AddInstruction(startOfInstruction, new IRConvertCheckedInstruction(Assembly.AppDomain.System_Int16, IROverflowType.Unsigned)); break;
                    case ILOpcode.Conv_Ovf_U4_Un: AddInstruction(startOfInstruction, new IRConvertCheckedInstruction(Assembly.AppDomain.System_Int32, IROverflowType.Unsigned)); break;
                    case ILOpcode.Conv_Ovf_U8_Un: AddInstruction(startOfInstruction, new IRConvertCheckedInstruction(Assembly.AppDomain.System_Int64, IROverflowType.Unsigned)); break;
                    case ILOpcode.Conv_Ovf_I_Un: AddInstruction(startOfInstruction, new IRConvertCheckedInstruction(Assembly.AppDomain.System_IntPtr, IROverflowType.Unsigned)); break;
                    case ILOpcode.Conv_Ovf_U_Un: AddInstruction(startOfInstruction, new IRConvertCheckedInstruction(Assembly.AppDomain.System_UIntPtr, IROverflowType.Unsigned)); break;

                    case ILOpcode.Box: AddInstruction(startOfInstruction, new IRBoxInstruction(Assembly.AppDomain.PresolveType(Assembly.File.ExpandMetadataToken(reader.ReadUInt32())))); break;
                    case ILOpcode.NewArr: AddInstruction(startOfInstruction, new IRNewArrayInstruction(Assembly.AppDomain.PresolveType(Assembly.File.ExpandMetadataToken(reader.ReadUInt32())))); break;
                    case ILOpcode.LdLen: AddInstruction(startOfInstruction, new IRLoadArrayLengthInstruction()); break;
                    case ILOpcode.LdElemA: AddInstruction(startOfInstruction, new IRLoadArrayElementAddressInstruction(Assembly.AppDomain.PresolveType(Assembly.File.ExpandMetadataToken(reader.ReadUInt32())))); break;
                    case ILOpcode.LdElem_I1: AddInstruction(startOfInstruction, new IRLoadArrayElementInstruction(Assembly.AppDomain.System_SByte)); break;
                    case ILOpcode.LdElem_U1: AddInstruction(startOfInstruction, new IRLoadArrayElementInstruction(Assembly.AppDomain.System_Byte)); break;
                    case ILOpcode.LdElem_I2: AddInstruction(startOfInstruction, new IRLoadArrayElementInstruction(Assembly.AppDomain.System_Int16)); break;
                    case ILOpcode.LdElem_U2: AddInstruction(startOfInstruction, new IRLoadArrayElementInstruction(Assembly.AppDomain.System_UInt16)); break;
                    case ILOpcode.LdElem_I4: AddInstruction(startOfInstruction, new IRLoadArrayElementInstruction(Assembly.AppDomain.System_Int32)); break;
                    case ILOpcode.LdElem_U4: AddInstruction(startOfInstruction, new IRLoadArrayElementInstruction(Assembly.AppDomain.System_UInt32)); break;
                    case ILOpcode.LdElem_I8: AddInstruction(startOfInstruction, new IRLoadArrayElementInstruction(Assembly.AppDomain.System_Int64)); break;
                    case ILOpcode.LdElem_I: AddInstruction(startOfInstruction, new IRLoadArrayElementInstruction(Assembly.AppDomain.System_IntPtr)); break;
                    case ILOpcode.LdElem_R4: AddInstruction(startOfInstruction, new IRLoadArrayElementInstruction(Assembly.AppDomain.System_Single)); break;
                    case ILOpcode.LdElem_R8: AddInstruction(startOfInstruction, new IRLoadArrayElementInstruction(Assembly.AppDomain.System_Double)); break;
                    case ILOpcode.LdElem_Ref: AddInstruction(startOfInstruction, new IRLoadArrayElementInstruction(null)); break;
                    case ILOpcode.StElem_I: AddInstruction(startOfInstruction, new IRStoreArrayElementInstruction(Assembly.AppDomain.System_IntPtr)); break;
                    case ILOpcode.StElem_I1: AddInstruction(startOfInstruction, new IRStoreArrayElementInstruction(Assembly.AppDomain.System_SByte)); break;
                    case ILOpcode.StElem_I2: AddInstruction(startOfInstruction, new IRStoreArrayElementInstruction(Assembly.AppDomain.System_Int16)); break;
                    case ILOpcode.StElem_I4: AddInstruction(startOfInstruction, new IRStoreArrayElementInstruction(Assembly.AppDomain.System_Int32)); break;
                    case ILOpcode.StElem_I8: AddInstruction(startOfInstruction, new IRStoreArrayElementInstruction(Assembly.AppDomain.System_Int64)); break;
                    case ILOpcode.StElem_R4: AddInstruction(startOfInstruction, new IRStoreArrayElementInstruction(Assembly.AppDomain.System_Single)); break;
                    case ILOpcode.StElem_R8: AddInstruction(startOfInstruction, new IRStoreArrayElementInstruction(Assembly.AppDomain.System_Double)); break;
                    case ILOpcode.StElem_Ref: AddInstruction(startOfInstruction, new IRStoreArrayElementInstruction(null)); break;
                    case ILOpcode.LdElem: AddInstruction(startOfInstruction, new IRLoadArrayElementInstruction(Assembly.AppDomain.PresolveType(Assembly.File.ExpandMetadataToken(reader.ReadUInt32())))); break;
                    case ILOpcode.StElem: AddInstruction(startOfInstruction, new IRStoreArrayElementInstruction(Assembly.AppDomain.PresolveType(Assembly.File.ExpandMetadataToken(reader.ReadUInt32())))); break;
                    case ILOpcode.Unbox_Any: AddInstruction(startOfInstruction, new IRUnboxInstruction(Assembly.AppDomain.PresolveType(Assembly.File.ExpandMetadataToken(reader.ReadUInt32())), true)); break;
                    case ILOpcode.Conv_Ovf_I1: AddInstruction(startOfInstruction, new IRConvertCheckedInstruction(Assembly.AppDomain.System_SByte, IROverflowType.Signed)); break;
                    case ILOpcode.Conv_Ovf_U1: AddInstruction(startOfInstruction, new IRConvertCheckedInstruction(Assembly.AppDomain.System_Byte, IROverflowType.Signed)); break;
                    case ILOpcode.Conv_Ovf_I2: AddInstruction(startOfInstruction, new IRConvertCheckedInstruction(Assembly.AppDomain.System_Int16, IROverflowType.Signed)); break;
                    case ILOpcode.Conv_Ovf_U2: AddInstruction(startOfInstruction, new IRConvertCheckedInstruction(Assembly.AppDomain.System_UInt16, IROverflowType.Signed)); break;
                    case ILOpcode.Conv_Ovf_I4: AddInstruction(startOfInstruction, new IRConvertCheckedInstruction(Assembly.AppDomain.System_Int32, IROverflowType.Signed)); break;
                    case ILOpcode.Conv_Ovf_U4: AddInstruction(startOfInstruction, new IRConvertCheckedInstruction(Assembly.AppDomain.System_UInt32, IROverflowType.Signed)); break;
                    case ILOpcode.Conv_Ovf_I8: AddInstruction(startOfInstruction, new IRConvertCheckedInstruction(Assembly.AppDomain.System_Int64, IROverflowType.Signed)); break;
                    case ILOpcode.Conv_Ovf_U8: AddInstruction(startOfInstruction, new IRConvertCheckedInstruction(Assembly.AppDomain.System_UInt64, IROverflowType.Signed)); break;
                    case ILOpcode.RefAnyVal: AddInstruction(startOfInstruction, new IRLoadTypedReferenceAddressInstruction(Assembly.AppDomain.PresolveType(Assembly.File.ExpandMetadataToken(reader.ReadUInt32())))); break;
                    case ILOpcode.CkFinite: AddInstruction(startOfInstruction, new IRCheckFiniteInstruction()); break;
                    case ILOpcode.MkRefAny: AddInstruction(startOfInstruction, new IRLoadTypedReferenceInstruction(Assembly.AppDomain.PresolveType(Assembly.File.ExpandTypeDefRefOrSpecToken(reader.ReadUInt32())))); break;
                    case ILOpcode.LdToken:
                        {
                            IRType type = null;
                            IRMethod method = null;
                            IRField field = null;
                            MetadataToken token = Assembly.File.ExpandMetadataToken(reader.ReadUInt32());
                            switch (token.Table)
                            {
                                case CLIMetadataTables.TypeDef: type = Assembly.AppDomain.PresolveType((TypeDefData)token.Data); break;
                                case CLIMetadataTables.TypeRef: type = Assembly.AppDomain.PresolveType((TypeRefData)token.Data); break;
                                case CLIMetadataTables.TypeSpec: type = Assembly.AppDomain.PresolveType((TypeSpecData)token.Data); break;
                                case CLIMetadataTables.MethodDef: method = Assembly.AppDomain.PresolveMethod((MethodDefData)token.Data); break;
                                case CLIMetadataTables.MethodSpec: method = Assembly.AppDomain.PresolveMethod((MethodSpecData)token.Data); break;
                                case CLIMetadataTables.Field: field = Assembly.AppDomain.PresolveField((FieldData)token.Data); break;
                                case CLIMetadataTables.MemberRef:
                                    {
                                        MemberRefData memberRefData = (MemberRefData)token.Data;
                                        if (memberRefData.IsMethodRef) method = Assembly.AppDomain.PresolveMethod(memberRefData);
                                        else field = Assembly.AppDomain.PresolveField(memberRefData);
                                        break;
                                    }
                            }
                            AddInstruction(startOfInstruction, new IRLoadRuntimeHandleInstruction(type, method, field));
                            break;
                        }
                    case ILOpcode.Conv_U2: AddInstruction(startOfInstruction, new IRConvertUncheckedInstruction(Assembly.AppDomain.System_UInt16)); break;
                    case ILOpcode.Conv_U1: AddInstruction(startOfInstruction, new IRConvertUncheckedInstruction(Assembly.AppDomain.System_Byte)); break;
                    case ILOpcode.Conv_I: AddInstruction(startOfInstruction, new IRConvertUncheckedInstruction(Assembly.AppDomain.System_IntPtr)); break;
                    case ILOpcode.Conv_Ovf_I: AddInstruction(startOfInstruction, new IRConvertCheckedInstruction(Assembly.AppDomain.System_IntPtr, IROverflowType.Signed)); break;
                    case ILOpcode.Conv_Ovf_U: AddInstruction(startOfInstruction, new IRConvertCheckedInstruction(Assembly.AppDomain.System_UIntPtr, IROverflowType.Signed)); break;
                    case ILOpcode.Add_Ovf: AddInstruction(startOfInstruction, new IRAddInstruction(IROverflowType.Signed)); break;
                    case ILOpcode.Add_Ovf_Un: AddInstruction(startOfInstruction, new IRAddInstruction(IROverflowType.Unsigned)); break;
                    case ILOpcode.Mul_Ovf: AddInstruction(startOfInstruction, new IRMultiplyInstruction(IROverflowType.Signed)); break;
                    case ILOpcode.Mul_Ovf_Un: AddInstruction(startOfInstruction, new IRMultiplyInstruction(IROverflowType.Unsigned)); break;
                    case ILOpcode.Sub_Ovf: AddInstruction(startOfInstruction, new IRSubtractInstruction(IROverflowType.Signed)); break;
                    case ILOpcode.Sub_Ovf_Un: AddInstruction(startOfInstruction, new IRSubtractInstruction(IROverflowType.Unsigned)); break;
                    case ILOpcode.EndFinally: throw new NotImplementedException("EndFinally");
                    case ILOpcode.Leave: AddInstruction(startOfInstruction, new IRLeaveInstruction(reader.ReadUInt32() + reader.Offset)); break;
                    case ILOpcode.Leave_S: AddInstruction(startOfInstruction, new IRLeaveInstruction((uint)(reader.ReadByte() + reader.Offset))); break;
                    case ILOpcode.StInd_I: AddInstruction(startOfInstruction, new IRStoreIndirectInstruction(Assembly.AppDomain.System_IntPtr)); break;
                    case ILOpcode.Conv_U: AddInstruction(startOfInstruction, new IRConvertUncheckedInstruction(Assembly.AppDomain.System_UIntPtr)); break;

                    case ILOpcode.Extended:
                        {
                            extendedOpcode = (ILExtendedOpcode)reader.ReadByte();
                            switch (extendedOpcode)
                            {
                                case ILExtendedOpcode.ArgList: throw new NotImplementedException("ArgList");
                                case ILExtendedOpcode.Ceq: AddInstruction(startOfInstruction, new IRCompareInstruction(IRCompareCondition.Equal)); break;
                                case ILExtendedOpcode.Cgt: AddInstruction(startOfInstruction, new IRCompareInstruction(IRCompareCondition.GreaterThan)); break;
                                case ILExtendedOpcode.Cgt_Un: AddInstruction(startOfInstruction, new IRCompareInstruction(IRCompareCondition.GreaterThanUnsigned)); break;
                                case ILExtendedOpcode.Clt: AddInstruction(startOfInstruction, new IRCompareInstruction(IRCompareCondition.LessThan)); break;
                                case ILExtendedOpcode.Clt_Un: AddInstruction(startOfInstruction, new IRCompareInstruction(IRCompareCondition.LessThanUnsigned)); break;
                                case ILExtendedOpcode.LdFtn: AddInstruction(startOfInstruction, new IRLoadFunctionInstruction(Assembly.AppDomain.PresolveMethod(Assembly.File.ExpandMetadataToken(reader.ReadUInt32())), false)); break;
                                case ILExtendedOpcode.LdVirtFtn: AddInstruction(startOfInstruction, new IRLoadFunctionInstruction(Assembly.AppDomain.PresolveMethod(Assembly.File.ExpandMetadataToken(reader.ReadUInt32())), true)); break;
                                case ILExtendedOpcode.LdArg: AddInstruction(startOfInstruction, new IRLoadParameterInstruction(reader.ReadUInt16())); break;
                                case ILExtendedOpcode.LdArgA: AddInstruction(startOfInstruction, new IRLoadParameterAddressInstruction(reader.ReadUInt16())); break;
                                case ILExtendedOpcode.StArg: AddInstruction(startOfInstruction, new IRStoreParameterInstruction(reader.ReadUInt16())); break;
                                case ILExtendedOpcode.LdLoc: AddInstruction(startOfInstruction, new IRLoadLocalInstruction(reader.ReadUInt16())); break;
                                case ILExtendedOpcode.LdLocA: AddInstruction(startOfInstruction, new IRLoadLocalAddressInstruction(reader.ReadUInt16())); break;
                                case ILExtendedOpcode.StLoc: AddInstruction(startOfInstruction, new IRStoreLocalInstruction(reader.ReadUInt16())); break;
                                case ILExtendedOpcode.LocAlloc: AddInstruction(startOfInstruction, new IRStackAllocateInstruction()); break;
                                case ILExtendedOpcode.EndFilter: throw new NotImplementedException("EndFilter");
                                case ILExtendedOpcode.Unaligned__: prefixFlags |= IRPrefixFlags.Unaligned; clearFlags = false; break;
                                case ILExtendedOpcode.Volatile__: prefixFlags |= IRPrefixFlags.Volatile; clearFlags = false; break;
                                case ILExtendedOpcode.Tail__: prefixFlags |= IRPrefixFlags.Tail; clearFlags = false; break;
                                case ILExtendedOpcode.InitObj: AddInstruction(startOfInstruction, new IRInitializeObjectInstruction(Assembly.AppDomain.PresolveType(Assembly.File.ExpandMetadataToken(reader.ReadUInt32())))); break;
                                case ILExtendedOpcode.Constrained__: prefixConstrainedToken = reader.ReadUInt32(); prefixFlags |= IRPrefixFlags.Constrained; clearFlags = false; break;
                                case ILExtendedOpcode.CpBlk: AddInstruction(startOfInstruction, new IRCopyBlockInstruction()); break;
                                case ILExtendedOpcode.InitBlk: AddInstruction(startOfInstruction, new IRInitializeBlockInstruction()); break;
                                case ILExtendedOpcode.No__: prefixFlags |= IRPrefixFlags.No; clearFlags = false; break;
                                case ILExtendedOpcode.ReThrow: throw new NotImplementedException("ReThrow");
                                case ILExtendedOpcode.SizeOf: AddInstruction(startOfInstruction, new IRSizeOfInstruction(Assembly.AppDomain.PresolveType(Assembly.File.ExpandMetadataToken(reader.ReadUInt32())))); break;
                                case ILExtendedOpcode.RefAnyType: AddInstruction(startOfInstruction, new IRLoadTypedReferenceTypeInstruction()); break;
                                case ILExtendedOpcode.ReadOnly__: prefixFlags |= IRPrefixFlags.ReadOnly; clearFlags = false; break;
                            }
                            break;
                        }
                    default: break;
                }
                if (clearFlags)
                {
                    prefixFlags = IRPrefixFlags.None;
                    prefixConstrainedToken = 0;
                }
            }
        }

        public void LinearizeInstructions()
        {
            Stack<IRStackObject> stack = new Stack<IRStackObject>((int)MaximumStackDepth);
            ControlFlowGraph = IRControlFlowGraph.Build(this);
            foreach (IRInstruction instruction in Instructions) instruction.Linearize(stack);
        }

        public void Resolve(GenericParameterCollection typeParams, GenericParameterCollection methodParams)
        {
        }
    }
}
