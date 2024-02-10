using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Core
{
    public static class StringUtility
    {
        public const string NULL = "NULL";
        public const string EMPTY = "EMPTY";

        public static string GetCompositeString<T>(IEnumerable<T> collection, bool vertical = true, Func<T, string> getter = null,
            bool numerate = true,
            string separator = "")
        {
            if (collection == null) return string.Empty;

            return GetCompositeString(new List<T>(collection), vertical, getter, numerate, separator);
        }

        public static string GetCompositeString<T>(List<T> items, bool vertical = true, Func<T, string> getter = null,
            bool numerate = true,
            string separator = "")
        {
            if (items == null) return NULL;
            if (items.Count == 0) return EMPTY;

            var sb = new StringBuilder();

            for (int i = 0; i < items.Count; i++)
            {
                T item = items[i];

                string value =
                    item == null ? NULL :
                    getter != null ? getter.Invoke(item) :
                    item.ToString();

                string prefix = !separator.IsNullOrEmpty() ? separator :
                    numerate ? $"{i + 1}. " :
                    null;

                string next = vertical ? $"\n{prefix}{value}" : $"{prefix}{value} ";

                sb.Append(next);
            }

            return sb.ToString();
        }
    }
}