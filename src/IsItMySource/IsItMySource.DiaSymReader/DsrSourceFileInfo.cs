using System;
using IKriv.IsItMySource.Interfaces;
using Microsoft.DiaSymReader;

namespace IKriv.IsItMySource.DiaSymReader
{
    internal class DsrSourceFileInfo : ISourceFileInfo
    {
        private static class CorSym
        {
            // guids are from corsym.h
            public static readonly Guid SourceHashMd5 = new Guid(0x406ea660, 0x64cf, 0x4c82, 0xb6, 0xf0, 0x42, 0xd4, 0x81, 0x72, 0xa7, 0x99);
            public static readonly Guid SourceHashSha1 = new Guid(0xff1816ec, 0xaa5e, 0x4d10, 0x87, 0xf7, 0x6f, 0x49, 0x63, 0x83, 0x34, 0x60);
        }

        private static readonly byte[] EmptyByteArray = new byte[0];

        public DsrSourceFileInfo(ISymUnmanagedDocument doc)
        {
            if (doc == null) throw new ArgumentNullException(nameof(doc));

            int len;
            Ensure.Success("doc.GetUrl() getting URL size", doc.GetUrl(0, out len, null));

            if (len > 0)
            {
                var urlChars = new char[len];
                Ensure.Success("doc.getUrl()", doc.GetUrl(len, out len, urlChars));
                Path = new string(urlChars, 0, len - 1);
            }

            Ensure.Success("doc.GetChecksum(), getting checksum size", doc.GetChecksum(0, out len, null));

            if (len == 0)
            {
                Checksum = EmptyByteArray;
            }
            else
            {
                Checksum = new byte[len];
                Ensure.Success("doc.GetChecksum()", doc.GetChecksum(len, out len, Checksum));
            }

            Guid id = Guid.Empty;
            Ensure.Success(" doc.GetChecksumAlgorithmId", doc.GetChecksumAlgorithmId(ref id));

            if (id == CorSym.SourceHashMd5)
            {
                ChecksumType = ChecksumType.Md5;
                ChecksumTypeStr = "MD5";
            }
            else if (id == CorSym.SourceHashSha1)
            {
                ChecksumType = ChecksumType.Sha1;
                ChecksumTypeStr = "SHA1";
            }
            else if (id == Guid.Empty)
            {
                ChecksumType = ChecksumType.None;
            }
            else
            {
                ChecksumType = ChecksumType.Unknown;
                ChecksumTypeStr = id.ToString();
            }
        }

        public string Path { get; private set; }
        public ChecksumType ChecksumType { get; private set; }
        public string ChecksumTypeStr { get; private set; }
        public byte[] Checksum { get; private set; }
    }
}
