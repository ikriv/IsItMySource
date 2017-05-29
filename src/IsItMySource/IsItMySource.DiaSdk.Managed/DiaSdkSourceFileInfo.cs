using System;
using System.Runtime.InteropServices;
using IKriv.IsItMySource.Interfaces;

namespace IKriv.IsItMySource.DiaSdk.Managed
{
    internal class DiaSdkSourceFileInfo : ISourceFileInfo
    {
        private static readonly byte[] EmptyByteArray = new byte[0];

        public DiaSdkSourceFileInfo(IDiaSourceFile sourceFile)
        {
            uint? id = null;

            try
            {
                id = sourceFile.uniqueId;
                Path = sourceFile.fileName;
                ChecksumType = (ChecksumType) sourceFile.checksumType;
                ChecksumTypeStr = ChecksumType.ToString().ToUpperInvariant();
                uint checksumSize;
                sourceFile.get_checksum(0, out checksumSize, null);

                if (checksumSize == 0)
                {
                    Checksum = EmptyByteArray;
                }
                else
                {
                    byte[] checksum = new byte[checksumSize];
                    sourceFile.get_checksum(checksumSize, out checksumSize, checksum);
                    Checksum = checksum;
                }

                Marshal.ReleaseComObject(sourceFile);
            }
            catch (Exception e)
            {
                throw new ApplicationException($"Error reading source file with id {id}, path '{Path}': " + e.Message, e);
            }

        }

        public string Path { get; }
        public ChecksumType ChecksumType { get; }
        public string ChecksumTypeStr { get; }
        public byte[] Checksum { get; }
    }
}
