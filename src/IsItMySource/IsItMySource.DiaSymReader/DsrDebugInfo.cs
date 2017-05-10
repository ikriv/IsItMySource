using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using IKriv.IsItMySource.Interfaces;
using Microsoft.DiaSymReader;

namespace IKriv.IsItMySource.DiaSymReader
{
    internal class DsrDebugInfo : IDebugInfo
    {
        private readonly ISymUnmanagedReader _reader;

        public DsrDebugInfo(string exePath, string searchPath)
        {
            IMetaDataDispenser dispenser = null;
            IMetaDataImport import = null;
            ISymUnmanagedBinder binder = null;

            try
            {
                if (exePath == null) throw new ArgumentNullException(nameof(exePath));

                if (exePath.ToLower().EndsWith("pdb"))
                {
                    throw new NotSupportedException(
                        "DiaSymReader does not support opening PDB files directly. " +
                        "Open corresponding EXE or DLL file. Provide search path if necessary.");
                }

                // ReSharper disable once SuspiciousTypeConversion.Global
                dispenser = (IMetaDataDispenser) new CorMetaDataDispenser();
                import = dispenser.OpenScope(exePath, 0, typeof(IMetaDataImport).GUID);
                // ReSharper disable once SuspiciousTypeConversion.Global
                binder = (ISymUnmanagedBinder) new CorSymBinder_SxS();

                Ensure.Success("Reading debug information via binder.GetReaderForFile()",
                    binder.GetReaderForFile(import, exePath, searchPath, out _reader));
            }
            finally
            {
                if (binder != null) Marshal.ReleaseComObject(binder);
                if (import != null) Marshal.ReleaseComObject(import);
                if (dispenser != null) Marshal.ReleaseComObject(dispenser);
            }
        }

        public void Dispose()
        {
            Marshal.ReleaseComObject(_reader);
        }


        public IEnumerable<ISourceFileInfo> GetSourceFiles()
        {

            int count;
            Ensure.Success("GetDocuments(), reading documents count", _reader.GetDocuments(0, out count, null));

            if (count == 0) return Enumerable.Empty<ISourceFileInfo>();

            var docs = new ISymUnmanagedDocument[count];
            Ensure.Success("_reader.GetDocuments()", _reader.GetDocuments(count, out count, docs));

            try
            {
                var sources = docs.Select(d => new DsrSourceFileInfo(d)).ToArray();
                return sources;
            }
            finally
            {
                foreach (var doc in docs) Marshal.ReleaseComObject(doc);
            }
        }
    }
}
