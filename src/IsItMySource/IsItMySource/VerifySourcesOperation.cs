using System.Collections.Generic;
using System.IO;
using System.Linq;
using IKriv.IsItMySource.Interfaces;

namespace IKriv.IsItMySource
{
    internal class VerifySourcesOperation : IOperation
    {
        private static readonly Dictionary<VerificationStatus, string> ShortStatusStr =
            new Dictionary<VerificationStatus, string>
            {
                {VerificationStatus.Skipped, "SKIPPED"},
                {VerificationStatus.SameChecksum, "VERIFIED"},
                {VerificationStatus.DifferentChecksum, "DIFFERENT"},
                {VerificationStatus.Missing, "MISSING"},
                {VerificationStatus.NoChecksum, "PRESENT"},
                {VerificationStatus.UnknownChecksumType, "PRESENT"},
                {VerificationStatus.CouldNotCalculateChecksum, "ERROR"}
            };

        private readonly TextWriter _output;
        private readonly IFileVerifier _fileVerifier;

        // production constructor
        public VerifySourcesOperation(TextWriter output)
            :
            this(output, new FileVerifier())
        {
        }

        // test constructor
        public VerifySourcesOperation(TextWriter output, IFileVerifier fileVerifier)
        {
            _output = output;
            _fileVerifier = fileVerifier;
        }

        public void Run(IEnumerable<SourceFileInfo> sources, Options options)
        {
            int nLeftOut = 0;
            int nFailedVerification = 0;

            foreach (var doc in sources.OrderBy(s => s.Path))
            {
                var record = Report(_fileVerifier.Run(doc, options));
                if (record.Status == VerificationStatus.Skipped)
                {
                    ++nLeftOut;
                }
                else if (record.Status != VerificationStatus.SameChecksum)
                {
                    ++nFailedVerification;
                }

            }

            if (nFailedVerification > 0)
            {
                _output.WriteLine($"{nFailedVerification} file(s) failed verification");
            }

            if (nLeftOut > 0)
            {
                _output.WriteLine($"{nLeftOut} file(s) outside of {options.RootPath}");
            }
        }

        private static string GetShortStatusStr(VerificationStatus status)
        {
            string result;
            if (!ShortStatusStr.TryGetValue(status, out result)) result = status.ToString();
            return result;
        }

        private VerificationRecord Report(VerificationRecord r)
        {
            if (r.Status == VerificationStatus.Skipped) return r;

            var statusStr = GetShortStatusStr(r.Status);
            var checksumStr = Util.ToHex(r.FileInfo.Checksum);
            _output.Write("{0,-10}", statusStr);
            var filler = (checksumStr == "") ? "" : " ";
            _output.WriteLine($"{r.RelativePath} {r.FileInfo.ChecksumTypeStr}{filler}{checksumStr}");
            return r;
        }
    }
}
