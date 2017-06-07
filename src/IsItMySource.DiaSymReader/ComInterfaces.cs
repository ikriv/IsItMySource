using System;
using System.Runtime.InteropServices;

namespace IKriv.IsItMySource.DiaSymReader
{
    [ComImport, Guid("0A29FF9E-7F9C-4437-8B11-F424491E3931")]
    internal class CorSymBinder_SxS // from corsym.h
    {
    }

    [ComImport, Guid("E5CB7A31-7512-11d2-89CE-0080C792E5D8")]
    internal class CorMetaDataDispenser // from cor.h
    {
    }

    [Guid("7DAC8207-D3AE-4c75-9B67-92801A497D44"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMetaDataImport // from cor.h
    {
        // we don't need to use what's inside
    }

    [Guid("809c652e-7396-11d2-9771-00a0c9b4d50c"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMetaDataDispenser // from cor.h
    {
        void _VtblGap0_1(); // skip 1 method

        IMetaDataImport OpenScope([MarshalAs(UnmanagedType.LPWStr)] string szScope, int dwOpenFlags,
            [MarshalAs(UnmanagedType.LPStruct)] Guid riid);
    }
}