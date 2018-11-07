using org.apache.utils;
using org.apache.zookeeper.proto;

namespace org.apache.zookeeper
{
    /// <summary>
    /// an incoming event
    /// </summary>
    public class WatchedEvent {
        private readonly Watcher.Event.KeeperState keeperState;
        private readonly Watcher.Event.EventType eventType;
        private readonly string path;

        internal WatchedEvent(Watcher.Event.EventType eventType, Watcher.Event.KeeperState keeperState, string path) {
            this.keeperState = keeperState;
            this.eventType = eventType;
            this.path = path;
        }

        /**
     * Convert a WatcherEvent sent over the wire into a full-fledged WatcherEvent
     */

        internal WatchedEvent(WatcherEvent eventMessage) {
            keeperState = EnumUtil<Watcher.Event.KeeperState>.DefinedCast(eventMessage.getState());
            eventType = EnumUtil<Watcher.Event.EventType>.DefinedCast(eventMessage.get_Type());
            path = eventMessage.getPath();
        }

        /// <summary>
        /// Gets the state of the client.
        /// </summary>
        public Watcher.Event.KeeperState getState() {
            return keeperState;
        }

        /// <summary>
        /// Gets the node type.
        /// </summary>
        public Watcher.Event.EventType get_Type() {
            return eventType;
        }

        /// <summary>
        /// Gets the ndoe path.
        /// </summary>
        public string getPath() {
            return path;
        }

        /// <summary/>
        public override string ToString() {
            return "WatchedEvent state:" + keeperState
                   + " type:" + eventType + " path:" + path;
        }

        /**
     *  Convert WatchedEvent to type that can be sent over network
     */

        internal WatcherEvent getWrapper() {
            return new WatcherEvent((int)eventType,
                (int)keeperState,
                path);
        }
    }
}
