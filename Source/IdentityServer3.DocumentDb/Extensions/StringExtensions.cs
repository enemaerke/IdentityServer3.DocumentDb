using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer3.DocumentDb.Extensions
{
    internal static class StringExtensions
    {
        internal static string JoinToString(this IEnumerable<string> strings, string separator,
            Func<string, string> stringifier = null)
        {
            if (strings == null)
                return string.Empty;

            stringifier = stringifier ?? new Func<string, string>(str => str.ToString());

            StringBuilder sb = new StringBuilder();
            foreach (var s in strings)
            {
                if (sb.Length != 0)
                    sb.Append(separator);
                sb.Append(stringifier(s));
            }
            return sb.ToString();
        }
    }
}
