using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace org.apache.zookeeper
{
	/// <summary>
	/// Testing Zookeeper public methods
	/// </summary>
	public class ZooKeeperTest : ClientBase
	{
        [Fact]
		public async Task testDeleteRecursive()
		{
			ZooKeeper zk = await createClient();
			// making sure setdata works on /
            await zk.setDataAsync("/", "some".UTF8getBytes(), -1);
            await zk.createAsync("/a", "some".UTF8getBytes(), ZooDefs.Ids.OPEN_ACL_UNSAFE, CreateMode.PERSISTENT);

            await zk.createAsync("/a/b", "some".UTF8getBytes(), ZooDefs.Ids.OPEN_ACL_UNSAFE, CreateMode.PERSISTENT);

            await zk.createAsync("/a/b/v", "some".UTF8getBytes(), ZooDefs.Ids.OPEN_ACL_UNSAFE, CreateMode.PERSISTENT);

            await zk.createAsync("/a/b/v/1", "some".UTF8getBytes(), ZooDefs.Ids.OPEN_ACL_UNSAFE, CreateMode.PERSISTENT);

            await zk.createAsync("/a/c", "some".UTF8getBytes(), ZooDefs.Ids.OPEN_ACL_UNSAFE, CreateMode.PERSISTENT);

            await zk.createAsync("/a/c/v", "some".UTF8getBytes(), ZooDefs.Ids.OPEN_ACL_UNSAFE, CreateMode.PERSISTENT);

			IList<string> children = (await zk.getChildrenAsync("/a", false)).Children;

            Assert.assertEquals("2 children - b & c should be present ", children.Count, 2);
            Assert.assertTrue(children.Contains("b"));
            Assert.assertTrue(children.Contains("c"));

            ZKUtil.deleteRecursiveAsync(zk, "/a").GetAwaiter().GetResult();
            Assert.assertNull(await zk.existsAsync("/a", null));
		}

	}

}