using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using IKriv.IsItMySource.Interfaces;

namespace IKriv.IsItMySource.DiaSdk
{
    internal class DiaSdkDebugInfo : IDebugInfo
    {
        private readonly DiaSource _diaSource;
        private readonly IDiaSession _session;
        private readonly IDiaSymbol _globalScope;

        public DiaSdkDebugInfo(string exeOrPdb, string searchPath)
        {
            try
            {
                if (exeOrPdb == null) throw new ArgumentNullException(nameof(exeOrPdb));
                _diaSource = new DiaSource();

                bool isPdb = exeOrPdb.ToLowerInvariant().EndsWith(".pdb");

                if (isPdb)
                {
                    _diaSource.loadDataFromPdb(exeOrPdb);
                }
                else
                {
                    _diaSource.loadDataForExe(exeOrPdb, searchPath, null);
                }

                _diaSource.openSession(out _session);
                _globalScope = _session.globalScope;
            }
            catch
            {
                Dispose();
                throw;
            }
        }

        public void Dispose()
        {
            if (_globalScope != null) Marshal.ReleaseComObject(_globalScope);
            if (_session != null) Marshal.ReleaseComObject(_session);
            if (_diaSource != null) Marshal.ReleaseComObject(_diaSource);
        }

        public IEnumerable<SourceFileInfo> GetSourceFiles()
        {
            var result = new List<SourceFileInfo>();

            IDiaEnumSymbols enumSymbols;
            _globalScope.findChildren(SymTagEnum.SymTagCompiland, null, 0, out enumSymbols);

            try
            {
                while (true)
                {
                    uint nElements;
                    IDiaSymbol compiland;

                    enumSymbols.Next(1, out compiland, out nElements);
                    if (nElements != 1) break;

                    IDiaEnumSourceFiles enumSourceFiles;
                    _session.findFile(compiland, null, 0, out enumSourceFiles);

                    try
                    {
                        while (true)
                        {
                            uint nElements2;
                            IDiaSourceFile sourceFile;

                            enumSourceFiles.Next(1, out sourceFile, out nElements2);
                            if (nElements2 != 1) break;

                            result.Add(DiaSdkSourceFileInfo.Create(sourceFile));
                        }
                    }
                    finally
                    {
                        Marshal.ReleaseComObject(enumSourceFiles);
                    }
                }
            }
            finally
            {
                Marshal.ReleaseComObject(enumSymbols);
            }

            return result;
        }
    }
}
