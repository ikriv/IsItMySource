namespace IKriv.IsItMySource.Interfaces
{
    public interface IDebugInfoReader
    {
        IDebugInfo GetDebugInfo(string exeOrPdbfilePath, string pdbSearchPath);
    }
}
