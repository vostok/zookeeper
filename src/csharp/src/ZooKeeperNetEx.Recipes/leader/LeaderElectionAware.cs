using System.Threading.Tasks;

namespace org.apache.zookeeper.recipes.leader {
    /// <summary>
    ///     An interface to be implemented by clients that want to receive election
    ///     events.
    /// </summary>
    public interface LeaderElectionAware {
        /// <summary>
        ///     Called during each state transition. Current, low level events are provided
        ///     at the beginning and end of each state. For instance, START may be followed
        ///     by OFFER_START, OFFER_COMPLETE, DETERMINE_START, DETERMINE_COMPLETE, and so
        ///     on.
        /// </summary>
        /// <param name="eventType"> </param>
        Task onElectionEvent(ElectionEventType eventType);
    }
}