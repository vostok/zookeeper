using System.Threading.Tasks;

namespace org.apache.zookeeper.client
{

	/// <summary>
	/// A set of hosts a ZooKeeper client should connect to.
	/// 
	/// Classes implementing this interface must guarantee the following:
	/// 
    /// * Every call to next() returns an IPEndPoint. So the iterator never
	/// ends.
	/// 
	/// * The size() of a HostProvider may never be zero.
	/// 
    /// A HostProvider must return resolved IPEndPoint instances on next(),
	/// but it's up to the HostProvider, when it wants to do the resolving.
	/// 
	/// Different HostProvider could be imagined:
	/// 
	/// * A HostProvider that loads the list of Hosts from an URL or from DNS 
    /// * A HostProvider that re-resolves the IPEndPoint after a timeout. 
	/// * A HostProvider that prefers nearby hosts.
	/// </summary>
	internal interface HostProvider
	{
		int size();

	    /// <summary>
	    /// The next host to try to connect to.
	    /// 
	    /// For a spinDelay of 0 there should be no wait.
	    /// </summary>
	    /// <param name="spinDelay">
	    ///     Milliseconds to wait if all hosts have been tried once. </param>
	    Task<ResolvedEndPoint> next(int spinDelay);

		/// <summary>
		/// Notify the HostProvider of a successful connection.
		/// 
		/// The HostProvider may use this notification to reset it's inner state.
		/// </summary>
		void onConnected();
	}

}