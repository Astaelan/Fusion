using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.PE
{
    public sealed class DataDirectory
    {
        public uint VirtualAddress;
        public uint Size;

        public bool Read(PEReader pReader)
        {
            if (!pReader.ReadUInt32(ref VirtualAddress) ||
                !pReader.ReadUInt32(ref Size)) return false;
            return true;
        }
    }
}
