using Fusion.PE;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fusion.CLI.Metadata
{
    public sealed class CustomAttributeData
    {
        public static void Initialize(CLIFile pFile)
        {
            if ((pFile.CLIMetadataTablesHeader.PresentTables & (1ul << CLIMetadataTables.CustomAttribute)) != 0)
            {
                pFile.CustomAttributeTable = new CustomAttributeData[pFile.ReadInt32()];
                for (int index = 0; index < pFile.CustomAttributeTable.Length; ++index) pFile.CustomAttributeTable[index] = new CustomAttributeData() { CLIFile = pFile, TableIndex = index };
            }
            else pFile.CustomAttributeTable = new CustomAttributeData[0];
        }

        public static void Load(CLIFile pFile)
        {
            for (int index = 0; index < pFile.CustomAttributeTable.Length; ++index) pFile.CustomAttributeTable[index].LoadData(pFile);
        }

        public static void Link(CLIFile pFile)
        {
            for (int index = 0; index < pFile.CustomAttributeTable.Length; ++index) pFile.CustomAttributeTable[index].LinkData(pFile);
        }

        public CLIFile CLIFile = null;

        public int TableIndex = 0;
        public HasCustomAttributeIndex Parent = new HasCustomAttributeIndex();
        public CustomAttributeTypeIndex Type = new CustomAttributeTypeIndex();
        public byte[] Value = null;

        private void LoadData(CLIFile pFile)
        {
            Parent.LoadData(pFile);
            Type.LoadData(pFile);
            Value = pFile.ReadBlobHeap(pFile.ReadHeapIndex(CLIHeapOffsetSize.Blob32Bit));
        }

        private void LinkData(CLIFile pFile)
        {
        }
    }
}
