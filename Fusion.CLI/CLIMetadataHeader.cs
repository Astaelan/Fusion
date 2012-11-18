using Fusion.PE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.CLI
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

        public bool Read(PEFile pFile)
        {
            Signature = pFile.ReadUInt32();
            MajorVersion = pFile.ReadUInt16();
            MinorVersion = pFile.ReadUInt16();
            Reserved = pFile.ReadUInt32();
            VersionLength = pFile.ReadUInt32();
            Version = pFile.ReadBytes(VersionLength);
            Flags = pFile.ReadUInt16();
            StreamCount = pFile.ReadUInt16();

            Streams = new CLIMetadataStreamHeader[StreamCount];
            for (int index = 0; index < Streams.Length; ++index)
            {
                Streams[index] = new CLIMetadataStreamHeader();
                Streams[index].Read(pFile);
            }
            return true;
        }
    }
}
