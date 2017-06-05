using System.IO;
using IKriv.IsItMySource.Interfaces;
using NUnit.Framework;

namespace IKriv.IsItMySource.Tests
{
    [TestFixture]
    public class ListSourcesOperationTest
    {
        private StringWriter _output;
        private Options _options;

        [SetUp]
        public void Setup()
        {
            _output = new StringWriter();
            _options = new Options
            {
                EngineName = Options.EngineNameManaged,
                ExeOrPdbPath = "something.exe",
                Operation = Operation.List
            };
        }

        [Test]
        public void EmptyListYieldsEmptyOutput()
        {
            var operation = CreateObject();
            operation.Run(new SourceFileInfo[0], _options);

            var str = _output.ToString();
            Assert.AreEqual("", str);
        }

        [Test]
        public void OneFile_NoChecksum()
        {
            var file = new SourceFileInfo
            {
                Path = @"c:\temp\foobar.txt",
                ChecksumTypeStr = "NOCHECKSUM",
                ChecksumType = ChecksumType.NoChecksum,
                Checksum = new byte[0]
            };

            var operation = CreateObject();
            operation.Run(new[] { file }, _options);

            var str = _output.ToString();
            Assert.AreEqual("c:\\temp\\foobar.txt NOCHECKSUM\r\n", str);
        }


        [Test]
        public void OneFile_Md5()
        {
            var file = new SourceFileInfo
            {
                Path = @"c:\temp\foobar.txt",
                ChecksumTypeStr = "MD5",
                ChecksumType = ChecksumType.Md5,
                Checksum = new byte[]
                    {0x12, 0x34, 0x56, 0x78, 0x9a, 0xbc, 0xde, 0xf0, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88}
            };

            var operation = CreateObject();
            operation.Run(new [] {file}, _options);

            var str = _output.ToString();
            Assert.AreEqual("c:\\temp\\foobar.txt MD5 123456789ABCDEF01122334455667788\r\n", str);
        }


        [Test]
        public void OneFile_Sha1()
        {
            var file = new SourceFileInfo
            {
                Path = @"c:\temp\foobar.txt",
                ChecksumTypeStr = "SHA1",
                ChecksumType = ChecksumType.Sha1,
                Checksum = new byte[]
                    {0x12, 0x34, 0x56, 0x78, 0x9a, 0xbc, 0xde, 0xf0, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xaa, 0xbb, 0xcc}
            };

            var operation = CreateObject();
            operation.Run(new[] { file }, _options);

            var str = _output.ToString();
            Assert.AreEqual("c:\\temp\\foobar.txt SHA1 123456789ABCDEF0112233445566778899AABBCC\r\n", str);
        }

        [Test]
        public void OneFile_UnknownChecksum()
        {
            var file = new SourceFileInfo
            {
                Path = @"c:\temp\foobar.txt",
                ChecksumTypeStr = "CRC32",
                ChecksumType = ChecksumType.Unknown,
                Checksum = new byte[]
                    {0x12, 0x34, 0x56, 0x78}
            };

            var operation = CreateObject();
            operation.Run(new[] { file }, _options);

            var str = _output.ToString();
            Assert.AreEqual("c:\\temp\\foobar.txt CRC32 12345678\r\n", str);
        }

        [Test]
        public void MultipleFiles_SortedAlphabetically()
        {
            var files = new[]
            {
                new SourceFileInfo
                {
                    Path = @"c:\temp\foobar.pch",
                    ChecksumTypeStr = "NOCHECKSUM",
                    ChecksumType = ChecksumType.NoChecksum,
                    Checksum = new byte[0]
                },
                new SourceFileInfo
                {
                    Path = @"c:\temp\foobar.cpp",
                    ChecksumTypeStr = "MD5",
                    ChecksumType = ChecksumType.Md5,
                    Checksum = new byte[]
                        {0x12, 0x34, 0x56, 0x78, 0x9a, 0xbc, 0xde, 0xf0, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88}
                },
                new SourceFileInfo
                {
                    Path = @"f:\dd\external\source.h",
                    ChecksumTypeStr = "MD5",
                    ChecksumType = ChecksumType.Md5,
                    Checksum = new byte[]
                        {0xab, 0x28, 0x44, 0x95, 0x13, 0xc2, 0x07, 0xf0, 0xd3, 0xee, 0xa4, 0x1f, 0x92, 0x5b, 0x38, 0x6d}
                }

            };

            var operation = CreateObject();
            operation.Run(files, _options);

            var str = _output.ToString();
            Assert.AreEqual(
@"c:\temp\foobar.cpp MD5 123456789ABCDEF01122334455667788
c:\temp\foobar.pch NOCHECKSUM
f:\dd\external\source.h MD5 AB28449513C207F0D3EEA41F925B386D
", str);
        }

        private ListSourcesOperation CreateObject()
        {
            return new ListSourcesOperation(_output);
        }
    }
}
