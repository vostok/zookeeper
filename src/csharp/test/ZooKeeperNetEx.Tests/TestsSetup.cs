using Xunit;

namespace org.apache.zookeeper
{
    public class TestsSetup
    {
        static TestsSetup()
        {
            ZooKeeper.LogToFile = false;
            ZooKeeper.LogToTrace = false;
        }
    }

    [CollectionDefinition("Setup")]
    public class SetupCollection : ICollectionFixture<TestsSetup>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
