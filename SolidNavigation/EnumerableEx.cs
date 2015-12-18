using System;
using System.Collections.Generic;
using System.Text;

namespace SolidNavigation {
    public static class EnumerableEx {
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action) {
            foreach (var item in list) {
                action(item);
            }
        }

        public static IEnumerable<T> AsEnumerable<T>(this T item) {
            yield return item;
        }

        public static object IfType<T>(this object item, Action<T> action) where T : class {
            if (item is T) {
                action(item as T);
            }
            return item;
        }

        public static string ToString<T>(this IEnumerable<T> list, string separator) {
            var sb = new StringBuilder();
            foreach (var obj in list) {
                if (sb.Length > 0) {
                    sb.Append(separator);
                }
                sb.Append(obj);
            }
            return sb.ToString();
        }
    }
}
