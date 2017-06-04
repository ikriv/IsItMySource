using System;
using IKriv.IsItMySource.Interfaces;
using Microsoft.DiaSymReader;

namespace IKriv.IsItMySource.DiaSymReader
{
    internal static class DsrSourceFileInfo
    {
        private static class CorSym
        {
            // guids are from corsym.h
            public static readonly Guid SourceHashMd5 = new Guid(0x406ea660, 0x64cf, 0x4c82, 0xb6, 0xf0, 0x42, 0xd4, 0x81, 0x72, 0xa7, 0x99);
            public static readonly Guid SourceHashSha1 = new Guid(0xff1816ec, 0xaa5e, 0x4d10, 0x87, 0xf7, 0x6f, 0x49, 0x63, 0x83, 0x34, 0x60);
        }

        private static readonly byte[] EmptyByteArray = new byte[0];

        public static SourceFileInfo Create(ISymUnmanagedDocument doc)
        {
            var result = new SourceFileInfo();
            if (doc == null) throw new ArgumentNullException(nameof(doc));

            int len;
            Ensure.Success("doc.GetUrl() getting URL size", doc.GetUrl(0, out len, null));

            if (len > 0)
            {
                var urlChars = new char[len];
                Ensure.Success("doc.getUrl()", doc.GetUrl(len, out len, urlChars));
                result.Path = new string(urlChars, 0, len - 1);
            }

            Ensure.Success("doc.GetChecksum(), getting checksum size", doc.GetChecksum(0, out len, null));

            if (len == 0)
            {
                result.Checksum = EmptyByteArray;
            }
            else
            {
                result.Checksum = new byte[len];
                Ensure.Success("doc.GetChecksum()", doc.GetChecksum(len, out len, result.Checksum));
            }

            Guid id = Guid.Empty;
            Ensure.Success(" doc.GetChecksumAlgorithmId", doc.GetChecksumAlgorithmId(ref id));

            if (id == CorSym.SourceHashMd5)
            {
                result.ChecksumType = ChecksumType.Md5;
                result.ChecksumTypeStr = "MD5";
            }
            else if (id == CorSym.SourceHashSha1)
            {
                result.ChecksumType = ChecksumType.Sha1;
                result.ChecksumTypeStr = "SHA1";
            }
            else if (id == Guid.Empty)
            {
                result.ChecksumType = ChecksumType.None;
            }
            else
            {
                result.ChecksumType = ChecksumType.Unknown;
                result.ChecksumTypeStr = id.ToString();
            }

            return result;
        }
    }
}
