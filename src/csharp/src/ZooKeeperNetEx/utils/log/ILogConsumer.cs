using System;
using System.Diagnostics;

namespace org.apache.utils
{
    /// <summary>
    /// This is for providing external loggers for ZooKeeper
    /// </summary>
    public interface ILogConsumer
    {
        /// <summary>
        /// The log method
        /// </summary>
        /// <param name="severity"></param>
        /// <param name="className"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        void Log(TraceLevel severity, string className, string message, Exception exception);
    }
}