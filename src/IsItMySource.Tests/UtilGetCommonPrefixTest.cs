using NUnit.Framework;

namespace IKriv.IsItMySource.Tests
{
    [TestFixture]
    public class UtilGetCommonPrefixTest
    {
        [Test]
        public void EmptListEmptyPrefix()
        {
            Assert.AreEqual("", Util.GetCommonPrefix(new string[0]));
        }

        [Test]
        public void OneStringCommonPrefixOfSelf()
        {
            Assert.AreEqual("bla", Util.GetCommonPrefix(new[] {"bla"}));
        }

        [Test]
        public void TwoStringsWithoutCommonPrefix()
        {
            Assert.AreEqual("", Util.GetCommonPrefix(new[] { "foo", "bar" }));
        }

        [Test]
        public void TwoStringsWithCommonPrefix()
        {
            Assert.AreEqual("ba", Util.GetCommonPrefix(new[] { "bar", "baz" }));
        }

        [Test]
        public void ManyStringsWithCommonPrefix()
        {
            Assert.AreEqual(@"c:\temp\src\module", Util.GetCommonPrefix(new[] {
                @"c:\temp\src\module1\file1.txt",
                @"c:\temp\src\module1\file2.txt",
                @"c:\temp\src\module2\file1.txt",
                @"c:\temp\src\module3\file4.txt"
            }));
        }

    }
}
