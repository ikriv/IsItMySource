using System;
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
    }
}
