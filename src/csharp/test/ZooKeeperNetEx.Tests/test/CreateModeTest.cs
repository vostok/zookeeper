using System.Collections.Generic;
using Xunit;

namespace org.apache.zookeeper.test
{
    public sealed class CreateModeTest
	{
        [Fact]
		public void testBasicCreateMode()
		{
			CreateMode cm = CreateMode.PERSISTENT;
			Assert.assertEquals(cm.toFlag(), 0);
			Assert.assertFalse(cm.isEphemeral());
			Assert.assertFalse(cm.isSequential());

			cm = CreateMode.EPHEMERAL;
			Assert.assertEquals(cm.toFlag(), 1);
			Assert.assertTrue(cm.isEphemeral());
			Assert.assertFalse(cm.isSequential());

			cm = CreateMode.PERSISTENT_SEQUENTIAL;
			Assert.assertEquals(cm.toFlag(), 2);
			Assert.assertFalse(cm.isEphemeral());
			Assert.assertTrue(cm.isSequential());

			cm = CreateMode.EPHEMERAL_SEQUENTIAL;
			Assert.assertEquals(cm.toFlag(), 3);
			Assert.assertTrue(cm.isEphemeral());
			Assert.assertTrue(cm.isSequential());
		}

        [Fact]
        public void testFlagConversion() {
            // Ensure we get the same value back after round trip conversion
            IEnumerable<CreateMode> allModes = new List<CreateMode>
            {
                CreateMode.EPHEMERAL,
                CreateMode.EPHEMERAL_SEQUENTIAL,
                CreateMode.PERSISTENT,
                CreateMode.PERSISTENT_SEQUENTIAL
            };

            foreach (CreateMode cm in allModes) {
                Assert.assertEquals(cm, CreateMode.fromFlag(cm.toFlag()));
            }
        }

        [Fact]
		public void testInvalidFlagConversion()
		{
			try
			{
				CreateMode cm = CreateMode.fromFlag(99);
				Assert.fail("Shouldn't be able to convert 99 to a CreateMode.");
			}
			catch (KeeperException ke)
			{
				Assert.assertEquals(KeeperException.Code.BADARGUMENTS, ke.getCode());
			}

			try
			{
				CreateMode cm = CreateMode.fromFlag(-1);
				Assert.fail("Shouldn't be able to convert -1 to a CreateMode.");
			}
			catch (KeeperException ke)
			{
                Assert.assertEquals(KeeperException.Code.BADARGUMENTS, ke.getCode());
			}
		}
	}

}