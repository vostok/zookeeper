using System;

namespace org.apache.utils
{
    internal interface ILogProducer
    {
        void debugFormat(string format, params object[] args);
        void debug(object message, Exception e = null);
        void warn(object message, Exception e = null);
        void info(object message, Exception e = null);
        void error(object message, Exception e = null);
        bool isDebugEnabled();
    }
}