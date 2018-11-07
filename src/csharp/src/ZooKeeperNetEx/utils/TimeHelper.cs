using System.Diagnostics;

namespace org.apache.utils
{
    internal static class TimeHelper
    {
        private static readonly Stopwatch stopwatch = new Stopwatch();

        static TimeHelper()
        {
            stopwatch.Start();
        }

        public static long ElapsedMiliseconds
        {
            get { return stopwatch.ElapsedMilliseconds; }
        }

        public static long ElapsedNanoseconds
        {
            get { return (long)((double)stopwatch.ElapsedTicks / Stopwatch.Frequency * 1000000000); }
        }
    }
}
