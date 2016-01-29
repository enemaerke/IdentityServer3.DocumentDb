using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer3.DocumentDb.Util
{
    public static class ReflectionUtil
    {
        public static TValue GetAttributeValue<TType, TAttribute, TValue>(Func<TAttribute, TValue> valueFunc)
        {
            Type type = typeof(TType);
            var attribute = type.GetCustomAttributes(typeof(TAttribute), false).Cast<TAttribute>().First();
            return valueFunc(attribute);
        }
    }
}
