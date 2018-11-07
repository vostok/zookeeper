using System.Collections.Generic;
using org.apache.zookeeper.data;

namespace org.apache.zookeeper
{
    /// <summary>
    /// this class is the base class for all return values of ZooKeeper methods
    /// </summary>
    public abstract class NodeResult {
        /// <summary>
        /// The Node stat
        /// </summary>
        public readonly Stat Stat;

        internal NodeResult(Stat stat) {
            Stat = stat;
        }
    }

    /// <summary>
    /// this class is the return value of the public getDataAsync methods
    /// </summary>
    public class DataResult : NodeResult {
        /// <summary>
        /// The node data
        /// </summary>
        public readonly byte[] Data;

        internal DataResult(byte[] data, Stat stat) : base(stat) {
            Data = data;
        }
    }

    /// <summary>
    /// this class is the return value of the public getChildrenAsync methods
    /// </summary>
    public class ChildrenResult : NodeResult {
        /// <summary>
        /// The node's children
        /// </summary>
        public readonly List<string> Children;

        internal ChildrenResult(List<string> children, Stat stat) : base(stat) {
            {
                Children = children;
            }
        }
    }

    /// <summary>
    /// this class is the return value of the public getACLAsync methods
    /// </summary>
    public class ACLResult: NodeResult {

        /// <summary>
        /// The node acls
        /// </summary>
        public readonly List<ACL> Acls;

        internal ACLResult(List<ACL> acls, Stat stat)
            : base(stat)
        {
            Acls = acls;
        }
    }
}
