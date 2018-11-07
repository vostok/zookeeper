using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace org.apache.zookeeper.recipes.leader {
    public sealed class LeaderElectionSupportTest : ClientBase {
        private static int globalCounter;
        private readonly string root = "/" + Interlocked.Increment(ref globalCounter);
        private ZooKeeper zooKeeper;

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            zooKeeper = await createClient();

            await zooKeeper.createAsync(root, new byte[0],
                ZooDefs.Ids.OPEN_ACL_UNSAFE, CreateMode.PERSISTENT);
        }

        [Fact]
        public async Task testNode() {
            var electionSupport = createLeaderElectionSupport();

            await electionSupport.start();
            await Task.Delay(3000);
            await electionSupport.stop();
        }

        private async Task CreateTestNodesTask(int testIterations, int millisecondsDelay)
        {
            Assert.assertTrue(await Task.WhenAll(Enumerable.Repeat(runElectionSupportTask(), testIterations))
                        .WithTimeout(millisecondsDelay));
        }

        [Fact]
        public Task testNodes3() {
            return CreateTestNodesTask(3, 10 * 1000);
        }

        [Fact]
        public Task testNodes9() {
            return CreateTestNodesTask(9, 10 * 1000);
        }

        [Fact]
        public Task testNodes20() {
            return CreateTestNodesTask(20, 10 * 1000);
        }

        [Fact]
        public Task testNodes100() {
            return CreateTestNodesTask(100, 20 * 1000);
        }

        [Fact]
        public async Task testOfferShuffle() {
            const int testIterations = 10;

            var elections = Enumerable.Range(1, testIterations)
                .Select(i => runElectionSupportTask(Math.Min(i*1200, 10000)));
            Assert.assertTrue(await Task.WhenAll(elections).WithTimeout(60*1000));
        }

        [Fact]
        public async Task testGetLeaderHostName() {
            var electionSupport = createLeaderElectionSupport();

            await electionSupport.start();

            // Sketchy: We assume there will be a leader (probably us) in 3 seconds.
            await Task.Delay(3000);

            var leaderHostName = await electionSupport.getLeaderHostName();

            Assert.assertNotNull(leaderHostName);
            Assert.assertEquals("foohost", leaderHostName);

            await electionSupport.stop();
        }

        private LeaderElectionSupport createLeaderElectionSupport()
        {
            return new LeaderElectionSupport(zooKeeper, root, "foohost");
        }

        private async Task runElectionSupportTask(int sleepDuration = 3000)
        {
            var electionSupport = createLeaderElectionSupport();

            await electionSupport.start();
            await Task.Delay(sleepDuration);
            await electionSupport.stop();
        }
    }
}