using Fusion.PE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.CLI
{
    public sealed class CLIMetadataTablesHeader
    {
        public uint ReservedA;
        public byte MajorVersion;
        public byte MinorVersion;
        public byte HeapOffsetSizes;
        public byte ReservedB;
        public ulong PresentTables;
        public ulong SortedTables;

        public void Read(PEFile pFile)
        {
            ReservedA = pFile.ReadUInt32();
            MajorVersion = pFile.ReadByte();
            MinorVersion = pFile.ReadByte();
            HeapOffsetSizes = pFile.ReadByte();
            ReservedB = pFile.ReadByte();
            PresentTables = pFile.ReadUInt64();
            SortedTables = pFile.ReadUInt64();
        }
    }
}
