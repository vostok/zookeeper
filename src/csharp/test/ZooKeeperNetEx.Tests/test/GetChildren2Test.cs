using System.Collections.Generic;
using System.Threading.Tasks;
using org.apache.zookeeper.data;
using Xunit;

namespace org.apache.zookeeper.test
{
    public sealed class GetChildren2Test : ClientBase
	{

        [Fact]
		public async Task testChild()
		{
            var zk = await createClient();
			string name = "/foo";
			await zk.createAsync(name, name.UTF8getBytes(), ZooDefs.Ids.OPEN_ACL_UNSAFE, CreateMode.PERSISTENT);

			string childname = name + "/bar";
			await zk.createAsync(childname, childname.UTF8getBytes(), ZooDefs.Ids.OPEN_ACL_UNSAFE, CreateMode.EPHEMERAL);

            var childrenResult = await zk.getChildrenAsync(name, false);

            Stat stat = childrenResult.Stat;

			Assert.assertEquals(stat.getCzxid(), stat.getMzxid());
			Assert.assertEquals(stat.getCzxid() + 1, stat.getPzxid());
			Assert.assertEquals(stat.getCtime(), stat.getMtime());
			Assert.assertEquals(1, stat.getCversion());
			Assert.assertEquals(0, stat.getVersion());
			Assert.assertEquals(0, stat.getAversion());
			Assert.assertEquals(0, stat.getEphemeralOwner());
			Assert.assertEquals(name.Length, stat.getDataLength());
			Assert.assertEquals(1, stat.getNumChildren());
			Assert.assertEquals(childrenResult.Children.Count, stat.getNumChildren());

            childrenResult = await zk.getChildrenAsync(childname, false);
            stat = childrenResult.Stat;

            Assert.assertEquals(stat.getCzxid(), stat.getMzxid());
			Assert.assertEquals(stat.getCzxid(), stat.getPzxid());
			Assert.assertEquals(stat.getCtime(), stat.getMtime());
			Assert.assertEquals(0, stat.getCversion());
			Assert.assertEquals(0, stat.getVersion());
			Assert.assertEquals(0, stat.getAversion());
			Assert.assertEquals(zk.getSessionId(), stat.getEphemeralOwner());
			Assert.assertEquals(childname.Length, stat.getDataLength());
			Assert.assertEquals(0, stat.getNumChildren());
			Assert.assertEquals(childrenResult.Children.Count, stat.getNumChildren());
		}



        [Fact]
		public async Task testChildren()
        {
            var zk = await createClient();

            string name = "/foo";
			await zk.createAsync(name, name.UTF8getBytes(), ZooDefs.Ids.OPEN_ACL_UNSAFE, CreateMode.PERSISTENT);

			IList<string> children = new List<string>();
			List<string> children_s = new List<string>();

			for (int i = 0; i < 10; i++)
			{
				string childname = name + "/bar" + i;
				string childname_s = "bar" + i;
				children.Add(childname);
				children_s.Add(childname_s);
			}

			for (int i = 0; i < children.Count; i++)
			{
				string childname = children[i];
				await zk.createAsync(childname, childname.UTF8getBytes(), ZooDefs.Ids.OPEN_ACL_UNSAFE, CreateMode.EPHEMERAL);

			    var childrenResult = await zk.getChildrenAsync(name, false);
                Stat stat = childrenResult.Stat;

				Assert.assertEquals(stat.getCzxid(), stat.getMzxid());
				Assert.assertEquals(stat.getCzxid() + i + 1, stat.getPzxid());
				Assert.assertEquals(stat.getCtime(), stat.getMtime());
				Assert.assertEquals(i + 1, stat.getCversion());
				Assert.assertEquals(0, stat.getVersion());
				Assert.assertEquals(0, stat.getAversion());
				Assert.assertEquals(0, stat.getEphemeralOwner());
				Assert.assertEquals(name.Length, stat.getDataLength());
				Assert.assertEquals(i + 1, stat.getNumChildren());
				Assert.assertEquals(childrenResult.Children.Count, stat.getNumChildren());
			}
			List<string> p = (await zk.getChildrenAsync(name, false)).Children;
			List<string> c_a = children_s;
			List<string> c_b = p;
			c_a.Sort();
			c_b.Sort();
			Assert.assertEquals(c_a.Count, 10);
			Assert.assertEquals(c_a, c_b);
		}
	}

}