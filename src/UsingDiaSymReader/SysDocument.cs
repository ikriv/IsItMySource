using Microsoft.DiaSymReader;
using System;
using System.IO;
using System.Security.Cryptography;

namespace IKriv.IsItMySource
{
    public class SymDocument
    {
        public SymDocument(ISymUnmanagedDocument doc)
        {
            if (doc == null)
                throw new ArgumentNullException(nameof(doc));

            int len;
            doc.GetUrl(0, out len, null);
            if (len > 0)
            {
                var urlChars = new char[len];
                doc.GetUrl(len, out len, urlChars);
                Url = new string(urlChars, 0, len - 1);
            }

            doc.GetChecksum(0, out len, null);
            if (len > 0)
            {
                Checksum = new byte[len];
                doc.GetChecksum(len, out len, Checksum);
            }

            Guid id = Guid.Empty;
            doc.GetChecksumAlgorithmId(ref id);
            ChecksumAlgorithmId = id;
        }

        public string Url { get; private set; }
        public byte[] Checksum { get; private set; }
        public Guid ChecksumAlgorithmId { get; private set; }

        public byte[] ComputeChecksum()
        {
            HashAlgorithm algo;
            if (ChecksumAlgorithmId == CorSym.SourceHashMd5)
            {
                algo = MD5.Create();
            }
            else if (ChecksumAlgorithmId == CorSym.SourceHashSha1)
            {
                algo = SHA1.Create();
            }
            else
                throw new NotSupportedException();

            try
            {
                return algo.ComputeHash(File.ReadAllBytes(Url));
            }
            finally
            {
                algo.Dispose();
            }
        }
    }

}
