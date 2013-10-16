using System.Security.Cryptography;
using System.Text;

namespace IssueTracker.Web.Code {
    public static class StringExtensionMethods {

        public static string Hash(this string s) {
            if (s == null)
                return null;

            var bytes = MD5.Create().ComputeHash(Encoding.Default.GetBytes(s));
            var sb = new StringBuilder();
            foreach (var t in bytes)
                sb.Append(t.ToString("x2"));
            return sb.ToString();
        }
    }
}