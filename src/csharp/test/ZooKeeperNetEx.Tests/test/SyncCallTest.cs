using System;
using System.Threading.Tasks;
using org.apache.utils;
using Xunit;

namespace org.apache.zookeeper.test
{
    public sealed class SyncCallTest : ClientBase
	{
        private static readonly ILogProducer LOG = TypeLogger<SyncCallTest>.Instance;

        [Fact]
        public async Task testSync()
        {
            LOG.info("Starting ZK:" + DateTime.Now);

            ZooKeeper zk = await createClient();

            LOG.info("Beginning test:" + DateTime.Now);
            for (var i = 0; i < 100; i++)
            {
                await zk.createAsync("/test" + i, new byte[0], ZooDefs.Ids.OPEN_ACL_UNSAFE, CreateMode.PERSISTENT);
            }
            await zk.sync("/test");
            for (var i = 0; i < 100; i++)
            {
                await zk.deleteAsync("/test" + i, 0);
            }
            for (var i = 0; i < 100; i++)
            {
                await zk.getChildrenAsync("/", false);
            }
            for (var i = 0; i < 100; i++)
            {
                await zk.getChildrenAsync("/", false);
            }
            LOG.info("Submitted all operations:" + DateTime.Now);
        }
    }
}