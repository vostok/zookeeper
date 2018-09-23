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

        private const int PENDING = 0;
        private const int COMPLETED = 1;
        private const int RESETING = 2;

        private Action _continuation;
        private int _state;

        public AwaitableSignal GetAwaiter() { return this; }

        public bool IsCompleted => Volatile.Read(ref _state) == COMPLETED;

        public void OnCompleted(Action continuation) 
        { 
            if (Interlocked.CompareExchange(ref _continuation, continuation, null) != null)
            {
                Task.Run(continuation);
            }
        }

        public bool TrySetCompleted()
        {
            return Interlocked.CompareExchange(ref _state, COMPLETED, PENDING) == PENDING;
        }

        public void TrySignal()
        {
            if (TrySetCompleted() && Interlocked.CompareExchange(ref _continuation, s_sentinel, null) != null)
            {
                _continuation();
            }
        }

        public void GetResult()
        {
            Volatile.Write(ref _state, RESETING);
            Volatile.Write(ref _continuation, null);
            Volatile.Write(ref _state, PENDING);
        }
    }
}
