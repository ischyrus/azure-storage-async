using System;
using System.Linq;
using System.Text;

namespace Porges.WindowsAzure.Storage.Async.Test.Integration
{
    internal static class TableName
    {
        private static readonly string ValidChars = MakeValidChars();
        private const int maxTableNameLength = 63;

        private static string MakeValidChars()
        {
            var s = new StringBuilder();
            foreach (var c in Enumerable.Range('a', 'z' - 'a'+1).Concat(Enumerable.Range('A', 'Z' - 'A'+1)).Concat(Enumerable.Range('0', '9'-'0'+1)))
                s.Append((char) c);
            return s.ToString();
        }

        public static string GenerateUnique(string prefix = "Test")
        {
            if (prefix.Length > maxTableNameLength) throw new ArgumentException("prefix too long", "prefix");

            var rng = new Random();

            var sb = new StringBuilder(maxTableNameLength);
            sb.Append(prefix);

            for (int i = 0; i < maxTableNameLength - prefix.Length; ++i)
            {
                sb.Append(ValidChars.SelectRandom(rng));
            }

            return sb.ToString();
        }
    }
}
