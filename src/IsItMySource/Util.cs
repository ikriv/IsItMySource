using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace IKriv.IsItMySource
{
    internal static class Util
    {
        public static string ToHex(byte[] ba)
        {
            if (ba == null) return "(null)";
            return BitConverter.ToString(ba).Replace("-", "");
        }

        public static byte[] ToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length / 2)
                .Select(x => Convert.ToByte(hex.Substring(x * 2, 2), 16))
                .ToArray();
        }

        public static string GetRelativePath(string path, string root)
        {
            if (path == null) return null;
            if (String.IsNullOrEmpty(root)) return path;
            if (!path.StartsWith(root, true, CultureInfo.CurrentCulture)) return null;

            int len = root.Length;
            if (path.Length <= len) return null;
            if (path[len] == '\\') len++;
            if (path.Length <= len) return null;
            return path.Substring(len);
        }

        public static string GetCommonPrefix(IEnumerable<string> list)
        {
            int len = -1;
            string firstItem = null;

            foreach (var item in list)
            {
                if (item == null) continue;
                if (firstItem == null)
                {
                    firstItem = item;
                    len = item.Length;
                }
                else
                {
                    len = GetCommonPrefixLength(firstItem, item, Math.Min(len, item.Length));
                }

                if (len == 0) return String.Empty;
            }

            if (firstItem == null) return String.Empty; // no items in list
            if (len <= 0) return String.Empty; // no common prefix
            return firstItem.Substring(0, len);
        }

        public static string GetCommonRootDir(IEnumerable<string> paths)
        {
            var prefix = GetCommonPrefix(paths);
            if (prefix == "") return "";
            var lastSlash = prefix.LastIndexOf('\\');
            if (lastSlash <= 0) return "";
            return prefix.Substring(0, lastSlash);

        }

        private static int GetCommonPrefixLength(string s1, string s2, int maxLen)
        {
            for (int i = 0; i < maxLen; ++i)
            {
                if (s1[i] != s2[i]) return i;
            }

            return maxLen;
        }
    }
}
