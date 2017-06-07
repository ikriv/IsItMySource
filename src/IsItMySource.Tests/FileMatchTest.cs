using NUnit.Framework;
using System.Globalization;
using System.Threading;

namespace IKriv.IsItMySource.Tests
{
    [TestFixture]
    public class FileMatchTest
    {
        [Test]
        public void EmptyList_NothingMatches()
        {
            var m = new FileMatch("");
            Assert.IsFalse(m.IsMatch(""));
            Assert.IsFalse(m.IsMatch("bla"));
        }

        [Test]
        public void SingleStarMatchesAnySequenceWithoutBackslash()
        {
            var m = new FileMatch("*.txt");
            Assert.IsFalse(m.IsMatch(""));
            Assert.IsFalse(m.IsMatch("bla"));
            Assert.IsFalse(m.IsMatch("bla.txt2"));
            Assert.IsFalse(m.IsMatch(@"foo\bar\baz.txt"));

            Assert.IsTrue(m.IsMatch(".txt"));
            Assert.IsTrue(m.IsMatch("bla.txt"));
        }

        [Test]
        public void DoubleStarMatchesAnySequence()
        {
            var m = new FileMatch("**.txt");
            Assert.IsFalse(m.IsMatch(""));
            Assert.IsFalse(m.IsMatch("bla"));
            Assert.IsFalse(m.IsMatch("bla.txt2"));

            Assert.IsTrue(m.IsMatch(".txt"));
            Assert.IsTrue(m.IsMatch("bla.txt"));
            Assert.IsTrue(m.IsMatch(@"foo\bar\baz.txt"));
        }

        [Test]
        public void DoubleStarBackslashMatchesEmptySequence()
        {
            var m = new FileMatch(@"foo\**\bar.txt");
            Assert.IsFalse(m.IsMatch(""));
            Assert.IsFalse(m.IsMatch(@"foo\abar.txt"));
            Assert.IsFalse(m.IsMatch(@"foo\bar.txt2"));
            Assert.IsFalse(m.IsMatch(@"foo\baz\bar.txt2"));
            Assert.IsFalse(m.IsMatch(@"foo\baz\bar.txt\"));
            Assert.IsFalse(m.IsMatch(@"foo\baz\abar.txt"));
            Assert.IsFalse(m.IsMatch(@"\foo\baz\bar.txt\"));

            Assert.IsTrue(m.IsMatch(@"foo\bar.txt"));
            Assert.IsTrue(m.IsMatch(@"foo\\bar.txt"));
            Assert.IsTrue(m.IsMatch(@"foo\qwe\bar.txt"));
            Assert.IsTrue(m.IsMatch(@"foo\qwe\ui\bar.txt"));
        }

        [Test]
        public void QuestionMarkMatchesAnyCharExceptBackslash()
        {
            var m = new FileMatch("b?r.txt");
            Assert.IsFalse(m.IsMatch(""));
            Assert.IsFalse(m.IsMatch("bar"));
            Assert.IsFalse(m.IsMatch("baar.txt"));
            Assert.IsFalse(m.IsMatch("bar.txt2"));
            Assert.IsFalse(m.IsMatch(@"b\r.txt"));

            Assert.IsTrue(m.IsMatch("bar.txt"));
            Assert.IsTrue(m.IsMatch("ber.txt"));
        }

        [Test]
        public void MultiplePatterns()
        {
            var m = new FileMatch(@"*.txt;c:\**\sobaka.bat");
            Assert.IsFalse(m.IsMatch(""));
            Assert.IsFalse(m.IsMatch("bla"));
            Assert.IsTrue(m.IsMatch(".txt"));
            Assert.IsTrue(m.IsMatch("bla.txt"));
            Assert.IsFalse(m.IsMatch("bla.txt2"));
            Assert.IsTrue(m.IsMatch(@"c:\temp\sobaka.bat"));
            Assert.IsTrue(m.IsMatch(@"c:\temp\dog\sobaka.bat"));
            Assert.IsFalse(m.IsMatch(@"c:\temp\dog\sobaka.bat2"));
            Assert.IsFalse(m.IsMatch(@"c:\temp\dog\zlayasobaka.bat2"));
        }

        [Test]
        public void EmptyPatternsAndSpacesIgnored()
        {
            var m = new FileMatch(@" *.txt;; ;**.bat;");
            Assert.IsFalse(m.IsMatch(""));
            Assert.IsFalse(m.IsMatch("bla"));
            Assert.IsTrue(m.IsMatch(".txt"));
            Assert.IsTrue(m.IsMatch("bla.txt"));
            Assert.IsTrue(m.IsMatch("bla.bat"));
            Assert.IsFalse(m.IsMatch("bla.txt2"));
            Assert.IsTrue(m.IsMatch(@"c:\temp\sobaka.bat"));
            Assert.IsTrue(m.IsMatch(@"c:\temp\dog\sobaka.bat"));
            Assert.IsFalse(m.IsMatch(@"c:\temp\dog\sobaka.bat2"));
            Assert.IsFalse(m.IsMatch(@"c:\temp\dog\zlayasobaka.bat2"));
        }

        [Test]
        public void MatchIsCaseInsensitive()
        {
            var m = new FileMatch("*.ini");
            Assert.IsTrue(m.IsMatch("WIN.INI"));
        }

        [Test]
        public void MatchIsCaseInsensitiveInvariantCulture()
        {
            var thread = Thread.CurrentThread;
            var culture = thread.CurrentCulture;
            var uiCulture = thread.CurrentUICulture;

            try
            {
                // Switch to Turkish, where upper case I does not map to lower case i
                thread.CurrentCulture = thread.CurrentUICulture = new CultureInfo("tr-TR"); 
                var m = new FileMatch("*.ini");
                Assert.IsTrue(m.IsMatch("WIN.INI"));
            }
            finally
            {
                thread.CurrentCulture = culture;
                thread.CurrentUICulture = uiCulture;
            }
        }

    }
}

