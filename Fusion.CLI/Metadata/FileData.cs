﻿using Fusion.PE;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fusion.CLI.Metadata
{
    public sealed class FileData
    {
        public static void Initialize(CLIFile pFile)
        {
            if ((pFile.CLIMetadataTablesHeader.PresentTables & (1ul << CLIMetadataTables.File)) != 0)
            {
                pFile.FileTable = new FileData[pFile.ReadInt32()];
                for (int index = 0; index < pFile.FileTable.Length; ++index) pFile.FileTable[index] = new FileData() { CLIFile = pFile, TableIndex = index };
            }
            else pFile.FileTable = new FileData[0];
        }

        public static void Load(CLIFile pFile)
        {
            for (int index = 0; index < pFile.FileTable.Length; ++index) pFile.FileTable[index].LoadData(pFile);
        }

        public static void Link(CLIFile pFile)
        {
            for (int index = 0; index < pFile.FileTable.Length; ++index) pFile.FileTable[index].LinkData(pFile);
        }

        public CLIFile CLIFile = null;

        public int TableIndex = 0;
        public uint Flags = 0;
        public string Name = null;
        public byte[] HashValue = null;

        private void LoadData(CLIFile pFile)
        {
            Flags = pFile.ReadUInt32();
            Name = pFile.ReadStringHeap(pFile.ReadHeapIndex(CLIHeapOffsetSize.Strings32Bit));
            HashValue = pFile.ReadBlobHeap(pFile.ReadHeapIndex(CLIHeapOffsetSize.Blob32Bit));
        }

        private void LinkData(CLIFile pFile)
        {
        }
    }
}
