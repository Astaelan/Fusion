using Fusion.IR.Instructions;
using System;
using System.Collections.Generic;

namespace Fusion.IR
{
    public sealed class IRControlFlowGraph
    {

        public static IRControlFlowGraph Build(IRMethod pMethod)
        {
            IRControlFlowGraph cfg = new IRControlFlowGraph();

            HashSet<uint> branchTargets = new HashSet<uint>();
            foreach (IRInstruction instruction in pMethod.Instructions)
            {
                switch (instruction.Opcode)
                {
                    case IROpcode.Branch:
                        {
                            IRBranchInstruction branchInstruction = (IRBranchInstruction)instruction;
                            if (!branchTargets.Contains(branchInstruction.TargetILOffset)) branchTargets.Add(branchInstruction.TargetILOffset);
                            break;
                        }
                    case IROpcode.Switch:
                        {
                            IRSwitchInstruction switchInstruction = (IRSwitchInstruction)instruction;
                            foreach (uint targetILOffset in switchInstruction.TargetILOffsets)
                            {
                                if (!branchTargets.Contains(targetILOffset)) branchTargets.Add(targetILOffset);
                            }
                            break;
                        }
                    case IROpcode.Leave:
                        {
                            IRLeaveInstruction leaveInstruction = (IRLeaveInstruction)instruction;
                            if (!branchTargets.Contains(leaveInstruction.TargetILOffset)) branchTargets.Add(leaveInstruction.TargetILOffset);
                            break;
                        }
                    default: break;
                }
            }

            return cfg;
        }
    }
}
