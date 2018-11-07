using System.Collections.Concurrent;
using System.Threading.Tasks;
using org.apache.zookeeper.data;
using Xunit;

namespace org.apache.zookeeper.test
{
    public class WatcherTest : ClientBase
	{
        private class MyWatcher : Watcher {
            internal readonly BlockingCollection<WatchedEvent> events =
            new BlockingCollection<WatchedEvent>();

        public override Task process(WatchedEvent @event) {
            if (@event.get_Type() != Event.EventType.None) {
                events.Add(@event);
            }
            return CompletedTask;
        }
    }

		// <summary>
		// Verify that we get all of the events we expect to get. This particular
		// case verifies that we see all of the data events on a particular node.
		// There was a bug (ZOOKEEPER-137) that resulted in events being dropped
		// in some cases (timing).
		// </summary>
		// <exception cref="IOException"> </exception>
		// <exception cref="InterruptedException"> </exception>
		// <exception cref="KeeperException"> </exception>
        [Fact]
		public async Task testWatcherCorrectness()
		{
			ZooKeeper zk = null;
				MyWatcher watcher = new MyWatcher();
				zk = await createClient(watcher);

				string[] names = new string[10];
				for (int i = 0; i < names.Length; i++)
				{
					string name = await zk.createAsync("/tc-", "initialvalue".UTF8getBytes(), ZooDefs.Ids.OPEN_ACL_UNSAFE, CreateMode.PERSISTENT_SEQUENTIAL);
					names[i] = name;

					Stat stat = (await zk.getDataAsync(name, watcher)).Stat;
					await zk.setDataAsync(name, "new".UTF8getBytes(), stat.getVersion());
					stat = await zk.existsAsync(name, watcher);
					await zk.deleteAsync(name, stat.getVersion());
				}

				for (int i = 0; i < names.Length; i++)
				{
					string name = names[i];
					WatchedEvent @event = watcher.events.poll(10* 1000);
					Assert.assertEquals(name, @event.getPath());
					Assert.assertEquals(Watcher.Event.EventType.NodeDataChanged, @event.get_Type());
					Assert.assertEquals(Watcher.Event.KeeperState.SyncConnected, @event.getState());
					@event = watcher.events.poll(10* 1000);
					Assert.assertEquals(name, @event.getPath());
					Assert.assertEquals(Watcher.Event.EventType.NodeDeleted, @event.get_Type());
					Assert.assertEquals(Watcher.Event.KeeperState.SyncConnected, @event.getState());
				}
		}
	}

}