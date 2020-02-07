using org.apache.utils;

namespace org.apache.zookeeper
{
    /// <summary>
    /// The type of node creation
    /// </summary>
    public sealed class CreateMode
    {
        /// <summary>
        /// The znode will not be automatically deleted upon client's disconnect.
        /// </summary>
        public static readonly CreateMode PERSISTENT = new CreateMode(0, false, false, false, false);

        /// <summary>
        /// The znode will not be automatically deleted upon client's disconnect,
        /// and its name will be appended with a monotonically increasing number.
        /// </summary>
        public static readonly CreateMode PERSISTENT_SEQUENTIAL = new CreateMode(2, false, true, false, false);

        /// <summary>
        /// The znode will be deleted upon the client's disconnect.
        /// </summary>
        public static readonly CreateMode EPHEMERAL = new CreateMode(1, true, false, false, false);

        /// <summary>
        /// The znode will be deleted upon the client's disconnect, and its name
        /// will be appended with a monotonically increasing number.
        /// </summary>
        public static readonly CreateMode EPHEMERAL_SEQUENTIAL = new CreateMode(3, true, true, false, false);

        /// <summary>
        /// The znode will be a container node. Container
        /// nodes are special purpose nodes useful for recipes such as leader, lock,
        /// etc.When the last child of a container is deleted, the container becomes
        /// a candidate to be deleted by the server at some point in the future.
        /// Given this property, you should be prepared to get
        /// <see cref="KeeperException.NoNodeException"/>
        /// when creating children inside of this container node.
        /// </summary>
        public static readonly CreateMode CONTAINER = new CreateMode(4, false, false, true, false);

        /// <summary>
        /// The znode will not be automatically deleted upon client's disconnect.
        /// However if the znode has not been modified within the given TTL, it
		/// will be deleted once it has no children.
        /// </summary>  
        public static readonly CreateMode PERSISTENT_WITH_TTL = new CreateMode(5, false, false, false, true);

        /// <summary>
        /// The znode will not be automatically deleted upon client's disconnect,
        /// and its name will be appended with a monotonically increasing number.
        /// However if the znode has not been modified within the given TTL, it
        /// will be deleted once it has no children.
        /// </summary>       
        public static readonly CreateMode PERSISTENT_SEQUENTIAL_WITH_TTL = new CreateMode(6, false, true, false, true);

        private static readonly ILogProducer LOG = TypeLogger<CreateMode>.Instance;

        private readonly bool ephemeral;
        private readonly bool sequential;
        private readonly bool container;
        private readonly int flag;
        private readonly bool ttl;

        private CreateMode(int flag, bool ephemeral, bool sequential,
		                   bool container, bool ttl) {
            this.flag = flag;
            this.ephemeral = ephemeral;
            this.sequential = sequential;
            this.container = container;
            this.ttl = ttl;
        }

        /// <summary>
        /// Determines whether this instance is ephemeral.
        /// </summary>
        public bool isEphemeral() {
            return ephemeral;
        }

        /// <summary>
        /// Determines whether this instance is sequential.
        /// </summary>
        public bool isSequential() {
            return sequential;
        }

        /// <summary>
        /// Determines whether this instance is a container.
        /// </summary>
        public bool isContainer() {
            return container;
        }

        /// <summary>
        /// Determines whether this instance has ttl.
        /// </summary>
        public bool isTTL() {
            return ttl;
        }

        internal int toFlag() {
            return flag;
        }

    /**
     * Map an integer value to a CreateMode value
     */
        internal static CreateMode fromFlag(int flag) {
            switch (flag) {
                case 0: return PERSISTENT;
                case 1: return EPHEMERAL;
                case 2: return PERSISTENT_SEQUENTIAL;
                case 3: return EPHEMERAL_SEQUENTIAL;
                case 4: return CONTAINER;
                case 5: return PERSISTENT_WITH_TTL;
                case 6: return PERSISTENT_SEQUENTIAL_WITH_TTL;

                default:
                    string errMsg = "Received an invalid flag value: " + flag
                            + " to convert to a CreateMode";
                    LOG.error(errMsg);
                    throw new KeeperException.BadArgumentsException(errMsg);
            }
        }
    }
}