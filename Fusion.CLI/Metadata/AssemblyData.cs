using Fusion.PE;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fusion.CLI.Metadata
{
    public sealed class AssemblyData
    {
        public static void Initialize(CLIFile pFile)
        {
            if ((pFile.CLIMetadataTablesHeader.PresentTables & (1ul << CLIMetadataTables.Assembly)) != 0)
            {
                pFile.AssemblyTable = new AssemblyData[pFile.ReadInt32()];
                for (int index = 0; index < pFile.AssemblyTable.Length; ++index) pFile.AssemblyTable[index] = new AssemblyData() { CLIFile = pFile, TableIndex = index };
            }
            else pFile.AssemblyTable = new AssemblyData[0];
        }

        public static void Load(CLIFile pFile)
        {
            for (int index = 0; index < pFile.AssemblyTable.Length; ++index) pFile.AssemblyTable[index].LoadData(pFile);
        }

        public static void Link(CLIFile pFile)
        {
            for (int index = 0; index < pFile.AssemblyTable.Length; ++index) pFile.AssemblyTable[index].LinkData(pFile);
        }

        public CLIFile CLIFile = null;

        public int TableIndex = 0;
        public uint HashAlgId = 0;
        public ushort MajorVersion = 0;
        public ushort MinorVersion = 0;
        public ushort BuildNumber = 0;
        public ushort RevisionNumber = 0;
        public uint Flags = 0;
        public byte[] PublicKey = null;
        public string Name = null;
        public string Culture = null;

        private void LoadData(CLIFile pFile)
        {
            HashAlgId = pFile.ReadUInt32();
            MajorVersion = pFile.ReadUInt16();
            MinorVersion = pFile.ReadUInt16();
            BuildNumber = pFile.ReadUInt16();
            RevisionNumber = pFile.ReadUInt16();
            Flags = pFile.ReadUInt32();
            PublicKey = pFile.ReadBlobHeap(pFile.ReadHeapIndex(CLIHeapOffsetSize.Blob32Bit));
            Name = pFile.ReadStringHeap(pFile.ReadHeapIndex(CLIHeapOffsetSize.Strings32Bit));
            Culture = pFile.ReadStringHeap(pFile.ReadHeapIndex(CLIHeapOffsetSize.Strings32Bit));
        }

        private void LinkData(CLIFile pFile)
        {
        }
    }
}
