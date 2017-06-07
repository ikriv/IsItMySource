using NUnit.Framework;

namespace IKriv.IsItMySource.Tests
{
    [TestFixture]
    public class UtilGetCommonRootDirTest
    {
        [Test]
        public void EmptListEmptyRootDir()
        {
            Assert.AreEqual("", Util.GetCommonRootDir(new string[0]));
        }

        [Test]
        public void JustFileNameEmptyRootDir()
        {
            Assert.AreEqual("", Util.GetCommonRootDir(new[] { "bla.txt" }));
        }

        [Test]
        public void OnePathRootDir()
        {
            Assert.AreEqual(@"c:\temp", Util.GetCommonRootDir(new[] { @"c:\temp\bla.txt" }));
        }

        [Test]
        public void TwoPathsWithoutCommonPrefix()
        {
            Assert.AreEqual("", Util.GetCommonRootDir(new[] { @"c:\temp\bla.txt", @"d:\foo.cs" }));
        }

        [Test]
        public void TwoPathsWithCommonPrefix()
        {
            Assert.AreEqual(@"c:\temp", Util.GetCommonRootDir(new[] { @"c:\temp\file1.txt", @"c:\temp\file2.exe" }));
        }

        [Test]
        public void ManyPathsWithCommonPrefix()
        {
            Assert.AreEqual(@"c:\temp\src", Util.GetCommonRootDir(new[] {
                @"c:\temp\src\module1\file1.txt",
                @"c:\temp\src\module1\file2.txt",
                @"c:\temp\src\module2\file1.txt",
                @"c:\temp\src\module3\file4.txt"
            }));
        }
    }
}
