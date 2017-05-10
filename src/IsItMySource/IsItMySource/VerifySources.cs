using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using IKriv.IsItMySource.Interfaces;

namespace IKriv.IsItMySource
{
    internal class VerifySources : IOperation
    {
        public void Run(IEnumerable<ISourceFileInfo> sources, Options options)
        {
            int nLeftOut = 0;
            int nFailedVerification = 0;

            foreach (var doc in sources.OrderBy(s => s.Path))
            {
                var relativePath = Util.GetRelativePath(doc.Path, options.RootPath);
                if (relativePath == null)
                {
                    ++nLeftOut;
                    continue;
                }

                if (!VerifyFile(doc, relativePath, options)) ++nFailedVerification;
                Console.WriteLine($" {relativePath} {doc.ChecksumTypeStr} {Util.ToHex(doc.Checksum)}");
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

        private bool VerifyFile(ISourceFileInfo fileInfo, string relativePath, Options options)
        {
            var localRoot = options.RootPath ?? options.LocalRootPath;
            var localPath = String.IsNullOrEmpty(localRoot)
                ? relativePath
                : Path.Combine(localRoot, relativePath);

            if (!File.Exists(localPath))
            {
                Console.Write("NOT FOUND");
                return false;
            }

            if (fileInfo.ChecksumType == ChecksumType.None)
            {
                Console.Write("NO CHKSUM");
                return false;
            }

            if (fileInfo.ChecksumType == ChecksumType.Unknown)
            {
                Console.Write("UNKNOWN  ");
                return false;
            }

            var localChecksum = ComputeChecksum(localPath, fileInfo.ChecksumType);
            if (localChecksum == null)
            {
                Console.Write("ERROR    ");
                return false;
            }

            if (!localChecksum.SequenceEqual(fileInfo.Checksum))
            {
                Console.Write("DIFFERENT");
                return false;
            }

            Console.Write("VERIFIED ");
            return true;
        }

        private byte[] ComputeChecksum(string path, ChecksumType checksumType)
        {
            using (var algo = CreateAlgo(checksumType))
            {
                try
                {
                    return algo.ComputeHash(File.ReadAllBytes(path));
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e);
                    return null;
                }
            }
        }

        private static HashAlgorithm CreateAlgo(ChecksumType checksumType)
        {
            switch (checksumType)
            {
                case ChecksumType.Sha1: return SHA1.Create();
                case ChecksumType.Md5: return MD5.Create();
                default: throw new NotSupportedException("Checksum type not supported: " + checksumType);
            }
        }

    }
}
