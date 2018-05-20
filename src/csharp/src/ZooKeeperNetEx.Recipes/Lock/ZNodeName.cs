using System;
using org.apache.utils;

// 
// <summary>
// Licensed to the Apache Software Foundation (ASF) under one or more
// contributor license agreements.  See the NOTICE file distributed with
// this work for additional information regarding copyright ownership.
// The ASF licenses this file to You under the Apache License, Version 2.0
// (the "License"); you may not use this file except in compliance with
// the License.  You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </summary>
namespace org.apache.zookeeper.recipes.@lock
{
	/// <summary>
	/// Represents an ephemeral znode name which has an ordered sequence number
	/// and can be sorted in order
	/// 
	/// </summary>
	internal sealed class ZNodeName : IComparable<ZNodeName> {
		public readonly string Name;
	    private readonly string prefix;
	    private readonly int sequence = -1;
	    private static readonly ILogProducer LOG = TypeLogger<ZNodeName>.Instance;

	    public ZNodeName(string name) {
	        if (name == null) {
	            throw new NullReferenceException("id cannot be null");
	        }

	        this.Name = name;
	        this.prefix = name;
	        int idx = name.LastIndexOf('-');
	        if (idx >= 0) {
	            this.prefix = name.Substring(0, idx);
	            if (idx + 1 >= name.Length) {
	                LOG.info("Array out of bounds for " + idx);
	            }
	            else if (!int.TryParse(name.Substring(idx + 1), out sequence)) {
                    // If an exception occurred we misdetected a sequence suffix,
	                // so return -1.
	                LOG.info("Number format exception for " + idx);
	            }
	        }
	    }

	    public override string ToString() {
			return Name.ToString();
		}

	    public override bool Equals(object obj) {
	        if (ReferenceEquals(null, obj)) return false;
	        if (ReferenceEquals(this, obj)) return true;
	        return obj is ZNodeName nodeName && string.Equals(Name, nodeName.Name);
	    }
		public override int GetHashCode() {
		    return Name.GetHashCode();
		}

	    /// <summary>
        /// Compare znodes based on their sequence number
        /// </summary>
        /// <param name="that">other znode to compare to</param>
        /// <returns>
        /// the difference between their sequence numbers: a positive value if this
        /// znode has a larger sequence number, 0 if they have the same sequence number
        /// or a negative number if this znode has a lower sequence number
        /// </returns>
        public int CompareTo(ZNodeName that) {
	        int answer = this.sequence - that.sequence;
	        if (answer == 0) {
	            return String.Compare(this.prefix, that.prefix, StringComparison.Ordinal);
	        }
	        return answer;
	    }
	}
}