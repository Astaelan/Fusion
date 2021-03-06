﻿namespace Fusion.IL
{
    public enum ILOpcode : byte
    {
        Nop = 0x00,
        Break = 0x01,
        LdArg_0 = 0x02,
        LdArg_1 = 0x03,
        LdArg_2 = 0x04,
        LdArg_3 = 0x05,
        LdLoc_0 = 0x06,
        LdLoc_1 = 0x07,
        LdLoc_2 = 0x08,
        LdLoc_3 = 0x09,
        StLoc_0 = 0x0A,
        StLoc_1 = 0x0B,
        StLoc_2 = 0x0C,
        StLoc_3 = 0x0D,
        LdArg_S = 0x0E,
        LdArgA_S = 0x0F,
        StArg_S = 0x10,
        LdLoc_S = 0x11,
        LdLocA_S = 0x12,
        StLoc_S = 0x13,
        LdNull = 0x14,
        Ldc_I4_M1 = 0x15,
        Ldc_I4_0 = 0x16,
        Ldc_I4_1 = 0x17,
        Ldc_I4_2 = 0x18,
        Ldc_I4_3 = 0x19,
        Ldc_I4_4 = 0x1A,
        Ldc_I4_5 = 0x1B,
        Ldc_I4_6 = 0x1C,
        Ldc_I4_7 = 0x1D,
        Ldc_I4_8 = 0x1E,
        Ldc_I4_S = 0x1F,
        Ldc_I4 = 0x20,
        Ldc_I8 = 0x21,
        Ldc_R4 = 0x22,
        Ldc_R8 = 0x23,
        // N/A						= 0x24,
        Dup = 0x25,
        Pop = 0x26,
        Jmp = 0x27,
        Call = 0x28,
        CallI = 0x29,
        Ret = 0x2A,
        Br_S = 0x2B,
        BrFalse_S = 0x2C,
        BrTrue_S = 0x2D,
        Beq_S = 0x2E,
        Bge_S = 0x2F,
        Bgt_S = 0x30,
        Ble_S = 0x31,
        Blt_S = 0x32,
        Bne_Un_S = 0x33,
        Bge_Un_S = 0x34,
        Bgt_Un_S = 0x35,
        Ble_Un_S = 0x36,
        Blt_Un_S = 0x37,
        Br = 0x38,
        BrFalse = 0x39,
        BrTrue = 0x3A,
        Beq = 0x3B,
        Bge = 0x3C,
        Bgt = 0x3D,
        Ble = 0x3E,
        Blt = 0x3F,
        Bne_Un = 0x40,
        Bge_Un = 0x41,
        Bgt_Un = 0x42,
        Ble_Un = 0x43,
        Blt_Un = 0x44,
        Switch = 0x45,
        LdInd_I1 = 0x46,
        LdInd_U1 = 0x47,
        LdInd_I2 = 0x48,
        LdInd_U2 = 0x49,
        LdInd_I4 = 0x4A,
        LdInd_U4 = 0x4B,
        LdInd_I8 = 0x4C,
        LdInd_I = 0x4D,
        LdInd_R4 = 0x4E,
        LdInd_R8 = 0x4F,
        LdInd_Ref = 0x50,
        StInd_Ref = 0x51,
        StInd_I1 = 0x52,
        StInd_I2 = 0x53,
        StInd_I4 = 0x54,
        StInd_I8 = 0x55,
        StInd_R4 = 0x56,
        StInd_R8 = 0x57,
        Add = 0x58,
        Sub = 0x59,
        Mul = 0x5A,
        Div = 0x5B,
        Div_Un = 0x5C,
        Rem = 0x5D,
        Rem_Un = 0x5E,
        And = 0x5F,
        Or = 0x60,
        Xor = 0x61,
        Shl = 0x62,
        Shr = 0x63,
        Shr_Un = 0x64,
        Neg = 0x65,
        Not = 0x66,
        Conv_I1 = 0x67,
        Conv_I2 = 0x68,
        Conv_I4 = 0x69,
        Conv_I8 = 0x6A,
        Conv_R4 = 0x6B,
        Conv_R8 = 0x6C,
        Conv_U4 = 0x6D,
        Conv_U8 = 0x6E,
        CallVirt = 0x6F,
        CpObj = 0x70,
        LdObj = 0x71,
        LdStr = 0x72,
        NewObj = 0x73,
        CastClass = 0x74,
        IsInst = 0x75,
        Conv_R_Un = 0x76,
        // N/A						= 0x77,
        // N/A						= 0x78,
        Unbox = 0x79,
        Throw = 0x7A,
        LdFld = 0x7B,
        LdFldA = 0x7C,
        StFld = 0x7D,
        LdSFld = 0x7E,
        LdSFldA = 0x7F,
        StSFld = 0x80,
        StObj = 0x81,
        Conv_Ovf_I1_Un = 0x82,
        Conv_Ovf_I2_Un = 0x83,
        Conv_Ovf_I4_Un = 0x84,
        Conv_Ovf_I8_Un = 0x85,
        Conv_Ovf_U1_Un = 0x86,
        Conv_Ovf_U2_Un = 0x87,
        Conv_Ovf_U4_Un = 0x88,
        Conv_Ovf_U8_Un = 0x89,
        Conv_Ovf_I_Un = 0x8A,
        Conv_Ovf_U_Un = 0x8B,
        Box = 0x8C,
        NewArr = 0x8D,
        LdLen = 0x8E,
        LdElemA = 0x8F,
        LdElem_I1 = 0x90,
        LdElem_U1 = 0x91,
        LdElem_I2 = 0x92,
        LdElem_U2 = 0x93,
        LdElem_I4 = 0x94,
        LdElem_U4 = 0x95,
        LdElem_I8 = 0x96,
        LdElem_I = 0x97,
        LdElem_R4 = 0x98,
        LdElem_R8 = 0x99,
        LdElem_Ref = 0x9A,
        StElem_I = 0x9B,
        StElem_I1 = 0x9C,
        StElem_I2 = 0x9D,
        StElem_I4 = 0x9E,
        StElem_I8 = 0x9F,
        StElem_R4 = 0xA0,
        StElem_R8 = 0xA1,
        StElem_Ref = 0xA2,
        LdElem = 0xA3,
        StElem = 0xA4,
        Unbox_Any = 0xA5,
        // N/A						= 0xA6,
        // N/A						= 0xA7,
        // N/A						= 0xA8,
        // N/A						= 0xA9,
        // N/A						= 0xAA,
        // N/A						= 0xAB,
        // N/A						= 0xAC,
        // N/A						= 0xAD,
        // N/A						= 0xAE,
        // N/A						= 0xAF,
        // N/A						= 0xB0,
        // N/A						= 0xB1,
        // N/A						= 0xB2,
        Conv_Ovf_I1 = 0xB3,
        Conv_Ovf_U1 = 0xB4,
        Conv_Ovf_I2 = 0xB5,
        Conv_Ovf_U2 = 0xB6,
        Conv_Ovf_I4 = 0xB7,
        Conv_Ovf_U4 = 0xB8,
        Conv_Ovf_I8 = 0xB9,
        Conv_Ovf_U8 = 0xBA,
        // N/A						= 0xBB,
        // N/A						= 0xBC,
        // N/A						= 0xBD,
        // N/A						= 0xBE,
        // N/A						= 0xBF,
        // N/A						= 0xC0,
        // N/A						= 0xC1,
        RefAnyVal = 0xC2,
        CkFinite = 0xC3,
        // N/A						= 0xC4,
        // N/A						= 0xC5,
        MkRefAny = 0xC6,
        // N/A						= 0xC7,
        // N/A						= 0xC8,
        // N/A						= 0xC9,
        // N/A						= 0xCA,
        // N/A						= 0xCB,
        // N/A						= 0xCC,
        // N/A						= 0xCD,
        // N/A						= 0xCE,
        // N/A						= 0xCF,
        LdToken = 0xD0,
        Conv_U2 = 0xD1,
        Conv_U1 = 0xD2,
        Conv_I = 0xD3,
        Conv_Ovf_I = 0xD4,
        Conv_Ovf_U = 0xD5,
        Add_Ovf = 0xD6,
        Add_Ovf_Un = 0xD7,
        Mul_Ovf = 0xD8,
        Mul_Ovf_Un = 0xD9,
        Sub_Ovf = 0xDA,
        Sub_Ovf_Un = 0xDB,
        EndFinally = 0xDC,
        Leave = 0xDD,
        Leave_S = 0xDE,
        StInd_I = 0xDF,
        Conv_U = 0xE0,
        // N/A						= 0xE1,
        // N/A						= 0xE2,
        // N/A						= 0xE3,
        // N/A						= 0xE4,
        // N/A						= 0xE5,
        // N/A						= 0xE6,
        // N/A						= 0xE7,
        // N/A						= 0xE8,
        // N/A						= 0xE9,
        // N/A						= 0xEA,
        // N/A						= 0xEB,
        // N/A						= 0xEC,
        // N/A						= 0xED,
        // N/A						= 0xEE,
        // N/A						= 0xEF,
        // N/A						= 0xF0,
        // N/A						= 0xF1,
        // N/A						= 0xF2,
        // N/A						= 0xF3,
        // N/A						= 0xF4,
        // N/A						= 0xF5,
        // N/A						= 0xF6,
        // N/A						= 0xF7,
        // N/A						= 0xF8,
        // N/A						= 0xF9,
        // N/A						= 0xFA,
        // N/A						= 0xFB,
        // N/A						= 0xFC,
        // N/A						= 0xFD,
        Extended = 0xFE,
        // N/A						= 0xFF,
    }
}
