﻿using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using IKriv.IsItMySource.Interfaces;

namespace IKriv.IsItMySource
{
    internal class FileVerifier : IFileVerifier
    {
        public VerificationRecord Run(SourceFileInfo fileInfo, Options options)
        {
            var path = fileInfo.Path;
            var relativePath = Util.GetRelativePath(path, options.RootPath);

            var result = new VerificationRecord
            {
                FileInfo = fileInfo,
                RelativePath = relativePath,
                Status = VerifyFileImpl(fileInfo, relativePath, options)
            };

            return result;
        }

        private VerificationStatus VerifyFileImpl(SourceFileInfo fileInfo, string relativePath, Options options)
        {

            if (relativePath == null) return VerificationStatus.Skipped;

            var localRoot = options.LocalRootPath ?? options.RootPath;
            var localPath = String.IsNullOrEmpty(localRoot)
                ? relativePath
                : Path.Combine(localRoot, relativePath);

            if (!File.Exists(localPath)) return VerificationStatus.Missing;
            if (fileInfo.ChecksumType == ChecksumType.NoChecksum) return VerificationStatus.NoChecksum;
            if (fileInfo.ChecksumType == ChecksumType.Unknown) return VerificationStatus.UnknownChecksumType;

            var localChecksum = ComputeChecksum(localPath, fileInfo.ChecksumType);
            if (localChecksum == null) return VerificationStatus.CouldNotCalculateChecksum;
            if (!localChecksum.SequenceEqual(fileInfo.Checksum)) return VerificationStatus.DifferentChecksum;

            return VerificationStatus.SameChecksum;
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

        private HashAlgorithm CreateAlgo(ChecksumType checksumType)
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
