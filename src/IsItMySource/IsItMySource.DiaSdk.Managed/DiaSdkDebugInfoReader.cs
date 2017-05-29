using IKriv.IsItMySource.Interfaces;

namespace IKriv.IsItMySource.DiaSdk.Managed
{
    public class DiaSdkDebugInfoReader : IDebugInfoReader
    {
        public IDebugInfo GetDebugInfo(string exeOrPdbfilePath, string pdbSearchPath)
        {
            return new DiaSdkDebugInfo(exeOrPdbfilePath, pdbSearchPath);
        }
    }
}
