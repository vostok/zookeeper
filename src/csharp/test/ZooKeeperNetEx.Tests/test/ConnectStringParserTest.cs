using org.apache.zookeeper.client;
using Xunit;

namespace org.apache.zookeeper.test
{
    public class ConnectStringParserTest
	{
        [Fact]
		public void testSingleServerChrootPath()
		{
			const string chrootPath = "/hallo/welt";
			const string servers = "10.10.10.1";
			assertChrootPath(chrootPath, new ConnectStringParser(servers + chrootPath));
		}

        [Fact]
		public void testMultipleServersChrootPath()
		{
			const string chrootPath = "/hallo/welt";
			const string servers = "10.10.10.1,10.10.10.2";
			assertChrootPath(chrootPath, new ConnectStringParser(servers + chrootPath));
		}

        [Fact]
		public void testParseServersWithoutPort()
		{
			const string servers = "10.10.10.1,10.10.10.2";
			ConnectStringParser parser = new ConnectStringParser(servers);

			Assert.assertEquals("10.10.10.1", parser.getServerAddresses()[0].Host);
            Assert.assertEquals("10.10.10.2", parser.getServerAddresses()[1].Host);
		}

        [Fact]
		public void testParseServersWithPort()
		{
			const string servers = "10.10.10.1:112,10.10.10.2:110";
			ConnectStringParser parser = new ConnectStringParser(servers);

            Assert.assertEquals("10.10.10.1", parser.getServerAddresses()[0].Host);
            Assert.assertEquals("10.10.10.2", parser.getServerAddresses()[1].Host);

            Assert.assertEquals(112, parser.getServerAddresses()[0].Port);
            Assert.assertEquals(110, parser.getServerAddresses()[1].Port);
		}

		private void assertChrootPath(string expected, ConnectStringParser parser)
		{
			Assert.assertEquals(expected, parser.getChrootPath());
		}
	}

}