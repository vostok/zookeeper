using System;
using System.Diagnostics;

namespace org.apache.utils
{
    internal class TypeLogger<T> : ILogProducer
    {
        public static readonly ILogProducer Instance = new TypeLogger<T>();

        private static readonly string className = typeof (T).Name;

        public void debugFormat(string format, params object[] args)
        {
            // avoids exceptions if format string contains braces in calls that were not
            // designed to use format strings
            var message = args == null || args.Length == 0 ? format : string.Format(format, args);

            log(TraceLevel.Verbose, message);
        }

        public void debug(object message, Exception e = null)
        {
            log(TraceLevel.Verbose, message, e);
        }

        public void warn(object message, Exception e = null)
        {
            log(TraceLevel.Warning, message, e);
        }

        public void info(object message, Exception e = null)
        {
            log(TraceLevel.Info, message, e);
        }

        public void error(object message, Exception e = null)
        {
            log(TraceLevel.Error, message, e);
        }

        public bool isDebugEnabled()
        {
            return ZooKeeperLogger.Instance.LogLevel == TraceLevel.Verbose;
        }

        private static void log(TraceLevel traceLevel, object message, Exception e = null)
        {
            ZooKeeperLogger.Instance.Log(traceLevel, className, message.ToString(), e);
        }
    }
}