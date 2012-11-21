namespace System
{
    public struct Decimal
    {

        // internal representation of decimal
        private uint mFlags;
        private uint mHigh;
        private uint mLow;
        private uint mMiddle;

        public static int[] GetBits(Decimal d)
        {
            return new int[] { 0, 0, 0, 0 };
        }

    }
}
