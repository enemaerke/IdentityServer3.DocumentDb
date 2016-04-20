using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer3.DocumentDb
{
    internal static class DateTimeOffsetExtensions
    {
        private static readonly DateTimeOffset s_epoch = new DateTimeOffset(new DateTime(1970, 1, 1));

        public static int ToEpoch(this DateTimeOffset date)
        {
            TimeSpan epochTimeSpan = date - s_epoch;
            return (int)epochTimeSpan.TotalSeconds;
        }

        public static DateTimeOffset FromEpoch(this int secondsSinceEpoch)
        {
            return s_epoch.AddSeconds(secondsSinceEpoch);
        }
    }
}
