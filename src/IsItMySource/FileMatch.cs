using System.Linq;
using System.Text.RegularExpressions;

namespace IKriv.IsItMySource
{
    class FileMatch
    {
        private readonly Regex[] _patterns;

        public FileMatch(string ignoreFilesList)
        {
            _patterns = ignoreFilesList
                .Split(';')
                .Select(s => s.Trim())
                .Where(s => s != "")
                .Select(w=>new Regex(WildcardToRegEx(w), RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
                .ToArray();
        }

        public bool IsMatch(string path)
        {
            return _patterns.Any(p => p.IsMatch(path));
        }

        private static string WildcardToRegEx(string wildcard)
        {
            return "^" + Regex.Escape(wildcard)
                .Replace(@"\*\*\\", @"(.*\\)?")
                .Replace(@"\*\*", ".*")
                .Replace(@"\*", @"[^\\]*")
                .Replace(@"\?", @"[^\\]") + "$";
        }
    }
}
