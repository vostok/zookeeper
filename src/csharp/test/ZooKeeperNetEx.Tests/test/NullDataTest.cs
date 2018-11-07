using System.Threading.Tasks;
using Xunit;

namespace org.apache.zookeeper.test
{
    public sealed class NullDataTest : ClientBase {

        [Fact]
		public async Task testNullData()
		{
			const string path = "/SIZE";
            ZooKeeper zk = await createClient();
				await zk.createAsync(path, null, ZooDefs.Ids.OPEN_ACL_UNSAFE, CreateMode.PERSISTENT);
				// try sync zk exists 
				await zk.existsAsync(path, false);
				Assert.assertTrue(await zk.existsAsync(path, false).WithTimeout(10*1000));
		}
	}

}