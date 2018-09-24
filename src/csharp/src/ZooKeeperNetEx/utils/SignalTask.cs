/*
 *  Licensed to the Apache Software Foundation (ASF) under one or more
 *  contributor license agreements.  See the NOTICE file distributed with
 *  this work for additional information regarding copyright ownership.
 *  The ASF licenses this file to You under the Apache License, Version 2.0
 *  (the "License"); you may not use this file except in compliance with
 *  the License.  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 *
 */
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
