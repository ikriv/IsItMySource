using System;
using System.Globalization;

namespace IKriv.IsItMySource
{
    internal static class Util
    {
        public static string ToHex(byte[] ba)
        {
            if (ba == null) return "(null)";
            return BitConverter.ToString(ba).Replace("-", "");
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
    }
}
