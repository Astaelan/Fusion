using Fusion.PE;
using Fusion.CLI.Signature;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fusion.CLI.Metadata
{
    public sealed class MethodDefData
    {
        public static void Initialize(CLIFile pFile)
        {
            if ((pFile.CLIMetadataTablesHeader.PresentTables & (1ul << CLIMetadataTables.MethodDef)) != 0)
            {
                pFile.MethodDefTable = new MethodDefData[pFile.ReadInt32()];
                for (int index = 0; index < pFile.MethodDefTable.Length; ++index) pFile.MethodDefTable[index] = new MethodDefData() { CLIFile = pFile, TableIndex = index };
            }
            else pFile.MethodDefTable = new MethodDefData[0];
        }

        public static void Load(CLIFile pFile)
        {
            for (int index = 0; index < pFile.MethodDefTable.Length; ++index) pFile.MethodDefTable[index].LoadData(pFile);
        }

        public static void Link(CLIFile pFile)
        {
            for (int index = 0; index < pFile.MethodDefTable.Length; ++index) pFile.MethodDefTable[index].LinkData(pFile);
        }

        public CLIFile CLIFile = null;

        public int TableIndex = 0;
        public uint RVA = 0;
        public ushort ImplFlags = 0;
        public MethodAttributes Flags = MethodAttributes.None;
        public string Name = null;
        public byte[] Signature = null;
        public int ParamListIndex = 0;
        public List<ParamData> ParamList = new List<ParamData>();

        public TypeDefData ParentTypeDef = null;
        public MethodDefBodyData Body = null;
        public MethodSig ExpandedSignature = null;

        private void LoadData(CLIFile pFile)
        {
            RVA = pFile.ReadUInt32();
            ImplFlags = pFile.ReadUInt16();
            Flags = (MethodAttributes)pFile.ReadUInt16();
            Name = pFile.ReadStringHeap(pFile.ReadHeapIndex(CLIHeapOffsetSize.Strings32Bit));
            Signature = pFile.ReadBlobHeap(pFile.ReadHeapIndex(CLIHeapOffsetSize.Blob32Bit));
            if (pFile.ParamTable.Length >= 0xFFFF) ParamListIndex = pFile.ReadInt32() - 1;
            else ParamListIndex = pFile.ReadUInt16() - 1;

            if (RVA != 0)
            {
                Body = new MethodDefBodyData();
                Body.LoadData(this);
            }
        }

        private void LinkData(CLIFile pFile)
        {
            int paramListCount = pFile.ParamTable.Length - ParamListIndex;
            if (TableIndex < (pFile.MethodDefTable.Length - 1)) paramListCount = pFile.MethodDefTable[TableIndex + 1].ParamListIndex - ParamListIndex;
            for (int index = 0; index < paramListCount; ++index) { ParamList.Add(pFile.ParamTable[ParamListIndex + index]); pFile.ParamTable[ParamListIndex + index].ParentMethodDef = this; }

            int cursor = 0;
            ExpandedSignature = new MethodSig(pFile, Signature, ref cursor);

            if (Body != null) Body.LinkData(this);
        }

        public sealed class MethodDefBodyData
        {
            public ushort Flags = 0;
            public ushort MaxStack = 0;
            public uint CodeSize = 0;
            public uint LocalVarSigToken = 0;
            public uint CodeRVA = 0;

            public StandAloneSigData LocalVarSignature = null;
            public LocalVarSig ExpandedLocalVarSignature = null;

            public void LoadData(MethodDefData pMethodDef)
            {
                SectionHeader sectionHeader = pMethodDef.CLIFile.GetSection(pMethodDef.RVA);
                uint startOfBodyHeader = sectionHeader.PointerToRawData + (pMethodDef.RVA - sectionHeader.VirtualAddress);
                uint sizeOfBodyHeader = 1;
                Flags = (ushort)(pMethodDef.CLIFile.Data[startOfBodyHeader] & 0x03);
                MaxStack = 8;
                CodeSize = (uint)(pMethodDef.CLIFile.Data[startOfBodyHeader] >> 2);
                if (Flags == 0x03)
                {
                    Flags = pMethodDef.CLIFile.Data[startOfBodyHeader];
                    Flags |= (ushort)((pMethodDef.CLIFile.Data[startOfBodyHeader + 1] & 0x0F) << 8);
                    sizeOfBodyHeader = (uint)((pMethodDef.CLIFile.Data[startOfBodyHeader + 1] >> 4) * 4);
                    MaxStack = pMethodDef.CLIFile.Data[startOfBodyHeader + 2];
                    MaxStack |= (ushort)(pMethodDef.CLIFile.Data[startOfBodyHeader + 3] << 8);
                    CodeSize = pMethodDef.CLIFile.Data[startOfBodyHeader + 4];
                    CodeSize |= (uint)(pMethodDef.CLIFile.Data[startOfBodyHeader + 5] << 8);
                    CodeSize |= (uint)(pMethodDef.CLIFile.Data[startOfBodyHeader + 6] << 16);
                    CodeSize |= (uint)(pMethodDef.CLIFile.Data[startOfBodyHeader + 7] << 24);
                    LocalVarSigToken = pMethodDef.CLIFile.Data[startOfBodyHeader + 8];
                    LocalVarSigToken |= (uint)(pMethodDef.CLIFile.Data[startOfBodyHeader + 9] << 8);
                    LocalVarSigToken |= (uint)(pMethodDef.CLIFile.Data[startOfBodyHeader + 10] << 16);
                    LocalVarSigToken |= (uint)(pMethodDef.CLIFile.Data[startOfBodyHeader + 11] << 24);
                }
                CodeRVA = startOfBodyHeader + sizeOfBodyHeader;
            }

            public void LinkData(MethodDefData pMethodDef)
            {
                if (LocalVarSigToken != 0)
                {
                    LocalVarSignature = (StandAloneSigData)pMethodDef.CLIFile.ExpandMetadataToken(LocalVarSigToken).Data;
                    ExpandedLocalVarSignature = LocalVarSignature.ExpandedLocalVarSignature;
                }
            }
        }
    }
}
