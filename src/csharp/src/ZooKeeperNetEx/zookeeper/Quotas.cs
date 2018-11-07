namespace org.apache.zookeeper
{
    /// <summary>
    /// Path Quotas
    /// </summary>
    public static class Quotas {
        /// <summary>
        /// the zookeeper nodes that acts as the management and status node
        /// </summary>
        public const string procZookeeper = "/zookeeper";
        
        /// <summary>
        /// the zookeeper quota node that acts as the quota management node for zookeeper
        /// </summary>
        public const string quotaZookeeper = "/zookeeper/quota";
        
        /// <summary>
        /// the limit node that has the limit of a subtree
        /// </summary>
        public const string limitNode = "zookeeper_limits";
        
        /// <summary>
        /// the stat node that monitors the limit of a subtree.
        /// </summary>
        public const string statNode = "zookeeper_stats";
        
        /// <summary>
        /// return the quota path associated with this prefix.
        /// </summary>
        /// <param name="path">the actual path in zookeeper</param>
        /// <returns>the limit quota path</returns>
        public static string quotaPath(string path) {
            return quotaZookeeper + path +
                   "/" + limitNode;
        }
        
        /// <summary>
        /// return the stat quota path associated with this prefix.
        /// </summary>
        /// <param name="path">the actual path in zookeeper</param>
        /// <returns>the stat quota path</returns>
        public static string statPath(string path) {
            return quotaZookeeper + path + "/" +
                   statNode;
        }
    }
}
