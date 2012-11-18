using Fusion.PE;
using System;
using System.Collections.Generic;
using System.Text;
using Fusion.CLI.Signature;

namespace Fusion.CLI.Metadata
{
    public sealed class PropertyData
    {
        public static void Initialize(CLIFile pFile)
        {
            if ((pFile.CLIMetadataTablesHeader.PresentTables & (1ul << CLIMetadataTables.Property)) != 0)
            {
                pFile.PropertyTable = new PropertyData[pFile.ReadInt32()];
                for (int index = 0; index < pFile.PropertyTable.Length; ++index) pFile.PropertyTable[index] = new PropertyData() { CLIFile = pFile, TableIndex = index };
            }
            else pFile.PropertyTable = new PropertyData[0];
        }

        public static void Load(CLIFile pFile)
        {
            for (int index = 0; index < pFile.PropertyTable.Length; ++index) pFile.PropertyTable[index].LoadData(pFile);
        }

        public static void Link(CLIFile pFile)
        {
            for (int index = 0; index < pFile.PropertyTable.Length; ++index) pFile.PropertyTable[index].LinkData(pFile);
        }

        public CLIFile CLIFile = null;

        public int TableIndex = 0;
        public ushort Flags = 0;
        public string Name = null;
        public byte[] Type = null;

        public PropertyMapData ParentPropertyMap = null;
        public PropertySig ExpandedType = null;

        private void LoadData(CLIFile pFile)
        {
            Flags = pFile.ReadUInt16();
            Name = pFile.ReadStringHeap(pFile.ReadHeapIndex(CLIHeapOffsetSize.Strings32Bit));
            Type = pFile.ReadBlobHeap(pFile.ReadHeapIndex(CLIHeapOffsetSize.Blob32Bit));
        }

        private void LinkData(CLIFile pFile)
        {
            int cursor = 0;
            ExpandedType = new PropertySig(pFile, Type, ref cursor);
        }
    }
}
