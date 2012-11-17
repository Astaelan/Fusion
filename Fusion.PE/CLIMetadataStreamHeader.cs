using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.PE
{
    public sealed class CLIMetadataStreamHeader
    {
        public uint Offset;
        public uint Size;
        public string Name;

        public bool Read(PEReader pReader)
        {
            if (!pReader.ReadUInt32(ref Offset) ||
                !pReader.ReadUInt32(ref Size) ||
                !pReader.ReadString(ref Name)) return false;
            uint headerSize = (uint)(sizeof(uint) * 2 + (Name.Length + 1));
            if ((headerSize & 0x03) != 0) pReader.Cursor += 4 - (headerSize & 0x03);
            return true;
        }
    }
}
