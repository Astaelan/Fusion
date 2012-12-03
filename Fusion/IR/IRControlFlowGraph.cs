using Fusion.IR.Instructions;
using System;
using System.Collections.Generic;

namespace Fusion.IR
{
    public sealed class IRControlFlowGraph
    {
        public sealed class Node
        {
            public uint Index = 0;
            public List<IRInstruction> Instructions = new List<IRInstruction>();
        }

        public List<Node> Nodes = new List<Node>();

        public static IRControlFlowGraph Build(IRMethod pMethod)
        {
            if (pMethod.Instructions.Count == 0) return null;

            HashSet<IRInstruction> nodeBreaks = new HashSet<IRInstruction>();
            foreach (IRInstruction instruction in pMethod.Instructions)
            {
                switch (instruction.Opcode)
                {
                    case IROpcode.Branch:
                        {
                            IRBranchInstruction branchInstruction = (IRBranchInstruction)instruction;
                            if (!nodeBreaks.Contains(instruction)) nodeBreaks.Add(instruction);
                            if (!nodeBreaks.Contains(branchInstruction.TargetIRInstruction)) nodeBreaks.Add(branchInstruction.TargetIRInstruction);
                            break;
                        }
                    case IROpcode.Switch:
                        {
                            IRSwitchInstruction switchInstruction = (IRSwitchInstruction)instruction;
                            if (!nodeBreaks.Contains(instruction)) nodeBreaks.Add(instruction);
                            foreach (IRInstruction targetIRInstruction in switchInstruction.TargetIRInstructions)
                            {
                                if (!nodeBreaks.Contains(targetIRInstruction))
                                {
                                    nodeBreaks.Add(targetIRInstruction);
                                }
                            }
                            break;
                        }
                    case IROpcode.Leave:
                        {
                            IRLeaveInstruction leaveInstruction = (IRLeaveInstruction)instruction;
                            if (!nodeBreaks.Contains(instruction)) nodeBreaks.Add(instruction);
                            if (!nodeBreaks.Contains(leaveInstruction.TargetIRInstruction)) nodeBreaks.Add(leaveInstruction.TargetIRInstruction);
                            break;
                        }
                    default: break;
                }
            }

            IRControlFlowGraph cfg = new IRControlFlowGraph();
            foreach (IRInstruction instruction in pMethod.Instructions)
            {
            }
            return cfg;
        }
    }
}
