using Fusion.CLI.Metadata;
using Fusion.PE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusion.CLI
{
    public sealed class CLIFile : PEFile
    {
        public string ReferenceName = null;
        public CLIHeader CLIHeader = new CLIHeader();
        public uint CLIMetadataHeaderOffset = 0;
        public CLIMetadataHeader CLIMetadataHeader = new CLIMetadataHeader();
        public CLIMetadataStreamHeader TablesStream = null;
        public CLIMetadataStreamHeader StringsStream = null;
        public CLIMetadataStreamHeader USStream = null;
        public CLIMetadataStreamHeader GUIDStream = null;
        public CLIMetadataStreamHeader BlobStream = null;
        public CLIMetadataTablesHeader CLIMetadataTablesHeader = new CLIMetadataTablesHeader();


        private delegate void CLIFileDelegate(CLIFile pFile);
        private class MetadataLoader
        {
            public CLIFileDelegate Initializer;
            public CLIFileDelegate Loader;
            public CLIFileDelegate Linker;
            public MetadataLoader(CLIFileDelegate pInitializer, CLIFileDelegate pLoader, CLIFileDelegate pLinker) { Initializer = pInitializer; Loader = pLoader; Linker = pLinker; }
        }
        private static MetadataLoader[] sMetadataLoaders = new MetadataLoader[]
        {
            new MetadataLoader(ModuleData.Initialize, ModuleData.Load, ModuleData.Link),
            new MetadataLoader(TypeRefData.Initialize, TypeRefData.Load, TypeRefData.Link),
            new MetadataLoader(TypeDefData.Initialize, TypeDefData.Load, TypeDefData.Link),
            new MetadataLoader(FieldData.Initialize, FieldData.Load, FieldData.Link),
            new MetadataLoader(MethodDefData.Initialize, MethodDefData.Load, MethodDefData.Link),
            new MetadataLoader(ParamData.Initialize, ParamData.Load, ParamData.Link),
            new MetadataLoader(InterfaceImplData.Initialize, InterfaceImplData.Load, InterfaceImplData.Link),
            new MetadataLoader(MemberRefData.Initialize, MemberRefData.Load, MemberRefData.Link),
            new MetadataLoader(ConstantData.Initialize, ConstantData.Load, ConstantData.Link),
            new MetadataLoader(CustomAttributeData.Initialize, CustomAttributeData.Load, CustomAttributeData.Link),
            new MetadataLoader(FieldMarshalData.Initialize, FieldMarshalData.Load, FieldMarshalData.Link),
            new MetadataLoader(DeclSecurityData.Initialize, DeclSecurityData.Load, DeclSecurityData.Link),
            new MetadataLoader(ClassLayoutData.Initialize, ClassLayoutData.Load, ClassLayoutData.Link),
            new MetadataLoader(FieldLayoutData.Initialize, FieldLayoutData.Load, FieldLayoutData.Link),
            new MetadataLoader(StandAloneSigData.Initialize, StandAloneSigData.Load, StandAloneSigData.Link),
            new MetadataLoader(EventMapData.Initialize, EventMapData.Load, EventMapData.Link),
            new MetadataLoader(EventData.Initialize, EventData.Load, EventData.Link),
            new MetadataLoader(PropertyMapData.Initialize, PropertyMapData.Load, PropertyMapData.Link),
            new MetadataLoader(PropertyData.Initialize, PropertyData.Load, PropertyData.Link),
            new MetadataLoader(MethodSemanticsData.Initialize, MethodSemanticsData.Load, MethodSemanticsData.Link),
            new MetadataLoader(MethodImplData.Initialize, MethodImplData.Load, MethodImplData.Link),
            new MetadataLoader(ModuleRefData.Initialize, ModuleRefData.Load, ModuleRefData.Link),
            new MetadataLoader(TypeSpecData.Initialize, TypeSpecData.Load, TypeSpecData.Link),
            new MetadataLoader(ImplMapData.Initialize, ImplMapData.Load, ImplMapData.Link),
            new MetadataLoader(FieldRVAData.Initialize, FieldRVAData.Load, FieldRVAData.Link),
            new MetadataLoader(AssemblyData.Initialize, AssemblyData.Load, AssemblyData.Link),
            new MetadataLoader(AssemblyProcessorData.Initialize, AssemblyProcessorData.Load, AssemblyProcessorData.Link),
            new MetadataLoader(AssemblyOSData.Initialize, AssemblyOSData.Load, AssemblyOSData.Link),
            new MetadataLoader(AssemblyRefData.Initialize, AssemblyRefData.Load, AssemblyRefData.Link),
            new MetadataLoader(AssemblyRefProcessorData.Initialize, AssemblyRefProcessorData.Load, AssemblyRefProcessorData.Link),
            new MetadataLoader(AssemblyRefOSData.Initialize, AssemblyRefOSData.Load, AssemblyRefOSData.Link),
            new MetadataLoader(FileData.Initialize, FileData.Load, FileData.Link),
            new MetadataLoader(ExportedTypeData.Initialize, ExportedTypeData.Load, ExportedTypeData.Link),
            new MetadataLoader(ManifestResourceData.Initialize, ManifestResourceData.Load, ManifestResourceData.Link),
            new MetadataLoader(NestedClassData.Initialize, NestedClassData.Load, NestedClassData.Link),
            new MetadataLoader(GenericParamData.Initialize, GenericParamData.Load, GenericParamData.Link),
            new MetadataLoader(MethodSpecData.Initialize, MethodSpecData.Load, MethodSpecData.Link),
            new MetadataLoader(GenericParamConstraintData.Initialize, GenericParamConstraintData.Load, GenericParamConstraintData.Link)
        };

        public ModuleData[] ModuleTable = null;
        public TypeRefData[] TypeRefTable = null;
        public TypeDefData[] TypeDefTable = null;
        public FieldData[] FieldTable = null;
        public MethodDefData[] MethodDefTable = null;
        public ParamData[] ParamTable = null;
        public InterfaceImplData[] InterfaceImplTable = null;
        public MemberRefData[] MemberRefTable = null;
        public ConstantData[] ConstantTable = null;
        public CustomAttributeData[] CustomAttributeTable = null;
        public FieldMarshalData[] FieldMarshalTable = null;
        public DeclSecurityData[] DeclSecurityTable = null;
        public ClassLayoutData[] ClassLayoutTable = null;
        public FieldLayoutData[] FieldLayoutTable = null;
        public StandAloneSigData[] StandAloneSigTable = null;
        public EventMapData[] EventMapTable = null;
        public EventData[] EventTable = null;
        public PropertyMapData[] PropertyMapTable = null;
        public PropertyData[] PropertyTable = null;
        public MethodSemanticsData[] MethodSemanticsTable = null;
        public MethodImplData[] MethodImplTable = null;
        public ModuleRefData[] ModuleRefTable = null;
        public TypeSpecData[] TypeSpecTable = null;
        public ImplMapData[] ImplMapTable = null;
        public FieldRVAData[] FieldRVATable = null;
        public AssemblyData[] AssemblyTable = null;
        public AssemblyProcessorData[] AssemblyProcessorTable = null;
        public AssemblyOSData[] AssemblyOSTable = null;
        public AssemblyRefData[] AssemblyRefTable = null;
        public AssemblyRefProcessorData[] AssemblyRefProcessorTable = null;
        public AssemblyRefOSData[] AssemblyRefOSTable = null;
        public FileData[] FileTable = null;
        public ExportedTypeData[] ExportedTypeTable = null;
        public ManifestResourceData[] ManifestResourceTable = null;
        public NestedClassData[] NestedClassTable = null;
        public GenericParamData[] GenericParamTable = null;
        public MethodSpecData[] MethodSpecTable = null;
        public GenericParamConstraintData[] GenericParamConstraintTable = null;

        public CLIFile(string pReferenceName, byte[] pData) : base(pData) { ReferenceName = pReferenceName; }

        public override void Load()
        {
            base.Load();

            DataDirectory headerDataDirectory = OptionalHeader.DataDirectories[14];
            SectionHeader headerSectionHeader = GetSection(headerDataDirectory.VirtualAddress);
            Cursor = headerSectionHeader.PointerToRawData + (headerDataDirectory.VirtualAddress - headerSectionHeader.VirtualAddress);
            CLIHeader.Read(this);
            SectionHeader metadataSectionHeader = GetSection(CLIHeader.Metadata.VirtualAddress);
            CLIMetadataHeaderOffset = metadataSectionHeader.PointerToRawData + (CLIHeader.Metadata.VirtualAddress - metadataSectionHeader.VirtualAddress);
            Cursor = CLIMetadataHeaderOffset;
            CLIMetadataHeader.Read(this);
            Cursor = CLIMetadataHeaderOffset;
            foreach (CLIMetadataStreamHeader streamHeader in CLIMetadataHeader.Streams)
            {
                switch (streamHeader.Name)
                {
                    case "#~": TablesStream = streamHeader; break;
                    case "#Strings": StringsStream = streamHeader; break;
                    case "#US": USStream = streamHeader; break;
                    case "#GUID": GUIDStream = streamHeader; break;
                    case "#Blob": BlobStream = streamHeader; break;
                    default: throw new BadImageFormatException("Invalid CLIMetadataHeader Stream");
                }
            }
            Cursor = CLIMetadataHeaderOffset + TablesStream.Offset;
            CLIMetadataTablesHeader.Read(this);

            Array.ForEach(sMetadataLoaders, l => l.Initializer(this));
            Array.ForEach(sMetadataLoaders, l => l.Loader(this));
            Array.ForEach(sMetadataLoaders, l => l.Linker(this));
        }

        public int ReadHeapIndex(byte p32BitFlag)
        {
            int heapIndex = 0;
            if ((CLIMetadataTablesHeader.HeapOffsetSizes & p32BitFlag) != 0) heapIndex = ReadInt32();
            else heapIndex = ReadInt16();
            return heapIndex;
        }

        public static uint ReadCompressedUnsigned(byte[] pBuffer, ref int pOffset)
        {
            uint value = 0;
            if ((pBuffer[pOffset] & 0x80) == 0)
            {
                value = (uint)(pBuffer[pOffset] & 0x7F);
                pOffset += 1;
                return value;
            }
            if ((pBuffer[pOffset] & 0xC0) == 0x80)
            {
                value = (uint)(((pBuffer[pOffset] & 0x3F) << 8) + pBuffer[pOffset + 1]);
                pOffset += 2;
                return value;
            }
            if ((pBuffer[pOffset] & 0xE0) == 0xC0)
            {
                value = (uint)(((pBuffer[pOffset] & 0x1F) << 24) + (pBuffer[pOffset + 1] << 16) + (pBuffer[pOffset + 2] << 8) + pBuffer[pOffset + 3]);
                pOffset += 4;
                return value;
            }
            value = 0;
            return value;
        }

        public static int ReadCompressedSigned(byte[] pBuffer, ref int pOffset)
        {
            uint value = 0;
            if ((pBuffer[pOffset] & 0x80) == 0)
            {
                value = (uint)(pBuffer[pOffset] & 0x7F);
                value = RotateRight(value, 7);
                if ((value & 0x40) != 0) value |= 0xFFFFFF80;
                pOffset += 1;
                return (int)value;
            }
            if ((pBuffer[pOffset] & 0xC0) == 0x80)
            {
                value = (uint)(((pBuffer[pOffset] & 0x3F) << 8) + pBuffer[pOffset + 1]);
                value = RotateRight(value, 14);
                if ((value & 0x2000) != 0) value |= 0xFFFFC000;
                pOffset += 2;
                return (int)value;
            }
            if ((pBuffer[pOffset] & 0xE0) == 0xC0)
            {
                value = (uint)(((pBuffer[pOffset] & 0x1F) << 24) + (pBuffer[pOffset + 1] << 16) + (pBuffer[pOffset + 2] << 8) + pBuffer[pOffset + 3]);
                value = RotateRight(value, 29);
                if ((value & 0x10000000) != 0) value |= 0xE0000000;
                pOffset += 4;
                return (int)value;
            }
            value = 0;
            return (int)value;
        }

        private static uint RotateRight(uint pValue, byte pBits)
        {
            bool bit = (pValue & 0x01) != 0;
            pValue >>= 1;
            if (bit) pValue |= (uint)(1 << (pBits - 1));
            return pValue;
        }

        public string ReadStringHeap(int pHeapOffset)
        {
            int offset = (int)(CLIMetadataHeaderOffset + StringsStream.Offset + pHeapOffset);
            int length = 0;
            while (Data[offset + length] != 0x00) ++length;
            return Encoding.ASCII.GetString(Data, offset, length);
        }

        public string ReadUserStringHeap(int pHeapOffset)
        {
            int offset = (int)(CLIMetadataHeaderOffset + USStream.Offset + pHeapOffset);
            uint length = ReadCompressedUnsigned(Data, ref offset);
            if (length == 0 || length == 1) return "";
            --length;
            return Encoding.Unicode.GetString(Data, offset, (int)length);
        }

        public byte[] ReadBlobHeap(int pHeapOffset)
        {
            int offset = (int)(CLIMetadataHeaderOffset + BlobStream.Offset + pHeapOffset);
            uint length = ReadCompressedUnsigned(Data, ref offset);
            byte[] blob = new byte[length];
            Buffer.BlockCopy(Data, offset, blob, 0, (int)length);
            return blob;
        }

        public byte[] ReadGUIDHeap(int pHeapOffset)
        {
            int offset = (int)(CLIMetadataHeaderOffset + GUIDStream.Offset + pHeapOffset);
            byte[] guid = new byte[8];
            Buffer.BlockCopy(Data, offset, guid, 0, 8);
            return guid;
        }

        public MetadataToken ExpandMetadataToken(uint pToken)
        {
            MetadataToken token = new MetadataToken();
            token.Table = (byte)(pToken >> 24);
            token.IsUserString = token.Table == CLIMetadataTables.UserStrings;
            uint index = pToken & 0x00FFFFFF;
            if (index == 0) return token;
            if (token.IsUserString)
            {
                token.Data = ReadUserStringHeap((int)index);
                return token;
            }
            --index;
            switch (token.Table)
            {
                case CLIMetadataTables.Module: token.Data = ModuleTable[index]; break;
                case CLIMetadataTables.TypeRef: token.Data = TypeDefTable[index]; break;
                case CLIMetadataTables.TypeDef: token.Data = TypeDefTable[index]; break;
                case CLIMetadataTables.Field: token.Data = FieldTable[index]; break;
                case CLIMetadataTables.MethodDef: token.Data = MethodDefTable[index]; break;
                case CLIMetadataTables.Param: token.Data = ParamTable[index]; break;
                case CLIMetadataTables.InterfaceImpl: token.Data = InterfaceImplTable[index]; break;
                case CLIMetadataTables.MemberRef: token.Data = MemberRefTable[index]; break;
                case CLIMetadataTables.Constant: token.Data = ConstantTable[index]; break;
                case CLIMetadataTables.CustomAttribute: token.Data = CustomAttributeTable[index]; break;
                case CLIMetadataTables.FieldMarshal: token.Data = FieldMarshalTable[index]; break;
                case CLIMetadataTables.DeclSecurity: token.Data = DeclSecurityTable[index]; break;
                case CLIMetadataTables.ClassLayout: token.Data = ClassLayoutTable[index]; break;
                case CLIMetadataTables.FieldLayout: token.Data = FieldLayoutTable[index]; break;
                case CLIMetadataTables.StandAloneSig: token.Data = StandAloneSigTable[index]; break;
                case CLIMetadataTables.EventMap: token.Data = EventMapTable[index]; break;
                case CLIMetadataTables.Event: token.Data = EventTable[index]; break;
                case CLIMetadataTables.PropertyMap: token.Data = PropertyMapTable[index]; break;
                case CLIMetadataTables.Property: token.Data = PropertyTable[index]; break;
                case CLIMetadataTables.MethodSemantics: token.Data = MethodSemanticsTable[index]; break;
                case CLIMetadataTables.MethodImpl: token.Data = MethodImplTable[index]; break;
                case CLIMetadataTables.ModuleRef: token.Data = ModuleRefTable[index]; break;
                case CLIMetadataTables.TypeSpec: token.Data = TypeSpecTable[index]; break;
                case CLIMetadataTables.ImplMap: token.Data = ImplMapTable[index]; break;
                case CLIMetadataTables.FieldRVA: token.Data = FieldRVATable[index]; break;
                case CLIMetadataTables.Assembly: token.Data = AssemblyTable[index]; break;
                case CLIMetadataTables.AssemblyProcessor: token.Data = AssemblyProcessorTable[index]; break;
                case CLIMetadataTables.AssemblyOS: token.Data = AssemblyOSTable[index]; break;
                case CLIMetadataTables.AssemblyRef: token.Data = AssemblyRefTable[index]; break;
                case CLIMetadataTables.AssemblyRefProcessor: token.Data = AssemblyRefProcessorTable[index]; break;
                case CLIMetadataTables.AssemblyRefOS: token.Data = AssemblyRefOSTable[index]; break;
                case CLIMetadataTables.File: token.Data = FileTable[index]; break;
                case CLIMetadataTables.ExportedType: token.Data = ExportedTypeTable[index]; break;
                case CLIMetadataTables.ManifestResource: token.Data = ManifestResourceTable[index]; break;
                case CLIMetadataTables.NestedClass: token.Data = NestedClassTable[index]; break;
                case CLIMetadataTables.GenericParam: token.Data = GenericParamTable[index]; break;
                case CLIMetadataTables.MethodSpec: token.Data = MethodSpecTable[index]; break;
                case CLIMetadataTables.GenericParamConstraint: token.Data = GenericParamConstraintTable[index]; break;
                default: break;
            }
            return token;
        }

        public MetadataToken ExpandTypeDefRefOrSpecToken(uint pToken)
        {
            MetadataToken token = new MetadataToken();
            token.Table = (byte)(pToken & 0x03);
            switch (token.Table)
            {
                case 0: token.Table = CLIMetadataTables.TypeDef; break;
                case 1: token.Table = CLIMetadataTables.TypeRef; break;
                case 2: token.Table = CLIMetadataTables.TypeSpec; break;
                default: return token;
            }
            uint index = pToken >> 2;
            if (index == 0) return token;
            --index;
            switch (token.Table)
            {
                case CLIMetadataTables.TypeDef: token.Data = TypeDefTable[index]; break;
                case CLIMetadataTables.TypeRef: token.Data = TypeDefTable[index]; break;
                case CLIMetadataTables.TypeSpec: token.Data = TypeSpecTable[index]; break;
                default: break;
            }
            return token;
        }
    }
}
