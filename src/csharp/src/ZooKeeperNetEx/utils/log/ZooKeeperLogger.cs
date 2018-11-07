using System;
using System.Diagnostics;

namespace org.apache.utils
{
    internal class ZooKeeperLogger
    {
        internal static readonly ZooKeeperLogger Instance = new ZooKeeperLogger();
        internal ILogConsumer CustomLogConsumer;
        internal TraceLevel LogLevel = TraceLevel.Warning;
        private readonly LogWriter logWriter = new LogWriter();
        internal bool LogToFile
        {
            get { return logWriter.LogToFile; }
            set { logWriter.LogToFile = value; }
        }

        internal bool LogToTrace
        {
            get { return logWriter.LogToTrace; }
            set { logWriter.LogToTrace = value; }
        }

        internal string LogFileName
        {
            get { return logWriter.FileName; }
        }

        internal void Log(TraceLevel sev, string className, string message, Exception exception = null)
        {
            if (sev > LogLevel)
            {
                return;
            }

            logWriter.Log(sev, className, message, exception);
            var logConsumer = CustomLogConsumer;
            if (logConsumer == null) return;
            try
            {
                logConsumer.Log(sev, className, message, exception);
            }
            catch (Exception e)
            {
                Trace.TraceError(
                    $@"Exception while passing a log message to log consumer. TraceLogger type:{logConsumer.GetType().FullName},
                       name:{className}, severity:{sev}, message:{message}, message exception:{exception}, log consumer exception:{e}");
            }
        }
    }
}