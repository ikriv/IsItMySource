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


        private static readonly Dictionary<VerificationStatus, string> LongStatusStr =
            new Dictionary<VerificationStatus, string>
            {
                {VerificationStatus.Skipped, "skipped"},
                {VerificationStatus.SameChecksum, "verified"},
                {VerificationStatus.DifferentChecksum, "failed verification"},
                {VerificationStatus.Missing, "missing"},
                {VerificationStatus.NoChecksum, "present, but have no checksum information"},
                {VerificationStatus.UnknownChecksumType, "present, but have unsupported checksum type"},
                {VerificationStatus.CouldNotCalculateChecksum, "present, but an error occurred while calculating checksum"}
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
            var summary =
                sources
                    .OrderBy(s => s.Path)
                    .Select(s => Report(_fileVerifier.Run(s, options)))
                    .GroupBy(r => r.Status)
                    .ToDictionary(g => g.Key, g => g.Count());

            if (options.ShowSummary)
            {
                foreach (var status in summary.Keys.OrderBy(k => k))
                {
                    if (status == VerificationStatus.Skipped)
                    {
                        _output.WriteLine(
                            $"{summary[status]} file(s) skipped, because they are outside of {options.RootPath}");
                    }
                    else
                    {
                        _output.WriteLine($"{summary[status]} file(s) {LongStatusStr[status]}");
                    }
                }
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
