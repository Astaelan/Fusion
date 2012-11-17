using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.PE
{
    public sealed class DOSHeader
    {
        public ushort Signature;
        public ushort LastPageByteCount;
        public ushort PageCount;
        public ushort RelocationCount;
        public ushort HeaderParagraphCount;
        public ushort MinimumParagraphCount;
        public ushort MaximumParagraphCount;
        public ushort RegisterSS;
        public ushort RegisterSP;
        public ushort Checksum;
        public ushort RegisterIP;
        public ushort RegisterCS;
        public ushort RelocationOffset;
        public ushort Overlay;
        public ushort[] ReservedA = new ushort[4];
        public ushort OEMIdentifier;
        public ushort OEMInformation;
        public ushort[] ReservedB = new ushort[10];
        public uint NextHeaderOffset;

        public bool Read(PEReader pReader)
        {
            if (!pReader.ReadUInt16(ref Signature) ||
                !pReader.ReadUInt16(ref LastPageByteCount) ||
                !pReader.ReadUInt16(ref PageCount) ||
                !pReader.ReadUInt16(ref RelocationCount) ||
                !pReader.ReadUInt16(ref HeaderParagraphCount) ||
                !pReader.ReadUInt16(ref MinimumParagraphCount) ||
                !pReader.ReadUInt16(ref MaximumParagraphCount) ||
                !pReader.ReadUInt16(ref RegisterSS) ||
                !pReader.ReadUInt16(ref RegisterSP) ||
                !pReader.ReadUInt16(ref Checksum) ||
                !pReader.ReadUInt16(ref RegisterIP) ||
                !pReader.ReadUInt16(ref RegisterCS) ||
                !pReader.ReadUInt16(ref RelocationOffset) ||
                !pReader.ReadUInt16(ref Overlay) ||
                !pReader.ReadUInt16(ref ReservedA[0]) ||
                !pReader.ReadUInt16(ref ReservedA[1]) ||
                !pReader.ReadUInt16(ref ReservedA[2]) ||
                !pReader.ReadUInt16(ref ReservedA[3]) ||
                !pReader.ReadUInt16(ref OEMIdentifier) ||
                !pReader.ReadUInt16(ref OEMInformation) ||
                !pReader.ReadUInt16(ref ReservedB[0]) ||
                !pReader.ReadUInt16(ref ReservedB[1]) ||
                !pReader.ReadUInt16(ref ReservedB[2]) ||
                !pReader.ReadUInt16(ref ReservedB[3]) ||
                !pReader.ReadUInt16(ref ReservedB[4]) ||
                !pReader.ReadUInt16(ref ReservedB[5]) ||
                !pReader.ReadUInt16(ref ReservedB[6]) ||
                !pReader.ReadUInt16(ref ReservedB[7]) ||
                !pReader.ReadUInt16(ref ReservedB[8]) ||
                !pReader.ReadUInt16(ref ReservedB[9]) ||
                !pReader.ReadUInt32(ref NextHeaderOffset)) return false;
            return true;
        }
    }
}
