using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace ZooKeeperNetEx.utils
{
    // https://blogs.msdn.microsoft.com/pfxteam/2011/12/15/awaiting-socket-operations/
    // https://github.com/dotnet/corefx/blob/master/src/Common/tests/System/Threading/Tasks/Sources/ManualResetValueTaskSource.cs
    // https://github.com/dotnet/corefx/blob/master/src/System.Net.Sockets/src/System/Net/Sockets/Socket.Tasks.cs
    internal class AwaitableSignal : INotifyCompletion
    { 
        private static readonly Action s_sentinel = () => { };

		private readonly ThreadLocal<bool> _stackDiveOccured = new ThreadLocal<bool>();

        private readonly VolatileReference<Action> _continuation = new VolatileReference<Action>(null);

        public AwaitableSignal GetAwaiter() { return this; }

        public bool IsCompleted => _continuation.Value != null;

        public void OnCompleted(Action continuation) 
        { 
    		if (_continuation.CompareExchange(continuation, null) != null)
            {
                RunContinuation(continuation);
            }
        }

        public void TrySignal()
        {
            var continuation = _continuation.CompareExchange(s_sentinel, null);
            if (continuation != null && continuation != s_sentinel && 
                _continuation.CompareExchange(s_sentinel, continuation) == continuation)
            {
                RunContinuation(continuation);
            }
        }
        
        public void GetResult()
        {
        }

        public void Reset()
        {
            _continuation.Value = null;
        }

        private void RunContinuation(Action action)
        {
            if (_stackDiveOccured.Value)
            {
                _stackDiveOccured.Value = false;
                Task.Run(action);
            }
            else
            {
                _stackDiveOccured.Value = true;
                action();
            }
        }
    }
}
