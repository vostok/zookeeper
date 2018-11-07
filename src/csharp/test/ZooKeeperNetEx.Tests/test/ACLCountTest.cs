using System.Collections.Generic;
using System.Threading.Tasks;
using org.apache.zookeeper.data;
using Xunit;

namespace org.apache.zookeeper.test
{
    public sealed class ACLCountTest : ClientBase
	{
		/// 
		/// <summary>
		/// Create a node and add 4 ACL values to it, but there are only 2 unique ACL values,
		/// and each is repeated once:
		/// 
		///   ACL(ZooDefs.Perms.READ,ZooDefs.Ids.ANYONE_ID_UNSAFE);
		///   ACL(ZooDefs.Perms.ALL,ZooDefs.Ids.AUTH_IDS);
		///   ACL(ZooDefs.Perms.READ,ZooDefs.Ids.ANYONE_ID_UNSAFE);
		///   ACL(ZooDefs.Perms.ALL,ZooDefs.Ids.AUTH_IDS);
		/// 
		/// Even though we've added 4 ACL values, there should only be 2 ACLs for that node,
		/// since there are only 2 *unique* ACL values.
		/// </summary>

        [Fact]
		public async Task testAclCount() {

		    List<ACL> CREATOR_ALL_AND_WORLD_READABLE = new List<ACL>
		    {
		        new ACL((int) ZooDefs.Perms.READ, ZooDefs.Ids.ANYONE_ID_UNSAFE),
		        new ACL((int) ZooDefs.Perms.ALL, ZooDefs.Ids.AUTH_IDS),
		        new ACL((int) ZooDefs.Perms.READ, ZooDefs.Ids.ANYONE_ID_UNSAFE),
		        new ACL((int) ZooDefs.Perms.ALL, ZooDefs.Ids.AUTH_IDS)
		    };
		        var zk = await createClient();

		        zk.addAuthInfo("digest", "pat:test".UTF8getBytes());
		        await zk.setACLAsync("/", ZooDefs.Ids.CREATOR_ALL_ACL, -1);

		        await zk.createAsync("/path", "/path".UTF8getBytes(), CREATOR_ALL_AND_WORLD_READABLE, CreateMode.PERSISTENT);
		        IList<ACL> acls = (await zk.getACLAsync("/path")).Acls;
		        Assert.assertEquals(2, acls.Count);
		        await zk.setACLAsync("/", ZooDefs.Ids.OPEN_ACL_UNSAFE, -1);
                await zk.setACLAsync("/path", ZooDefs.Ids.OPEN_ACL_UNSAFE, -1);
		}

	}

}