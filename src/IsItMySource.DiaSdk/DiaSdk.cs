using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices.CustomMarshalers;

// ReSharper disable InconsistentNaming
namespace IKriv.IsItMySource.DiaSdk
{
    [Guid("A2EF5353-F5A8-4EB3-90D2-CB526ACB3CDD")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IDiaSourceFile
    {
        [DispId(2)]
        uint uniqueId { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(3)]
        string fileName { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

        [DispId(4)]
        uint checksumType { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(5)]
        IDiaEnumSymbols compilands { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void get_checksum([In] uint cbData, out uint pcbData, [MarshalAs(UnmanagedType.LPArray), In, Out] byte[] pbData);
    }

    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("10F3DBD9-664F-4469-B808-9471C7A50538")]
    [ComImport]
    public interface IDiaEnumSourceFiles
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(EnumeratorToEnumVariantMarshaler))]
        IEnumerator GetEnumerator();

        [DispId(1)]
        int count { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(UnmanagedType.Interface)]
        IDiaSourceFile Item([In] uint index);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Next([In] uint celt, [MarshalAs(UnmanagedType.Interface)] out IDiaSourceFile rgelt, out uint pceltFetched);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Skip([In] uint celt);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Reset();

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Clone([MarshalAs(UnmanagedType.Interface)] out IDiaEnumSourceFiles ppenum);
    }

    public enum SymTagEnum
    {
        SymTagNull,
        SymTagExe,
        SymTagCompiland,
        SymTagCompilandDetails,
        SymTagCompilandEnv,
        SymTagFunction,
        SymTagBlock,
        SymTagData,
        SymTagAnnotation,
        SymTagLabel,
        SymTagPublicSymbol,
        SymTagUDT,
        SymTagEnum,
        SymTagFunctionType,
        SymTagPointerType,
        SymTagArrayType,
        SymTagBaseType,
        SymTagTypedef,
        SymTagBaseClass,
        SymTagFriend,
        SymTagFunctionArgType,
        SymTagFuncDebugStart,
        SymTagFuncDebugEnd,
        SymTagUsingNamespace,
        SymTagVTableShape,
        SymTagVTable,
        SymTagCustom,
        SymTagThunk,
        SymTagCustomType,
        SymTagManagedType,
        SymTagDimension,
        SymTagCallSite,
        SymTagInlineSite,
        SymTagBaseInterface,
        SymTagVectorType,
        SymTagMatrixType,
        SymTagHLSLType,
        SymTagCaller,
        SymTagCallee,
        SymTagExport,
        SymTagHeapAllocationSite,
        SymTagCoffGroup,
        SymTagMax,
    }

    [Guid("B388EB14-BE4D-421D-A8A1-6CF7AB057086")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IDiaLineNumber
    {
        [DispId(1)]
        IDiaSymbol compiland { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

        [DispId(2)]
        IDiaSourceFile sourceFile { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

        [DispId(3)]
        uint lineNumber { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(4)]
        uint lineNumberEnd { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(5)]
        uint columnNumber { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(6)]
        uint columnNumberEnd { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(7)]
        uint addressSection { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(8)]
        uint addressOffset { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(9)]
        uint relativeVirtualAddress { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(10)]
        ulong virtualAddress { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(11)]
        uint length { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(12)]
        uint sourceFileId { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(13)]
        int statement { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(14)]
        uint compilandId { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }
    }

    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("FE30E878-54AC-44F1-81BA-39DE940F6052")]
    [ComImport]
    public interface IDiaEnumLineNumbers
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(EnumeratorToEnumVariantMarshaler))]
        IEnumerator GetEnumerator();

        [DispId(1)]
        int count { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(UnmanagedType.Interface)]
        IDiaLineNumber Item([In] uint index);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Next([In] uint celt, [MarshalAs(UnmanagedType.Interface)] out IDiaLineNumber rgelt, out uint pceltFetched);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Skip([In] uint celt);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Reset();

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Clone([MarshalAs(UnmanagedType.Interface)] out IDiaEnumLineNumbers ppenum);
    }

    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("3BFE56B0-390C-4863-9430-1F3D083B7684")]
    [ComImport]
    public interface IDiaInputAssemblyFile
    {
        [DispId(1)]
        uint uniqueId { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(2)]
        uint index { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(3)]
        uint timeStamp { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(4)]
        int pdbAvailableAtILMerge { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(5)]
        string fileName { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void get_version([In] uint cbData, out uint pcbData, out byte pbData);
    }

    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("CB787B2F-BD6C-4635-BA52-933126BD2DCD")]
    [ComImport]
    public interface IDiaSymbol
    {
        [DispId(0)]
        uint symIndexId { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(1)]
        uint symTag { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(2)]
        string name { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

        [DispId(3)]
        IDiaSymbol lexicalParent { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

        [DispId(4)]
        IDiaSymbol classParent { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

        [DispId(5)]
        IDiaSymbol type { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

        [DispId(6)]
        uint dataKind { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(7)]
        uint locationType { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(8)]
        uint addressSection { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(9)]
        uint addressOffset { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(10)]
        uint relativeVirtualAddress { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(11)]
        ulong virtualAddress { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(12)]
        uint registerId { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(13)]
        int offset { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(14)]
        ulong length { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(15)]
        uint slot { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(16)]
        int volatileType { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(17)]
        int constType { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(18)]
        int unalignedType { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(19)]
        uint access { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(20)]
        string libraryName { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

        [DispId(21)]
        uint platform { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(22)]
        uint language { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(23)]
        int editAndContinueEnabled { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(24)]
        uint frontEndMajor { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(25)]
        uint frontEndMinor { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(26)]
        uint frontEndBuild { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(27)]
        uint backEndMajor { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(28)]
        uint backEndMinor { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(29)]
        uint backEndBuild { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(30)]
        string sourceFileName { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

        [DispId(31)]
        string unused { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

        [DispId(32)]
        uint thunkOrdinal { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(33)]
        int thisAdjust { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(34)]
        uint virtualBaseOffset { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(35)]
        int @virtual { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(36)]
        int intro { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(37)]
        int pure { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(38)]
        uint callingConvention { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(39)]
        object value { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Struct)] get; }

        [DispId(40)]
        uint baseType { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(41)]
        uint token { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(42)]
        uint timeStamp { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(43)]
        Guid guid { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(44)]
        string symbolsFileName { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

        [DispId(46)]
        int reference { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(47)]
        uint count { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(49)]
        uint bitPosition { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(50)]
        IDiaSymbol arrayIndexType { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

        [DispId(51)]
        int packed { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(52)]
        int constructor { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(53)]
        int overloadedOperator { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(54)]
        int nested { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(55)]
        int hasNestedTypes { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(56)]
        int hasAssignmentOperator { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(57)]
        int hasCastOperator { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(58)]
        int scoped { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(59)]
        int virtualBaseClass { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(60)]
        int indirectVirtualBaseClass { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(61)]
        int virtualBasePointerOffset { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(62)]
        IDiaSymbol virtualTableShape { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

        [DispId(64)]
        uint lexicalParentId { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(65)]
        uint classParentId { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(66)]
        uint typeId { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(67)]
        uint arrayIndexTypeId { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(68)]
        uint virtualTableShapeId { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(69)]
        int code { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(70)]
        int function { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(71)]
        int managed { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(72)]
        int msil { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(73)]
        uint virtualBaseDispIndex { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(74)]
        string undecoratedName { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

        [DispId(75)]
        uint age { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(76)]
        uint signature { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(77)]
        int compilerGenerated { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(78)]
        int addressTaken { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(79)]
        uint rank { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(80)]
        IDiaSymbol lowerBound { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

        [DispId(81)]
        IDiaSymbol upperBound { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

        [DispId(82)]
        uint lowerBoundId { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(83)]
        uint upperBoundId { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void get_dataBytes([In] uint cbData, out uint pcbData, out byte pbData);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findChildren([In] SymTagEnum symTag, [MarshalAs(UnmanagedType.LPWStr), In] string name, [In] uint compareFlags, [MarshalAs(UnmanagedType.Interface)] out IDiaEnumSymbols ppResult);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findChildrenEx([In] SymTagEnum symTag, [MarshalAs(UnmanagedType.LPWStr), In] string name, [In] uint compareFlags, [MarshalAs(UnmanagedType.Interface)] out IDiaEnumSymbols ppResult);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findChildrenExByAddr([In] SymTagEnum symTag, [MarshalAs(UnmanagedType.LPWStr), In] string name, [In] uint compareFlags, [In] uint isect, [In] uint offset, [MarshalAs(UnmanagedType.Interface)] out IDiaEnumSymbols ppResult);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findChildrenExByVA([In] SymTagEnum symTag, [MarshalAs(UnmanagedType.LPWStr), In] string name, [In] uint compareFlags, [In] ulong va, [MarshalAs(UnmanagedType.Interface)] out IDiaEnumSymbols ppResult);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findChildrenExByRVA([In] SymTagEnum symTag, [MarshalAs(UnmanagedType.LPWStr), In] string name, [In] uint compareFlags, [In] uint rva, [MarshalAs(UnmanagedType.Interface)] out IDiaEnumSymbols ppResult);

        [DispId(84)]
        uint targetSection { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(85)]
        uint targetOffset { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(86)]
        uint targetRelativeVirtualAddress { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(87)]
        ulong targetVirtualAddress { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(88)]
        uint machineType { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(89)]
        uint oemId { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(90)]
        uint oemSymbolId { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void get_types([In] uint cTypes, out uint pcTypes, [MarshalAs(UnmanagedType.Interface)] out IDiaSymbol pTypes);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void get_typeIds([In] uint cTypeIds, out uint pcTypeIds, out uint pdwTypeIds);

        [DispId(91)]
        IDiaSymbol objectPointerType { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

        [DispId(92)]
        uint udtKind { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void get_undecoratedNameEx([In] uint undecorateOptions, [MarshalAs(UnmanagedType.BStr)] out string name);

        [DispId(93)]
        int noReturn { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(94)]
        int customCallingConvention { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(95)]
        int noInline { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(96)]
        int optimizedCodeDebugInfo { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(97)]
        int notReached { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(98)]
        int interruptReturn { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(99)]
        int farReturn { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(100)]
        int isStatic { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(101)]
        int hasDebugInfo { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(102)]
        int isLTCG { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(103)]
        int isDataAligned { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(104)]
        int hasSecurityChecks { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(105)]
        string compilerName { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

        [DispId(106)]
        int hasAlloca { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(107)]
        int hasSetJump { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(108)]
        int hasLongJump { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(109)]
        int hasInlAsm { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(110)]
        int hasEH { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(111)]
        int hasSEH { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(112)]
        int hasEHa { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(113)]
        int isNaked { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(114)]
        int isAggregated { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(115)]
        int isSplitted { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(116)]
        IDiaSymbol container { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

        [DispId(117)]
        int inlSpec { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(118)]
        int noStackOrdering { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(119)]
        IDiaSymbol virtualBaseTableType { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

        [DispId(120)]
        int hasManagedCode { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(121)]
        int isHotpatchable { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(122)]
        int isCVTCIL { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(123)]
        int isMSILNetmodule { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(124)]
        int isCTypes { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(125)]
        int isStripped { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(126)]
        uint frontEndQFE { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(127)]
        uint backEndQFE { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(128)]
        int wasInlined { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(129)]
        int strictGSCheck { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(130)]
        int isCxxReturnUdt { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(131)]
        int isConstructorVirtualBase { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(132)]
        int RValueReference { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(133)]
        IDiaSymbol unmodifiedType { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

        [DispId(134)]
        int framePointerPresent { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(135)]
        int isSafeBuffers { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(136)]
        int intrinsic { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(137)]
        int @sealed { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(138)]
        int hfaFloat { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(139)]
        int hfaDouble { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(140)]
        uint liveRangeStartAddressSection { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(141)]
        uint liveRangeStartAddressOffset { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(142)]
        uint liveRangeStartRelativeVirtualAddress { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(143)]
        uint countLiveRanges { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(144)]
        ulong liveRangeLength { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(145)]
        uint offsetInUdt { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(146)]
        uint paramBasePointerRegisterId { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(147)]
        uint localBasePointerRegisterId { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(148)]
        int isLocationControlFlowDependent { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(149)]
        uint stride { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(150)]
        uint numberOfRows { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(151)]
        uint numberOfColumns { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(152)]
        int isMatrixRowMajor { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void get_numericProperties([In] uint cnt, out uint pcnt, out uint pProperties);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void get_modifierValues([In] uint cnt, out uint pcnt, out ushort pModifiers);

        [DispId(153)]
        int isReturnValue { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(154)]
        int isOptimizedAway { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(155)]
        uint builtInKind { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(156)]
        uint registerType { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(157)]
        uint baseDataSlot { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(158)]
        uint baseDataOffset { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(159)]
        uint textureSlot { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(160)]
        uint samplerSlot { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(161)]
        uint uavSlot { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(162)]
        uint sizeInUdt { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(163)]
        uint memorySpaceKind { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(164)]
        uint unmodifiedTypeId { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(165)]
        uint subTypeId { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(166)]
        IDiaSymbol subType { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

        [DispId(167)]
        uint numberOfModifiers { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(168)]
        uint numberOfRegisterIndices { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(169)]
        int isHLSLData { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(170)]
        int isPointerToDataMember { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(171)]
        int isPointerToMemberFunction { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(172)]
        int isSingleInheritance { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(173)]
        int isMultipleInheritance { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(174)]
        int isVirtualInheritance { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(175)]
        int restrictedType { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(176)]
        int isPointerBasedOnSymbolValue { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(177)]
        IDiaSymbol baseSymbol { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

        [DispId(178)]
        uint baseSymbolId { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(179)]
        string objectFileName { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

        [DispId(184)]
        int isSdl { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(185)]
        int isWinRTPointer { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(186)]
        int isRefUdt { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(187)]
        int isValueUdt { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(188)]
        int isInterfaceUdt { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findInlineFramesByAddr([In] uint isect, [In] uint offset, [MarshalAs(UnmanagedType.Interface)] out IDiaEnumSymbols ppResult);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findInlineFramesByRVA([In] uint rva, [MarshalAs(UnmanagedType.Interface)] out IDiaEnumSymbols ppResult);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findInlineFramesByVA([In] ulong va, [MarshalAs(UnmanagedType.Interface)] out IDiaEnumSymbols ppResult);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findInlineeLines([MarshalAs(UnmanagedType.Interface)] out IDiaEnumLineNumbers ppResult);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findInlineeLinesByAddr([In] uint isect, [In] uint offset, [In] uint length, [MarshalAs(UnmanagedType.Interface)] out IDiaEnumLineNumbers ppResult);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findInlineeLinesByRVA([In] uint rva, [In] uint length, [MarshalAs(UnmanagedType.Interface)] out IDiaEnumLineNumbers ppResult);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findInlineeLinesByVA([In] ulong va, [In] uint length, [MarshalAs(UnmanagedType.Interface)] out IDiaEnumLineNumbers ppResult);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void getSrcLineOnTypeDefn([MarshalAs(UnmanagedType.Interface)] out IDiaLineNumber ppResult);

        [DispId(189)]
        int isPGO { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(190)]
        int hasValidPGOCounts { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(191)]
        int isOptimizedForSpeed { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(192)]
        uint PGOEntryCount { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(193)]
        uint PGOEdgeCount { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(194)]
        ulong PGODynamicInstructionCount { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(195)]
        uint staticSize { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(196)]
        uint finalLiveStaticSize { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(197)]
        string phaseName { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

        [DispId(198)]
        int hasControlFlowCheck { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(199)]
        int constantExport { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(200)]
        int dataExport { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(201)]
        int privateExport { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(202)]
        int noNameExport { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(203)]
        int exportHasExplicitlyAssignedOrdinal { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(204)]
        int exportIsForwarder { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(205)]
        uint ordinal { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(206)]
        uint frameSize { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(207)]
        uint exceptionHandlerAddressSection { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(208)]
        uint exceptionHandlerAddressOffset { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(209)]
        uint exceptionHandlerRelativeVirtualAddress { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(210)]
        ulong exceptionHandlerVirtualAddress { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findInputAssemblyFile([MarshalAs(UnmanagedType.Interface)] out IDiaInputAssemblyFile ppResult);

        [DispId(211)]
        uint characteristics { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(212)]
        IDiaSymbol coffGroup { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

        [DispId(213)]
        uint bindID { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(214)]
        uint bindSpace { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(215)]
        uint bindSlot { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }
    }

    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("CAB72C48-443B-48F5-9B0B-42F0820AB29A")]
    [ComImport]
    public interface IDiaEnumSymbols
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(EnumeratorToEnumVariantMarshaler))]
        IEnumerator GetEnumerator();

        [DispId(1)]
        int count { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(UnmanagedType.Interface)]
        IDiaSymbol Item([In] uint index);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Next([In] uint celt, [MarshalAs(UnmanagedType.Interface)] out IDiaSymbol rgelt, out uint pceltFetched);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Skip([In] uint celt);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Reset();

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Clone([MarshalAs(UnmanagedType.Interface)] out IDiaEnumSymbols ppenum);
    }

    [Guid("624B7D9C-24EA-4421-9D06-3B577471C1FA")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IDiaEnumSymbolsByAddr
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(UnmanagedType.Interface)]
        IDiaSymbol symbolByAddr([In] uint isect, [In] uint offset);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(UnmanagedType.Interface)]
        IDiaSymbol symbolByRVA([In] uint relativeVirtualAddress);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(UnmanagedType.Interface)]
        IDiaSymbol symbolByVA([In] ulong virtualAddress);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Next([In] uint celt, [MarshalAs(UnmanagedType.Interface)] out IDiaSymbol rgelt, out uint pceltFetched);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Prev([In] uint celt, [MarshalAs(UnmanagedType.Interface)] out IDiaSymbol rgelt, out uint pceltFetched);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Clone([MarshalAs(UnmanagedType.Interface)] out IDiaEnumSymbolsByAddr ppenum);
    }

    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("00000100-0000-0000-C000-000000000046")]
    [ComImport]
    public interface IEnumUnknown
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void RemoteNext([In] uint celt, [MarshalAs(UnmanagedType.IUnknown)] out object rgelt, out uint pceltFetched);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Skip([In] uint celt);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Reset();

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Clone([MarshalAs(UnmanagedType.Interface)] out IEnumUnknown ppenum);
    }

    [Guid("4A59FB77-ABAC-469B-A30B-9ECC85BFEF14")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IDiaTable : IEnumUnknown
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void RemoteNext([In] uint celt, [MarshalAs(UnmanagedType.IUnknown)] out object rgelt, out uint pceltFetched);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void Skip([In] uint celt);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void Reset();

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void Clone([MarshalAs(UnmanagedType.Interface)] out IEnumUnknown ppenum);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(EnumeratorToEnumVariantMarshaler))]
        IEnumerator GetEnumerator();

        [DispId(1)]
        string name { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

        [DispId(2)]
        int count { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(UnmanagedType.IUnknown)]
        object Item([In] uint index);
    }

    [Guid("C65C2B0A-1150-4D7A-AFCC-E05BF3DEE81E")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IDiaEnumTables
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(EnumeratorToEnumVariantMarshaler))]
        IEnumerator GetEnumerator();

        [DispId(1)]
        int count { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(UnmanagedType.Interface)]
        IDiaTable Item([MarshalAs(UnmanagedType.Struct), In] object index);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Next(uint celt, [MarshalAs(UnmanagedType.Interface)] ref IDiaTable rgelt, ref uint pceltFetched);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Skip([In] uint celt);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Reset();

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Clone([MarshalAs(UnmanagedType.Interface)] out IDiaEnumTables ppenum);
    }

    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("486943E8-D187-4A6B-A3C4-291259FFF60D")]
    [ComImport]
    public interface IDiaEnumDebugStreamData
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(EnumeratorToEnumVariantMarshaler))]
        IEnumerator GetEnumerator();

        [DispId(1)]
        int count { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(2)]
        string name { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Item([In] uint index, [In] uint cbData, out uint pcbData, out byte pbData);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Next([In] uint celt, [In] uint cbData, out uint pcbData, out byte pbData, out uint pceltFetched);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Skip([In] uint celt);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Reset();

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Clone([MarshalAs(UnmanagedType.Interface)] out IDiaEnumDebugStreamData ppenum);
    }

    [Guid("08CBB41E-47A6-4F87-92F1-1C9C87CED044")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IDiaEnumDebugStreams
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(EnumeratorToEnumVariantMarshaler))]
        IEnumerator GetEnumerator();

        [DispId(1)]
        int count { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(UnmanagedType.Interface)]
        IDiaEnumDebugStreamData Item([MarshalAs(UnmanagedType.Struct), In] object index);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Next([In] uint celt, [MarshalAs(UnmanagedType.Interface)] out IDiaEnumDebugStreamData rgelt, out uint pceltFetched);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Skip([In] uint celt);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Reset();

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Clone([MarshalAs(UnmanagedType.Interface)] out IDiaEnumDebugStreams ppenum);
    }

    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("AE605CDC-8105-4A23-B710-3259F1E26112")]
    [ComImport]
    public interface IDiaInjectedSource
    {
        [DispId(1)]
        uint crc { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(2)]
        ulong length { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [DispId(3)]
        string fileName { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

        [DispId(4)]
        string objectFileName { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

        [DispId(5)]
        string virtualFilename { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

        [DispId(6)]
        uint sourceCompression { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void get_source([In] uint cbData, out uint pcbData, out byte pbData);
    }

    [Guid("D5612573-6925-4468-8883-98CDEC8C384A")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IDiaEnumInjectedSources
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(EnumeratorToEnumVariantMarshaler))]
        IEnumerator GetEnumerator();

        [DispId(1)]
        int count { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(UnmanagedType.Interface)]
        IDiaInjectedSource Item([In] uint index);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Next([In] uint celt, [MarshalAs(UnmanagedType.Interface)] out IDiaInjectedSource rgelt, out uint pceltFetched);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Skip([In] uint celt);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Reset();

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Clone([MarshalAs(UnmanagedType.Interface)] out IDiaEnumInjectedSources ppenum);
    }

    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("1C7FF653-51F7-457E-8419-B20F57EF7E4D")]
    [ComImport]
    public interface IDiaEnumInputAssemblyFiles
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(EnumeratorToEnumVariantMarshaler))]
        IEnumerator GetEnumerator();

        [DispId(1)]
        int count { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; }

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: MarshalAs(UnmanagedType.Interface)]
        IDiaInputAssemblyFile Item([In] uint index);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Next([In] uint celt, [MarshalAs(UnmanagedType.Interface)] out IDiaInputAssemblyFile rgelt, out uint pceltFetched);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Skip([In] uint celt);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Reset();

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Clone([MarshalAs(UnmanagedType.Interface)] out IDiaEnumInputAssemblyFiles ppenum);
    }

    [Guid("2F609EE1-D1C8-4E24-8288-3326BADCD211")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IDiaSession
    {
        [DispId(1)]
        ulong loadAddress { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] get; [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

        [DispId(2)]
        IDiaSymbol globalScope { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.Interface)] get; }

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void getEnumTables([MarshalAs(UnmanagedType.Interface)] out IDiaEnumTables ppEnumTables);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void getSymbolsByAddr([MarshalAs(UnmanagedType.Interface)] out IDiaEnumSymbolsByAddr ppEnumbyAddr);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findChildren([MarshalAs(UnmanagedType.Interface), In] IDiaSymbol parent, [In] SymTagEnum symTag, [MarshalAs(UnmanagedType.LPWStr), In] string name, [In] uint compareFlags, [MarshalAs(UnmanagedType.Interface)] out IDiaEnumSymbols ppResult);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findChildrenEx([MarshalAs(UnmanagedType.Interface), In] IDiaSymbol parent, [In] SymTagEnum symTag, [MarshalAs(UnmanagedType.LPWStr), In] string name, [In] uint compareFlags, [MarshalAs(UnmanagedType.Interface)] out IDiaEnumSymbols ppResult);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findChildrenExByAddr([MarshalAs(UnmanagedType.Interface), In] IDiaSymbol parent, [In] SymTagEnum symTag, [MarshalAs(UnmanagedType.LPWStr), In] string name, [In] uint compareFlags, [In] uint isect, [In] uint offset, [MarshalAs(UnmanagedType.Interface)] out IDiaEnumSymbols ppResult);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findChildrenExByVA([MarshalAs(UnmanagedType.Interface), In] IDiaSymbol parent, [In] SymTagEnum symTag, [MarshalAs(UnmanagedType.LPWStr), In] string name, [In] uint compareFlags, [In] ulong va, [MarshalAs(UnmanagedType.Interface)] out IDiaEnumSymbols ppResult);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findChildrenExByRVA([MarshalAs(UnmanagedType.Interface), In] IDiaSymbol parent, [In] SymTagEnum symTag, [MarshalAs(UnmanagedType.LPWStr), In] string name, [In] uint compareFlags, [In] uint rva, [MarshalAs(UnmanagedType.Interface)] out IDiaEnumSymbols ppResult);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findSymbolByAddr([In] uint isect, [In] uint offset, [In] SymTagEnum symTag, [MarshalAs(UnmanagedType.Interface)] out IDiaSymbol ppSymbol);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findSymbolByRVA([In] uint rva, [In] SymTagEnum symTag, [MarshalAs(UnmanagedType.Interface)] out IDiaSymbol ppSymbol);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findSymbolByVA([In] ulong va, [In] SymTagEnum symTag, [MarshalAs(UnmanagedType.Interface)] out IDiaSymbol ppSymbol);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findSymbolByToken([In] uint token, [In] SymTagEnum symTag, [MarshalAs(UnmanagedType.Interface)] out IDiaSymbol ppSymbol);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void symsAreEquiv([MarshalAs(UnmanagedType.Interface), In] IDiaSymbol symbolA, [MarshalAs(UnmanagedType.Interface), In] IDiaSymbol symbolB);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void symbolById([In] uint id, [MarshalAs(UnmanagedType.Interface)] out IDiaSymbol ppSymbol);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findSymbolByRVAEx([In] uint rva, [In] SymTagEnum symTag, [MarshalAs(UnmanagedType.Interface)] out IDiaSymbol ppSymbol, out int displacement);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findSymbolByVAEx([In] ulong va, [In] SymTagEnum symTag, [MarshalAs(UnmanagedType.Interface)] out IDiaSymbol ppSymbol, out int displacement);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findFile([MarshalAs(UnmanagedType.Interface), In] IDiaSymbol pCompiland, [MarshalAs(UnmanagedType.LPWStr), In] string name, [In] uint compareFlags, [MarshalAs(UnmanagedType.Interface)] out IDiaEnumSourceFiles ppResult);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findFileById([In] uint uniqueId, [MarshalAs(UnmanagedType.Interface)] out IDiaSourceFile ppResult);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findLines([MarshalAs(UnmanagedType.Interface), In] IDiaSymbol compiland, [MarshalAs(UnmanagedType.Interface), In] IDiaSourceFile file, [MarshalAs(UnmanagedType.Interface)] out IDiaEnumLineNumbers ppResult);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findLinesByAddr([In] uint seg, [In] uint offset, [In] uint length, [MarshalAs(UnmanagedType.Interface)] out IDiaEnumLineNumbers ppResult);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findLinesByRVA([In] uint rva, [In] uint length, [MarshalAs(UnmanagedType.Interface)] out IDiaEnumLineNumbers ppResult);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findLinesByVA([In] ulong va, [In] uint length, [MarshalAs(UnmanagedType.Interface)] out IDiaEnumLineNumbers ppResult);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findLinesByLinenum([MarshalAs(UnmanagedType.Interface), In] IDiaSymbol compiland, [MarshalAs(UnmanagedType.Interface), In] IDiaSourceFile file, [In] uint linenum, [In] uint column, [MarshalAs(UnmanagedType.Interface)] out IDiaEnumLineNumbers ppResult);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findInjectedSource([MarshalAs(UnmanagedType.LPWStr), In] string srcFile, [MarshalAs(UnmanagedType.Interface)] out IDiaEnumInjectedSources ppResult);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void getEnumDebugStreams([MarshalAs(UnmanagedType.Interface)] out IDiaEnumDebugStreams ppEnumDebugStreams);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findInlineFramesByAddr([MarshalAs(UnmanagedType.Interface), In] IDiaSymbol parent, [In] uint isect, [In] uint offset, [MarshalAs(UnmanagedType.Interface)] out IDiaEnumSymbols ppResult);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findInlineFramesByRVA([MarshalAs(UnmanagedType.Interface), In] IDiaSymbol parent, [In] uint rva, [MarshalAs(UnmanagedType.Interface)] out IDiaEnumSymbols ppResult);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findInlineFramesByVA([MarshalAs(UnmanagedType.Interface), In] IDiaSymbol parent, [In] ulong va, [MarshalAs(UnmanagedType.Interface)] out IDiaEnumSymbols ppResult);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findInlineeLines([MarshalAs(UnmanagedType.Interface), In] IDiaSymbol parent, [MarshalAs(UnmanagedType.Interface)] out IDiaEnumLineNumbers ppResult);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findInlineeLinesByAddr([MarshalAs(UnmanagedType.Interface), In] IDiaSymbol parent, [In] uint isect, [In] uint offset, [In] uint length, [MarshalAs(UnmanagedType.Interface)] out IDiaEnumLineNumbers ppResult);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findInlineeLinesByRVA([MarshalAs(UnmanagedType.Interface), In] IDiaSymbol parent, [In] uint rva, [In] uint length, [MarshalAs(UnmanagedType.Interface)] out IDiaEnumLineNumbers ppResult);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findInlineeLinesByVA([MarshalAs(UnmanagedType.Interface), In] IDiaSymbol parent, [In] ulong va, [In] uint length, [MarshalAs(UnmanagedType.Interface)] out IDiaEnumLineNumbers ppResult);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findInlineeLinesByLinenum([MarshalAs(UnmanagedType.Interface), In] IDiaSymbol compiland, [MarshalAs(UnmanagedType.Interface), In] IDiaSourceFile file, [In] uint linenum, [In] uint column, [MarshalAs(UnmanagedType.Interface)] out IDiaEnumLineNumbers ppResult);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findInlineesByName([MarshalAs(UnmanagedType.LPWStr), In] string name, [In] uint option, [MarshalAs(UnmanagedType.Interface)] out IDiaEnumSymbols ppResult);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void addressForVA([In] ulong va, out uint pISect, out uint pOffset);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void addressForRVA([In] uint rva, out uint pISect, out uint pOffset);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findILOffsetsByAddr([In] uint isect, [In] uint offset, [In] uint length, [MarshalAs(UnmanagedType.Interface)] out IDiaEnumLineNumbers ppResult);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findILOffsetsByRVA([In] uint rva, [In] uint length, [MarshalAs(UnmanagedType.Interface)] out IDiaEnumLineNumbers ppResult);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findILOffsetsByVA([In] ulong va, [In] uint length, [MarshalAs(UnmanagedType.Interface)] out IDiaEnumLineNumbers ppResult);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findInputAssemblyFiles([MarshalAs(UnmanagedType.Interface)] out IDiaEnumInputAssemblyFiles ppResult);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findInputAssembly([In] uint index, [MarshalAs(UnmanagedType.Interface)] out IDiaInputAssemblyFile ppResult);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findInputAssemblyById([In] uint uniqueId, [MarshalAs(UnmanagedType.Interface)] out IDiaInputAssemblyFile ppResult);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void getFuncMDTokenMapSize(out uint pcb);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void getFuncMDTokenMap([In] uint cb, out uint pcb, out byte pb);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void getTypeMDTokenMapSize(out uint pcb);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void getTypeMDTokenMap([In] uint cb, out uint pcb, out byte pb);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void getNumberOfFunctionFragments_VA([In] ulong vaFunc, [In] uint cbFunc, out uint pNumFragments);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void getNumberOfFunctionFragments_RVA([In] uint rvaFunc, [In] uint cbFunc, out uint pNumFragments);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void getFunctionFragments_VA([In] ulong vaFunc, [In] uint cbFunc, [In] uint cFragments, out ulong pVaFragment, out uint pLenFragment);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void getFunctionFragments_RVA([In] uint rvaFunc, [In] uint cbFunc, [In] uint cFragments, out uint pRvaFragment, out uint pLenFragment);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void getExports([MarshalAs(UnmanagedType.Interface)] out IDiaEnumSymbols ppResult);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void getHeapAllocationSites([MarshalAs(UnmanagedType.Interface)] out IDiaEnumSymbols ppResult);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void findInputAssemblyFile([MarshalAs(UnmanagedType.Interface), In] IDiaSymbol pSymbol, [MarshalAs(UnmanagedType.Interface)] out IDiaInputAssemblyFile ppResult);
    }

    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("79F1BB5F-B66E-48E5-B6A9-1545C323CA3D")]
    [ComImport]
    public interface IDiaDataSource
    {
        [DispId(1)]
        string lastError { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void loadDataFromPdb([MarshalAs(UnmanagedType.LPWStr), In] string pdbPath);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void loadAndValidateDataFromPdb([MarshalAs(UnmanagedType.LPWStr), In] string pdbPath, [In] ref Guid pcsig70, [In] uint sig, [In] uint age);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void loadDataForExe([MarshalAs(UnmanagedType.LPWStr), In] string executable, [MarshalAs(UnmanagedType.LPWStr), In] string searchPath, [MarshalAs(UnmanagedType.IUnknown), In] object pCallback);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void loadDataFromIStream([MarshalAs(UnmanagedType.Interface), In] IStream pIStream);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void openSession([MarshalAs(UnmanagedType.Interface)] out IDiaSession ppSession);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void loadDataFromCodeViewInfo([MarshalAs(UnmanagedType.LPWStr), In] string executable, [MarshalAs(UnmanagedType.LPWStr), In] string searchPath, [In] uint cbCvInfo, [In] ref byte pbCvInfo, [MarshalAs(UnmanagedType.IUnknown), In] object pCallback);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void loadDataFromMiscInfo([MarshalAs(UnmanagedType.LPWStr), In] string executable, [MarshalAs(UnmanagedType.LPWStr), In] string searchPath, [In] uint timeStampExe, [In] uint timeStampDbg, [In] uint sizeOfExe, [In] uint cbMiscInfo, [In] ref byte pbMiscInfo, [MarshalAs(UnmanagedType.IUnknown), In] object pCallback);
    }

    [Guid("79F1BB5F-B66E-48E5-B6A9-1545C323CA3D")]
    [CoClass(typeof(DiaSourceClass))]
    [ComImport]
    public interface DiaSource : IDiaDataSource
    {
    }

    [TypeLibType(TypeLibTypeFlags.FCanCreate)]
    [Guid("E6756135-1E65-4D17-8576-610761398C3C")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComImport]
    public class DiaSourceClass : IDiaDataSource, DiaSource
    {
        [DispId(1)]
        public virtual extern string lastError { [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; }

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern void loadDataFromPdb([MarshalAs(UnmanagedType.LPWStr), In] string pdbPath);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern void loadAndValidateDataFromPdb([MarshalAs(UnmanagedType.LPWStr), In] string pdbPath, [In] ref Guid pcsig70, [In] uint sig, [In] uint age);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern void loadDataForExe([MarshalAs(UnmanagedType.LPWStr), In] string executable, [MarshalAs(UnmanagedType.LPWStr), In] string searchPath, [MarshalAs(UnmanagedType.IUnknown), In] object pCallback);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern void loadDataFromIStream([MarshalAs(UnmanagedType.Interface), In] IStream pIStream);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern void openSession([MarshalAs(UnmanagedType.Interface)] out IDiaSession ppSession);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern void loadDataFromCodeViewInfo([MarshalAs(UnmanagedType.LPWStr), In] string executable, [MarshalAs(UnmanagedType.LPWStr), In] string searchPath, [In] uint cbCvInfo, [In] ref byte pbCvInfo, [MarshalAs(UnmanagedType.IUnknown), In] object pCallback);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern void loadDataFromMiscInfo([MarshalAs(UnmanagedType.LPWStr), In] string executable, [MarshalAs(UnmanagedType.LPWStr), In] string searchPath, [In] uint timeStampExe, [In] uint timeStampDbg, [In] uint sizeOfExe, [In] uint cbMiscInfo, [In] ref byte pbMiscInfo, [MarshalAs(UnmanagedType.IUnknown), In] object pCallback);
    }
}
