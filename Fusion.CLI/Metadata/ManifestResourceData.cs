﻿using Fusion.PE;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fusion.CLI.Metadata
{
    public sealed class ManifestResourceData
    {
        public static void Initialize(CLIFile pFile)
        {
            if ((pFile.CLIMetadataTablesHeader.PresentTables & (1ul << CLIMetadataTables.ManifestResource)) != 0)
            {
                pFile.ManifestResourceTable = new ManifestResourceData[pFile.ReadInt32()];
                for (int index = 0; index < pFile.ManifestResourceTable.Length; ++index) pFile.ManifestResourceTable[index] = new ManifestResourceData() { CLIFile = pFile, TableIndex = index };
            }
            else pFile.ManifestResourceTable = new ManifestResourceData[0];
        }

        public static void Load(CLIFile pFile)
        {
            for (int index = 0; index < pFile.ManifestResourceTable.Length; ++index) pFile.ManifestResourceTable[index].LoadData(pFile);
        }

        public static void Link(CLIFile pFile)
        {
            for (int index = 0; index < pFile.ManifestResourceTable.Length; ++index) pFile.ManifestResourceTable[index].LinkData(pFile);
        }

        public CLIFile CLIFile = null;

        public int TableIndex = 0;
        public uint Offset = 0;
        public uint Flags = 0;
        public string Name = null;
        public ImplementationIndex Implementation = new ImplementationIndex();

        private void LoadData(CLIFile pFile)
        {
            Offset = pFile.ReadUInt32();
            Flags = pFile.ReadUInt32();
            Name = pFile.ReadStringHeap(pFile.ReadHeapIndex(CLIHeapOffsetSize.Strings32Bit));
            Implementation.LoadData(pFile);
        }

        private void LinkData(CLIFile pFile)
        {
        }
    }
}
