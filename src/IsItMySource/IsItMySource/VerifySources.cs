using System;
using System.Collections.Generic;
using System.Linq;
using IKriv.IsItMySource.Interfaces;

namespace IKriv.IsItMySource
{
    internal class VerifySources : IOperation
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

   

        public void Run(IEnumerable<ISourceFileInfo> sources, Options options)
        {
            int nLeftOut = 0;
            int nFailedVerification = 0;

            foreach (var doc in sources.OrderBy(s => s.Path))
            {
                var record = Report(VerifyFile.Run(doc, options));
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
                Console.WriteLine($"{nFailedVerification} file(s) failed verification");
            }

            if (nLeftOut > 0)
            {
                Console.WriteLine($"{nLeftOut} file(s) outside of {options.RootPath}");
            }
        }

        private static string GetShortStatusStr(VerificationStatus status)
        {
            string result;
            if (!ShortStatusStr.TryGetValue(status, out result)) result = status.ToString();
            return result;
        }

        private static VerificationRecord Report(VerificationRecord r)
        {
            if (r.Status == VerificationStatus.Skipped) return r;

            var statusStr = GetShortStatusStr(r.Status);
            var checksumStr = Util.ToHex(r.FileInfo.Checksum);
            Console.Write("{0,-10}", statusStr);
            Console.WriteLine($"{r.RelativePath} {r.FileInfo.ChecksumTypeStr} {checksumStr}");
            return r;
        }

    }
}
