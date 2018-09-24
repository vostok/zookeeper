using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ZooKeeperNetEx.utils;

namespace org.apache.utils
{
    internal class NonBlockingFileWriter
    {
        private readonly StreamWriterWrapper logOutput;

        private readonly VolatileBool isEnabled = new VolatileBool(true);

        private int pendingMessages;

        internal bool IsDisposed => logOutput.IsDisposed;
        internal bool IsEmpty => logQueue.IsEmpty;

        internal bool HasFailed => logOutput.HasFailed;
        
        internal bool ThrowWrite
        {
            get { return logOutput.ThrowWrite.Value; }
            set { logOutput.ThrowWrite.Value = value; }
        }

        private readonly ConcurrentQueue<string> logQueue = new ConcurrentQueue<string>();

        public NonBlockingFileWriter(string filename)
        {
            logOutput = new StreamWriterWrapper(filename);
        }

        public void Write(string str)
        {
            if (!isEnabled.Value && logOutput.IsDisposed) return;
            logQueue.Enqueue(str);
            if (Interlocked.Increment(ref pendingMessages) == 1)
            {
                startLogTask();
            }
        }

        public bool IsEnabled
        {
            get { return isEnabled.Value; }
            set { isEnabled.Value = value; }
        }

        private async void startLogTask()
        {
            do
            {
                if (logQueue.TryDequeue(out var msg))
                {
                    if (isEnabled.Value && !logOutput.HasFailed)
                    {
                        await logOutput.WriteAsync(msg).ConfigureAwait(false);
                    }
                    else logOutput.Dispose();
                }
            } while (Interlocked.Decrement(ref pendingMessages) > 0);
        }

        private class StreamWriterWrapper
        {
            private readonly string fileName;
            private readonly VolatileBool isDisposed = new VolatileBool(true);
            private readonly VolatileBool hasFailed = new VolatileBool(false);
            internal readonly VolatileBool ThrowWrite = new VolatileBool(false);
            private StreamWriter streamWriter;

            public StreamWriterWrapper(string filename)
            {
                fileName = filename;
            }

            public async Task WriteAsync(string msg)
            {
                try
                {
                    if (isDisposed.Value)
                    {
                        isDisposed.Value = false;
                        streamWriter = GetOrCreateLogFile();
                    }
                    if (ThrowWrite.Value)
                    {
                        throw new InvalidOperationException();
                    }
                    await streamWriter.WriteLineAsync(msg).ConfigureAwait(false);
                    await streamWriter.FlushAsync().ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    hasFailed.Value = true;
                    Trace.TraceError("Error while writing to log, will not try again. Exception:" + e);
                    Dispose();
                }
            }

            public void Dispose()
            {
                if (!isDisposed.Value)
                {
                    isDisposed.Value = true;
                    streamWriter?.Dispose();
                }
            }

            public bool IsDisposed => isDisposed.Value;

            public bool HasFailed => hasFailed.Value;

            private StreamWriter GetOrCreateLogFile()
            {
                var logFile = new FileInfo(fileName);
                return logFile.Exists ? logFile.AppendText() : logFile.CreateText();
            }
        }
    }
}
