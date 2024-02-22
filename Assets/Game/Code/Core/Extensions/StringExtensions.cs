using System;
using System.Collections.Generic;

namespace Game.Core
{
    public static class StringExtensions
    {
        public static string GetCompositeString<T>(this IEnumerable<T> collection, Func<T, string> getter = null,
            bool vertical = true,
            bool numerate = true,
            string separator = "")
        {
            if (collection == null) return string.Empty;
            return StringUtility.GetCompositeString(collection, vertical, getter, numerate, separator);
        }

        public static bool IsNullOrEmpty(this string source)
        {
            return string.IsNullOrEmpty(source);
        }
    }
}