using System;
using IKriv.IsItMySource.Interfaces;

namespace IKriv.IsItMySource.DiaSymReader
{
    public class DsrDebugInfoReader : IDebugInfoReader
    {
        public IDebugInfo GetDebugInfo(string exeOrPdbfilePath, string pdbSearchPath)
        {
            return new DsrDebugInfo(exeOrPdbfilePath, pdbSearchPath);
        }
    }
}
