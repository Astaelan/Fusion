using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.PE
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

        public bool Read(PEReader pReader)
        {
            if (!pReader.ReadUInt32(ref ReservedA) ||
                !pReader.ReadByte(ref MajorVersion) ||
                !pReader.ReadByte(ref MinorVersion) ||
                !pReader.ReadByte(ref HeapOffsetSizes) ||
                !pReader.ReadByte(ref ReservedB) ||
                !pReader.ReadUInt64(ref PresentTables) ||
                !pReader.ReadUInt64(ref SortedTables)) return false;
            return true;
        }
    }
}
