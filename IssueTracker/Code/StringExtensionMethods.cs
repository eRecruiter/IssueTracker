using System.Security.Cryptography;
using System.Text;
using System;

namespace IssueTracker {
    public static class StringExtensionMethods {

        public static string Hash(this string s) {
            if (s == null)
                return null;

            var bytes = MD5.Create().ComputeHash(Encoding.Default.GetBytes(s));
            var sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
                sb.Append(bytes[i].ToString("x2"));
            return sb.ToString();
        }

        public static bool IsNoE(this string s) {
            return string.IsNullOrEmpty(s);
        }

        public static bool HasValue(this string s) {
            return !s.IsNoE();
        }

        public static bool Is(this string s, string compareTo) {
            if (s == null && compareTo == null)
                return true;
            if (s == null || compareTo == null)
                return false;
            return s.Equals(compareTo, StringComparison.InvariantCultureIgnoreCase);
        }

    }
}