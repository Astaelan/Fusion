using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.PE
{
    public sealed class CLIMetadataHeader
    {
        public uint Signature;
        public ushort MajorVersion;
        public ushort MinorVersion;
        public uint Reserved;
        public uint VersionLength;
        public byte[] Version;
        public ushort Flags;
        public ushort StreamCount;
        public CLIMetadataStreamHeader[] Streams;

        public bool Read(PEReader pReader)
        {
            if (!pReader.ReadUInt32(ref Signature) ||
                !pReader.ReadUInt16(ref MajorVersion) ||
                !pReader.ReadUInt16(ref MinorVersion) ||
                !pReader.ReadUInt32(ref Reserved) ||
                !pReader.ReadUInt32(ref VersionLength) ||
                !pReader.ReadBytes(ref Version, VersionLength) ||
                !pReader.ReadUInt16(ref Flags) ||
                !pReader.ReadUInt16(ref StreamCount)) return false;
            Streams = new CLIMetadataStreamHeader[StreamCount];
            for (int index = 0; index < Streams.Length; ++index)
            {
                Streams[index] = new CLIMetadataStreamHeader();
                if (!Streams[index].Read(pReader)) return false;
            }
            return true;
        }
    }
}
