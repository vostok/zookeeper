using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace org.apache.zookeeper
{
    internal static class MiscEx {
        public static TValue remove<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) {
            TValue value;
            if (dictionary.TryGetValue(key, out value))
                dictionary.Remove(key);
            return value;
        }

        public static bool isAlive(this ZooKeeper.States state) {
            return state != ZooKeeper.States.CLOSED && state != ZooKeeper.States.AUTH_FAILED;
        }

        /**
         * Returns whether we are connected to a server (which
         * could possibly be read-only, if this client is allowed
         * to go to read-only mode)
         * */

        public static bool isConnected(this ZooKeeper.States state) {
            return state == ZooKeeper.States.CONNECTED || state == ZooKeeper.States.CONNECTEDREADONLY;
        }

        public static int size(this ICollection collection) {
            return collection.Count;
        }

        public static void addAll<T>(this HashSet<T> hashSet, HashSet<T> another) {
            hashSet.UnionWith(another);
        }

        public static byte[] UTF8getBytes(this string str) {
            return Encoding.UTF8.GetBytes(str);
        }

        public static string UTF8bytesToString(this byte[] buffer)
        {
            return Encoding.UTF8.GetString(buffer, 0, buffer.Length);
        }

        public static string ToHexString(this long num) {
            return num.ToString("x");
        }
        public static string ToHexString(this byte num)
        {
            return num.ToString("x");
        }

        public static string ToCommaDelimited<T>(this IEnumerable<T> enumerable)
        {
            return string.Join(",", enumerable);
        }
    }
}