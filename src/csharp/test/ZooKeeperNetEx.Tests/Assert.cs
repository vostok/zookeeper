using System.Collections.Concurrent;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AssertXunit=Xunit.Assert;

namespace org.apache.zookeeper 
{
    internal static class Assert
    {
        public static void assertTrue(bool test)
        {
            AssertXunit.True(test);
        }
        public static void assertTrue(string msg, bool test)
        {
            AssertXunit.True(test, msg);
        }

        public static void assertFalse(bool test)
        {
            AssertXunit.False(test);
        }
        public static void assertFalse(string msg, bool test)
        {
            AssertXunit.False(test, msg);
        }

        public static void assertNull(object obj)
        {
            AssertXunit.Null(obj);
        }
        public static void assertNull(string msg, object obj)
        {
            AssertXunit.True(obj == null, msg);
        }

        public static void assertNotNull(object obj)
        {
            AssertXunit.NotNull(obj);
        }
        public static void assertNotNull(string msg, object obj)
        {
            AssertXunit.True(obj != null, msg);
        }

        public static void fail()
        {
            AssertXunit.True(false);
        }
        public static void fail(string msg)
        {
            AssertXunit.True(false, msg);
        }

        public static void assertEquals(object a, object b)
        {
            string aJson = JsonConvert.SerializeObject(a);
            string bJson = JsonConvert.SerializeObject(b);
            AssertXunit.Equal(aJson, bJson);
        }
        public static void assertEquals(string msg, object a, object b)
        {
            string aJson = JsonConvert.SerializeObject(a);
            string bJson = JsonConvert.SerializeObject(b);
            AssertXunit.True(aJson == bJson, msg);
        }
        
        public static void assertNotEquals<T>(T a, T b)
        {
            string aJson = JsonConvert.SerializeObject(a);
            string bJson = JsonConvert.SerializeObject(b);
            AssertXunit.NotEqual(aJson, bJson);
        }

        public static T poll<T>(this BlockingCollection<T> blockingCollection, int milliSeconds)
        {
            T item;
            blockingCollection.TryTake(out item, milliSeconds);
            return item;
        }

        public static async Task<bool> WithTimeout(this Task task, int millisecondsTimeout)
        {
            Task delayTask = Task.Delay(millisecondsTimeout);
            await Task.WhenAny(task, delayTask).ConfigureAwait(false);
            return !delayTask.IsCompleted;
        }
    }
}