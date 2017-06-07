using IKriv.IsItMySource.Interfaces;

namespace IKriv.IsItMySource
{
    interface IFileVerifier
    {
        VerificationRecord Run(SourceFileInfo fileInfo, Options options);
    }
}
