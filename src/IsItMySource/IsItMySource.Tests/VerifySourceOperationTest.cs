using System.Collections.Generic;
using System.IO;
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

            public void Setup(SourceFileInfo file, VerificationRecord result)
            {
                _records[file.Path] = result;
            }
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


        [Test]
        public void OneFile_Missing()
        {
            var sourceFile = new SourceFileInfo
            {
                Path = @"c:\temp\foobar.cpp",
                ChecksumTypeStr = "MD5",
                ChecksumType = ChecksumType.Md5,
                Checksum = new byte[]
                    {0x12, 0x34, 0x56, 0x78, 0x9a, 0xbc, 0xde, 0xf0, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88}
            };

            var operation = CreateObject();
            operation.Run(new []{sourceFile}, _options);

            Assert.AreEqual(@"MISSING   c:\temp\foobar.cpp MD5 123456789ABCDEF01122334455667788
1 file(s) failed verification
", _output.ToString());
        }

        private VerifySourcesOperation CreateObject()
        {
            return new VerifySourcesOperation(_output, _fileVerifier);
        }

    }
}
