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

        public void Read(PEFile pFile)
        {
            VirtualAddress = pFile.ReadUInt32();
            Size = pFile.ReadUInt32();
        }
    }
}
