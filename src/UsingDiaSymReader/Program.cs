using System;
using Microsoft.DiaSymReader;

namespace IKriv.IsItMySource
{
    // Based on http://stackoverflow.com/questions/36649271/check-that-pdb-file-matches-to-the-source
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.Error.WriteLine("Usage: IsItMySource file.exe");
                return;
            }

            try
            {
                ShowChecksums(args[0]);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
        }

        // this needs a reference to Microsoft.DiaSymReader
        // or redefine its interfaces manually from here https://github.com/dotnet/roslyn/tree/master/src/Dependencies/Microsoft.DiaSymReader
        public static void ShowChecksums(string exePath)
        {
            if (exePath == null)
                throw new ArgumentNullException(nameof(exePath));

            var dispenser = (IMetaDataDispenser)new CorMetaDataDispenser();
            var import = dispenser.OpenScope(exePath, 0, typeof(IMetaDataImport).GUID);

            var binder = (ISymUnmanagedBinder)new CorSymBinder_SxS();
            ISymUnmanagedReader reader;
            binder.GetReaderForFile(import, exePath, null, out reader);

            int count;
            reader.GetDocuments(0, out count, null);
            if (count > 0)
            {
                var docs = new ISymUnmanagedDocument[count];
                reader.GetDocuments(count, out count, docs);
                foreach (var d in docs)
                {
                    var doc = new SymDocument(d);
                    var algo = GetChecksumAlgStr(doc);

                    Console.WriteLine($"{doc.Url} {algo} {ToHex(doc.Checksum)}");
                }
            }
        }

        private static string GetChecksumAlgStr(SymDocument doc)
        {
            Guid guid = doc.ChecksumAlgorithmId;
            if (guid == CorSym.SourceHashMd5) return "MD5";
            if (guid == CorSym.SourceHashSha1) return "SHA1";
            return guid.ToString();
        }

        private static string ToHex(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }
    }
}
