using System;
using org.apache.utils;
using Xunit;

namespace org.apache.zookeeper.test
{
    public sealed class KeeperStateTest
	{
        //[Fact]
        //public void testIntConversion()
        //{
        //    // Ensure that we can convert all valid integers to KeeperStates
        //    EnumSet<Watcher.Event.KeeperState> allStates = EnumSet.allOf(typeof(Watcher.Event.KeeperState));

        //    foreach (Watcher.Event.KeeperState @as in allStates)
        //    {
        //        Assert.assertEquals(@as, Watcher.Event.KeeperState.fromInt(@as.getIntValue()));
        //    }
        //}


        [Fact]
		public void testInvalidIntConversion()
		{
			try {
			    Watcher.Event.KeeperState ks = EnumUtil<Watcher.Event.KeeperState>.DefinedCast(324142);
				Assert.fail("Was able to create an invalid KeeperState via an integer");
			}
			catch (Exception)
			{
				// we're good.
			}

		}

		// <summary>
		// Validate that the deprecated constant still works. There were issues
		// found with switch statements - which need compile time constants.
		// </summary>


        //public void testDeprecatedCodeOkInSwitch()
        //{
        //    int test = 1;
        //    switch (test)
        //    {
        //    case KeeperException.Code.Ok:
        //        Assert.assertTrue(true);
        //        break;
        //    }
        //}

		// <summary>
		// Verify the enum works (paranoid) </summary>

        [Fact]
		public void testCodeOKInSwitch()
		{
			KeeperException.Code test = KeeperException.Code.OK;
			switch (test)
			{
                case KeeperException.Code.OK:
				Assert.assertTrue(true);
				break;
			}
		}
	}

}