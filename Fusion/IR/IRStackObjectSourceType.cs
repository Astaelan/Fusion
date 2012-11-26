namespace Fusion.IR
{
    public enum IRStackObjectSourceType : byte
    {
        Constant,
        Parameter,
        Local,
        PinnedLocal,
        StaticField,
        Stack,
    }
}
