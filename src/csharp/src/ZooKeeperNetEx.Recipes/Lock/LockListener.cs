using System.Threading.Tasks;

namespace org.apache.zookeeper.recipes.@lock
{
	/// <summary>
	/// This class has two methods which are call
	/// back methods when a lock is acquired and 
	/// when the lock is released.
	/// 
	/// </summary>
	public interface LockListener
	{
		/// <summary>
		/// call back called when the lock 
		/// is acquired
		/// </summary>
		Task lockAcquired();

		/// <summary>
		/// call back called when the lock is 
		/// released.
		/// </summary>
		Task lockReleased();
	}

}