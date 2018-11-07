using System.Threading.Tasks;

namespace org.apache.zookeeper.recipes.@lock
{
	/// <summary>
	/// A callback object which can be used for implementing retry-able operations in the 
	/// <seealso cref="ProtocolSupport"/> class
	/// 
	/// </summary>
	public interface ZooKeeperOperation
	{

		/// <summary>
		/// Performs the operation - which may be involved multiple times if the connection
		/// to ZooKeeper closes during this operation
		/// </summary>
		/// <returns> the result of the operation or null </returns>
		/// <exception cref="KeeperException"> </exception>
		Task<bool> execute();
	}
}