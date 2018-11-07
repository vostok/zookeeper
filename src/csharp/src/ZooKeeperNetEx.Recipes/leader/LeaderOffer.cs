using System.Collections.Generic;

namespace org.apache.zookeeper.recipes.leader 
{
    /// <summary>
    ///     A leader offer is a numeric id / path pair. The id is the sequential node id
    ///     assigned by ZooKeeper where as the path is the absolute path to the ZNode.
    /// </summary>
    internal sealed class LeaderOffer {
        public string HostName;
        public int Id;
        public string NodePath;

        public LeaderOffer() {
            // Default constructor
        }

        public LeaderOffer(int id, string nodePath, string hostName) {
            Id = id;
            NodePath = nodePath;
            HostName = hostName;
        }

        public override string ToString() {
            return "{ id:" + Id + " nodePath:" + NodePath + " hostName:" + HostName + " }";
        }

        /// <summary>
        ///     Compare two instances of <seealso cref="LeaderOffer" /> using only the {code}id{code}
        ///     member.
        /// </summary>
        internal sealed class IdComparator : IComparer<LeaderOffer>
        {
            public int Compare(LeaderOffer o1, LeaderOffer o2) {
                return o1.Id.CompareTo(o2.Id);
            }
        }
    }
}