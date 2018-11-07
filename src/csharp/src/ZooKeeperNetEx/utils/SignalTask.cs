using System.Threading.Tasks;
using ZooKeeperNetEx.utils;

namespace org.apache.utils
{
    /// <summary>
    /// Uses <see cref="T:System.Threading.Tasks.TaskCompletionSource" />s for signals
    /// </summary>
    public class SignalTask
    {
        private readonly VolatileReference<TaskCompletionSource<bool>> tcs = new VolatileReference<TaskCompletionSource<bool>>(new TaskCompletionSource<bool>());

        /// <summary>
        /// Gets the underlying <see cref="T:System.Threading.Tasks.Task`1" />.
        /// </summary>
        public Task Task => tcs.Value.Task;

        /// <summary>
        /// Replaces the underlying <see cref="T:System.Threading.Tasks.TaskCompletionSource" /> with a new one.
        /// </summary>
        public void Reset()
        {
            tcs.Value = new TaskCompletionSource<bool>();
        }

        /// <summary>Attempts to transition the underlying <see cref="T:System.Threading.Tasks.Task`1" /> into the <see cref="F:System.Threading.Tasks.TaskStatus.RanToCompletion" /> state.</summary>
        public void TrySet()
        {
            tcs.Value.TrySetResult(true);
        }

        /// <summary>Transitions the underlying <see cref="T:System.Threading.Tasks.Task`1" /> into the <see cref="F:System.Threading.Tasks.TaskStatus.RanToCompletion" /> state.</summary>
        /// <exception cref="T:System.InvalidOperationException">The underlying <see cref="T:System.Threading.Tasks.Task`1" /> is already in state: <see cref="F:System.Threading.Tasks.TaskStatus.RanToCompletion" /> </exception>
        public void Set()
        {
            tcs.Value.SetResult(true);
        }
    }
}
