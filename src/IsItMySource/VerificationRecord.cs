using System.IO;
using IKriv.IsItMySource.Interfaces;

namespace IKriv.IsItMySource
{
    internal class VerificationRecord
    {
        public SourceFileInfo FileInfo { get; set; }
        public string RelativePath { get; set; }
        public VerificationStatus Status { get; set; }
    }
}
