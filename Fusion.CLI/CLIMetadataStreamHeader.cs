using Fusion.PE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.CLI
{
    public sealed class CLIMetadataStreamHeader
    {
        public uint Offset;
        public uint Size;
        public string Name;

        public void Read(PEFile pFile)
        {
            Offset = pFile.ReadUInt32();
            Size = pFile.ReadUInt32();
            Name = pFile.ReadString();
            uint headerSize = (uint)(sizeof(uint) * 2 + (Name.Length + 1));
            if ((headerSize & 0x03) != 0) pFile.Cursor += 4 - (headerSize & 0x03);
        }
    }
}
