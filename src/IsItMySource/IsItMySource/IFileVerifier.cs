using IKriv.IsItMySource.Interfaces;

namespace IKriv.IsItMySource
{
    interface IFileVerifier
    {
        VerificationRecord Run(ISourceFileInfo fileInfo, Options options);
    }
}
