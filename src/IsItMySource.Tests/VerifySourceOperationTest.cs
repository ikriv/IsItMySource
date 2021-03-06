﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IKriv.IsItMySource.Interfaces;
using NUnit.Framework;


namespace IKriv.IsItMySource.Tests
{
    [TestFixture]
    public class VerifySourceOperationTest
    {
        private StringWriter _output;
        private Options _options;
        private FakeFileVerifier _fileVerifier;

        private class FakeFileVerifier : IFileVerifier
        {
            private readonly Dictionary<string, VerificationRecord> _records = new Dictionary<string, VerificationRecord>();

            public VerificationRecord Run(SourceFileInfo fileInfo, Options options)
            {
                if (_records.TryGetValue(fileInfo.Path, out VerificationRecord record)) return record;

                return new VerificationRecord
                {
                    FileInfo = fileInfo,
                    RelativePath = fileInfo.Path,
                    Status = VerificationStatus.Missing
                };
            }

            public void Setup(VerificationRecord record)
            {
                _records[record.FileInfo.Path] = record;
            }
        }

        private VerificationRecord Record(string path, string relativePath, VerificationStatus status, string checksumTypeStr, string checksum)
        {
            if (!Enum.TryParse(checksumTypeStr, true, out ChecksumType checksumType)) checksumType = ChecksumType.Unknown;
            var file = new SourceFileInfo
            {
                Path = path,
                ChecksumType = checksumType,
                ChecksumTypeStr = checksumTypeStr,
                Checksum = checksum == null ? new byte[0] : Util.ToByteArray(checksum)
            };


            var record = new VerificationRecord
            {
                FileInfo = file,
                RelativePath = relativePath,
                Status = status
            };

            _fileVerifier.Setup(record);
            return record;
        }

        [SetUp]
        public void Setup()
        {
            _output = new StringWriter();
            _options = new Options
            {
                EngineName = Options.EngineNameManaged,
                ExeOrPdbPath = "something.exe",
                Operation = Operation.Verify
            };

            _fileVerifier = new FakeFileVerifier();
        }

        private VerifySourcesOperation CreateObject()
        {
            return new VerifySourcesOperation(_output, _fileVerifier);
        }

        private void Run(params VerificationRecord[] records)
        {
            CreateObject().Run(records.Select(r => r.FileInfo), _options);
        }

        [Test]
        public void OneFile_Missing()
        {
            _options.RootPath = "";
            var missing = Record(@"c:\temp\foobar.cpp", "foobar.cpp", VerificationStatus.Missing, "MD5", "123456789ABCDEF01122334455667788");
            Run(missing);

            Assert.AreEqual(@"MISSING   foobar.cpp MD5 123456789ABCDEF01122334455667788
1 file(s) missing
", _output.ToString());
        }

        [Test]
        public void OneFile_SameChecksum()
        {
            _options.RootPath = "";
            var verified = Record(@"c:\temp\foobar.cpp", "foobar.cpp", VerificationStatus.SameChecksum, "MD5", "123456789ABCDEF01122334455667788");
            Run(verified);

            Assert.AreEqual(@"VERIFIED  foobar.cpp MD5 123456789ABCDEF01122334455667788
1 file(s) verified
", _output.ToString());
        }

        [Test]
        public void OneFile_DifferentChecksum()
        {
            _options.RootPath = "";
            var failed = Record(@"c:\temp\foobar.cpp", "foobar.cpp", VerificationStatus.DifferentChecksum, "MD5", "123456789ABCDEF01122334455667788");
            Run(failed);

            Assert.AreEqual(@"DIFFERENT foobar.cpp MD5 123456789ABCDEF01122334455667788
1 file(s) failed verification
", _output.ToString());
        }

        [Test]
        public void OneFile_Skipped()
        {
            _options.RootPath = "root-path";
            var skipped = Record(@"c:\temp\foobar.cpp", "foobar.cpp", VerificationStatus.Skipped, "MD5", "123456789ABCDEF01122334455667788");
            Run(skipped);

            Assert.AreEqual(@"1 file(s) skipped, because they are outside of root-path
", _output.ToString());
        }

        [Test]
        public void OneFile_NoChecksum()
        {
            _options.RootPath = "";
            var noChecksum = Record(@"c:\temp\foobar.cpp", "foobar.cpp", VerificationStatus.NoChecksum, "NOCHECKSUM", null);
            Run(noChecksum);

            Assert.AreEqual(@"PRESENT   foobar.cpp NOCHECKSUM
1 file(s) present, but have no checksum information
", _output.ToString());
        }

        [Test]
        public void OneFile_UnknownChecksumType()
        {
            _options.RootPath = "";
            var unknownChecksumType = Record(@"c:\temp\foobar.cpp", "foobar.cpp", VerificationStatus.UnknownChecksumType, "CRC32", "12345678");
            Run(unknownChecksumType);

            Assert.AreEqual(@"PRESENT   foobar.cpp CRC32 12345678
1 file(s) present, but have unsupported checksum type
", _output.ToString());
        }

        [Test]
        public void OneFile_ErrorCalculatingChecksum()
        {
            _options.RootPath = "";
            var error = Record(@"c:\temp\foobar.cpp", "foobar.cpp", VerificationStatus.CouldNotCalculateChecksum, "MD5", "123456789ABCDEF01122334455667788");
            Run(error);

            Assert.AreEqual(@"ERROR     foobar.cpp MD5 123456789ABCDEF01122334455667788
1 file(s) present, but an error occurred while calculating checksum
", _output.ToString());
        }

        [Test]
        public void ManyFiles()
        {
            _options.RootPath = "c:\\temp";
            var missing = Record(@"c:\temp\missing.cs", "missing.cs", VerificationStatus.Missing, "MD5", "123456789ABCDEF01122334455667788");
            var verified = Record(@"c:\temp\verified.cs", "verified.cs", VerificationStatus.SameChecksum, "MD5", "9ABCD16A0832495F1E03EBC629A0D432");
            var verified2 = Record(@"c:\temp\verified2.cs", "verified2.cs", VerificationStatus.SameChecksum, "MD5", "9ABCD16A0832495F1E03EBC629A0D433");
            var failed = Record(@"c:\temp\failed.cs", "failed.cs", VerificationStatus.DifferentChecksum, "MD5", "123456789ABCDEF01122334455667789");
            var skipped = Record(@"c:\skipped.cs", "c:\\skipped.cs", VerificationStatus.Skipped, "MD5", "123456789ABCDEF0112233445566778A");
            var noChecksum = Record(@"c:\temp\none.cs", "none.cs", VerificationStatus.NoChecksum, "NOCHECKSUM", null);
            var unknownChecksumType = Record(@"c:\temp\unknown.cs", "unknown.cs", VerificationStatus.UnknownChecksumType, "CRC32", "12345678");
            var error = Record(@"c:\temp\error.cs", "error.cs", VerificationStatus.CouldNotCalculateChecksum, "MD5", "123456789ABCDEF0112233445566778B");

            Run(missing, verified, failed, skipped, noChecksum, unknownChecksumType, error, verified2);

            const string expectedResult =
@"ERROR     error.cs MD5 123456789ABCDEF0112233445566778B
DIFFERENT failed.cs MD5 123456789ABCDEF01122334455667789
MISSING   missing.cs MD5 123456789ABCDEF01122334455667788
PRESENT   none.cs NOCHECKSUM
PRESENT   unknown.cs CRC32 12345678
VERIFIED  verified.cs MD5 9ABCD16A0832495F1E03EBC629A0D432
VERIFIED  verified2.cs MD5 9ABCD16A0832495F1E03EBC629A0D433
2 file(s) verified
1 file(s) failed verification
1 file(s) missing
1 file(s) present, but have no checksum information
1 file(s) present, but have unsupported checksum type
1 file(s) present, but an error occurred while calculating checksum
1 file(s) skipped, because they are outside of c:\temp
";
            Assert.AreEqual(expectedResult, _output.ToString());
        }

        [Test]
        public void ManyFilesNoSummary()
        {
            _options.RootPath = "c:\\temp";
            _options.ShowSummary = false;

            var missing = Record(@"c:\temp\missing.cs", "missing.cs", VerificationStatus.Missing, "MD5", "123456789ABCDEF01122334455667788");
            var verified = Record(@"c:\temp\verified.cs", "verified.cs", VerificationStatus.SameChecksum, "MD5", "9ABCD16A0832495F1E03EBC629A0D432");
            var verified2 = Record(@"c:\temp\verified2.cs", "verified2.cs", VerificationStatus.SameChecksum, "MD5", "9ABCD16A0832495F1E03EBC629A0D433");
            var failed = Record(@"c:\temp\failed.cs", "failed.cs", VerificationStatus.DifferentChecksum, "MD5", "123456789ABCDEF01122334455667789");
            var skipped = Record(@"c:\skipped.cs", "c:\\skipped.cs", VerificationStatus.Skipped, "MD5", "123456789ABCDEF0112233445566778A");
            var noChecksum = Record(@"c:\temp\none.cs", "none.cs", VerificationStatus.NoChecksum, "NOCHECKSUM", null);
            var unknownChecksumType = Record(@"c:\temp\unknown.cs", "unknown.cs", VerificationStatus.UnknownChecksumType, "CRC32", "12345678");
            var error = Record(@"c:\temp\error.cs", "error.cs", VerificationStatus.CouldNotCalculateChecksum, "MD5", "123456789ABCDEF0112233445566778B");

            Run(missing, verified, failed, skipped, noChecksum, unknownChecksumType, error, verified2);

            const string expectedResult =
                @"ERROR     error.cs MD5 123456789ABCDEF0112233445566778B
DIFFERENT failed.cs MD5 123456789ABCDEF01122334455667789
MISSING   missing.cs MD5 123456789ABCDEF01122334455667788
PRESENT   none.cs NOCHECKSUM
PRESENT   unknown.cs CRC32 12345678
VERIFIED  verified.cs MD5 9ABCD16A0832495F1E03EBC629A0D432
VERIFIED  verified2.cs MD5 9ABCD16A0832495F1E03EBC629A0D433
";
            Assert.AreEqual(expectedResult, _output.ToString());
        }

        [Test]
        public void ManyFilesGuessRootPath()
        {
            var missing = Record(@"c:\temp\missing.cs", "missing.cs", VerificationStatus.Missing, "MD5", "123456789ABCDEF01122334455667788");
            var verified = Record(@"c:\temp\verified.cs", "verified.cs", VerificationStatus.SameChecksum, "MD5", "9ABCD16A0832495F1E03EBC629A0D432");
            var verified2 = Record(@"c:\temp\verified2.cs", "verified2.cs", VerificationStatus.SameChecksum, "MD5", "9ABCD16A0832495F1E03EBC629A0D433");
            var failed = Record(@"c:\temp\failed.cs", "failed.cs", VerificationStatus.DifferentChecksum, "MD5", "123456789ABCDEF01122334455667789");
            var noChecksum = Record(@"c:\temp\none.cs", "none.cs", VerificationStatus.NoChecksum, "NOCHECKSUM", null);
            var unknownChecksumType = Record(@"c:\temp\unknown.cs", "unknown.cs", VerificationStatus.UnknownChecksumType, "CRC32", "12345678");
            var error = Record(@"c:\temp\error.cs", "error.cs", VerificationStatus.CouldNotCalculateChecksum, "MD5", "123456789ABCDEF0112233445566778B");

            Run(missing, verified, failed, noChecksum, unknownChecksumType, error, verified2);

            const string expectedResult =
                "Running with --root \"c:\\temp\"" + @"
ERROR     error.cs MD5 123456789ABCDEF0112233445566778B
DIFFERENT failed.cs MD5 123456789ABCDEF01122334455667789
MISSING   missing.cs MD5 123456789ABCDEF01122334455667788
PRESENT   none.cs NOCHECKSUM
PRESENT   unknown.cs CRC32 12345678
VERIFIED  verified.cs MD5 9ABCD16A0832495F1E03EBC629A0D432
VERIFIED  verified2.cs MD5 9ABCD16A0832495F1E03EBC629A0D433
2 file(s) verified
1 file(s) failed verification
1 file(s) missing
1 file(s) present, but have no checksum information
1 file(s) present, but have unsupported checksum type
1 file(s) present, but an error occurred while calculating checksum
";
            Assert.AreEqual(expectedResult, _output.ToString());
        }

    }
}
