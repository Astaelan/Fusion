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
            pValue |= (uint)(mData[mCursor + 2] << 16);
            pValue |= (uint)(mData[mCursor + 3] << 24);
            mCursor += 4;
            return true;
        }

        public bool ReadInt32(ref int pValue)
        {
            if (mCursor + 4 >= mData.Length) return false;
            pValue = mData[mCursor];
            pValue |= (int)(mData[mCursor + 1] << 8);
            pValue |= (int)(mData[mCursor + 2] << 16);
            pValue |= (int)(mData[mCursor + 3] << 24);
            mCursor += 4;
            return true;
        }

        public bool ReadUInt64(ref ulong pValue)
        {
            if (mCursor + 8 >= mData.Length) return false;
            pValue = mData[mCursor];
            pValue |= (ulong)((ulong)mData[mCursor + 1] << 8);
            pValue |= (ulong)((ulong)mData[mCursor + 2] << 16);
            pValue |= (ulong)((ulong)mData[mCursor + 3] << 24);
            pValue |= (ulong)((ulong)mData[mCursor + 4] << 32);
            pValue |= (ulong)((ulong)mData[mCursor + 5] << 40);
            pValue |= (ulong)((ulong)mData[mCursor + 6] << 48);
            pValue |= (ulong)((ulong)mData[mCursor + 7] << 56);
            mCursor += 8;
            return true;
        }

        public bool ReadInt64(ref long pValue)
        {
            if (mCursor + 8 >= mData.Length) return false;
            pValue = mData[mCursor];
            pValue |= (long)((long)mData[mCursor + 1] << 8);
            pValue |= (long)((long)mData[mCursor + 2] << 16);
            pValue |= (long)((long)mData[mCursor + 3] << 24);
            pValue |= (long)((long)mData[mCursor + 4] << 32);
            pValue |= (long)((long)mData[mCursor + 5] << 40);
            pValue |= (long)((long)mData[mCursor + 6] << 48);
            pValue |= (long)((long)mData[mCursor + 7] << 56);
            mCursor += 8;
            return true;
        }

        public bool ReadBytes(ref byte[] pValue, uint pLength)
        {
            if (mCursor + pLength >= mData.Length) return false;
            pValue = new byte[pLength];
            Buffer.BlockCopy(mData, (int)mCursor, pValue, 0, (int)pLength);
            mCursor += pLength;
            return true;
        }

        public bool ReadString(ref string pValue)
        {
            uint length = 0;
            while (mData[mCursor + length] != 0x00)
            {
                ++length;
                if (length >= mData.Length) return false;
            }
            if (length == 0) pValue = "";
            else pValue = Encoding.ASCII.GetString(mData, (int)mCursor, (int)length);
            mCursor += length + 1;
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
