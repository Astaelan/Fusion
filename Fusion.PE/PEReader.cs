using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.PE
{
    public sealed class PEReader
    {
        private byte[] mData = null;
        private uint mCursor = 0;

        public PEReader(byte[] pData)
        {
            mData = pData;
        }

        public uint Cursor { get { return mCursor; } set { mCursor = value; } }

        public bool ReadByte(ref byte pValue)
        {
            if (mCursor + 1 >= mData.Length) return false;
            pValue = mData[mCursor];
            mCursor += 1;
            return true;
        }

        public bool ReadSByte(ref sbyte pValue)
        {
            if (mCursor + 1 >= mData.Length) return false;
            pValue = (sbyte)mData[mCursor];
            mCursor += 1;
            return true;
        }

        public bool ReadUInt16(ref ushort pValue)
        {
            if (mCursor + 2 >= mData.Length) return false;
            pValue = mData[mCursor];
            pValue |= (ushort)(mData[mCursor + 1] << 8);
            mCursor += 2;
            return true;
        }

        public bool ReadInt16(ref short pValue)
        {
            if (mCursor + 2 >= mData.Length) return false;
            pValue = mData[mCursor];
            pValue |= (short)(mData[mCursor + 1] << 8);
            mCursor += 2;
            return true;
        }

        public bool ReadUInt32(ref uint pValue)
        {
            if (mCursor + 4 >= mData.Length) return false;
            pValue = mData[mCursor];
            pValue |= (uint)(mData[mCursor + 1] << 8);
            pValue |= (uint)(mData[mCursor + 1] << 16);
            pValue |= (uint)(mData[mCursor + 1] << 24);
            mCursor += 4;
            return true;
        }

        public bool ReadInt32(ref int pValue)
        {
            if (mCursor + 4 >= mData.Length) return false;
            pValue = mData[mCursor];
            pValue |= (int)(mData[mCursor + 1] << 8);
            pValue |= (int)(mData[mCursor + 1] << 16);
            pValue |= (int)(mData[mCursor + 1] << 24);
            mCursor += 4;
            return true;
        }

        public bool ReadString(ref string pValue, uint pLength)
        {
            if (mCursor + pLength >= mData.Length) return false;
            pValue = Encoding.ASCII.GetString(mData, (int)mCursor, (int)pLength);
            mCursor += pLength;
            return true;
        }
    }
}
