using System;
using System.Runtime.InteropServices;
using IKriv.IsItMySource.Interfaces;

namespace IKriv.IsItMySource.DiaSdk
{
    internal static class DiaSdkSourceFileInfo
    {
        private static readonly byte[] EmptyByteArray = new byte[0];

        public static SourceFileInfo Create(IDiaSourceFile sourceFile)
        {
            uint? id = null;
            var result = new SourceFileInfo();

            try
            {
                id = sourceFile.uniqueId;
                result.Path = sourceFile.fileName;
                result.ChecksumType = (ChecksumType) sourceFile.checksumType;
                result.ChecksumTypeStr = result.ChecksumType.ToString().ToUpperInvariant();
                uint checksumSize;
                sourceFile.get_checksum(0, out checksumSize, null);

                if (checksumSize == 0)
                {
                    result.Checksum = EmptyByteArray;
                }
                else
                {
                    byte[] checksum = new byte[checksumSize];
                    sourceFile.get_checksum(checksumSize, out checksumSize, checksum);
                    result.Checksum = checksum;
                }

                Marshal.ReleaseComObject(sourceFile);
            }
            catch (Exception e)
            {
                throw new ApplicationException($"Error reading source file with id {id}, path '{result.Path}': " + e.Message, e);
            }

            return result;
        }
    }
}
