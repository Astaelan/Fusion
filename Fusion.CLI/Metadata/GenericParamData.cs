using Fusion.PE;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fusion.CLI.Metadata
{
    public sealed class GenericParamData
    {
        public static void Initialize(CLIFile pFile)
        {
            if ((pFile.CLIMetadataTablesHeader.PresentTables & (1ul << CLIMetadataTables.GenericParam)) != 0)
            {
                pFile.GenericParamTable = new GenericParamData[pFile.ReadInt32()];
                for (int index = 0; index < pFile.GenericParamTable.Length; ++index) pFile.GenericParamTable[index] = new GenericParamData() { CLIFile = pFile, TableIndex = index };
            }
            else pFile.GenericParamTable = new GenericParamData[0];
        }

        public static void Load(CLIFile pFile)
        {
            for (int index = 0; index < pFile.GenericParamTable.Length; ++index) pFile.GenericParamTable[index].LoadData(pFile);
        }

        public static void Link(CLIFile pFile)
        {
            for (int index = 0; index < pFile.GenericParamTable.Length; ++index) pFile.GenericParamTable[index].LinkData(pFile);
        }

        public CLIFile CLIFile = null;

        public int TableIndex = 0;
        public ushort Number = 0;
        public ushort Flags = 0;
        public TypeOrMethodDefIndex Owner = new TypeOrMethodDefIndex();
        public string Name = null;

        private void LoadData(CLIFile pFile)
        {
            Number = pFile.ReadUInt16();
            Flags = pFile.ReadUInt16();
            Owner.LoadData(pFile);
            Name = pFile.ReadStringHeap(pFile.ReadHeapIndex(CLIHeapOffsetSize.Strings32Bit));
        }

        private void LinkData(CLIFile pFile)
        {
        }
    }
}
