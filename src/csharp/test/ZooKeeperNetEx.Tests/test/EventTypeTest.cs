using System;
using org.apache.utils;
using Xunit;

namespace org.apache.zookeeper.test
{
    public sealed class EventTypeTest 
	{
        //[Fact]
        //public void testIntConversion()
        //{
        //    // Ensure that we can convert all valid integers to EventTypes
        //    IEnumerable<Watcher.Event.EventType> allTypes = EnumUtil<Watcher.Event.EventType>.GetValues();

        //    foreach (Watcher.Event.EventType et in allTypes) {
        //        Assert.assertEquals(et, (Watcher.Event.EventType) ((int) et));
        //    }
        //}

        [Fact]
		public void testInvalidIntConversion()
		{
			try {
			    Watcher.Event.EventType et = EnumUtil<Watcher.Event.EventType>.DefinedCast(324242);
				Assert.fail("Was able to create an invalid EventType via an integer");
			}
			catch (Exception)
			{
				// we're good.
			}

		}
	}
}