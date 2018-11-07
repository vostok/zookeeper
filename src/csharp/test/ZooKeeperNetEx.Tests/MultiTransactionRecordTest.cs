using System.IO;
using org.apache.jute;
using org.apache.utils;
using Xunit;

namespace org.apache.zookeeper
{
    public sealed class MultiTransactionRecordTest : ClientBase
	{
        [Fact]
		public void testRoundTrip()
		{
			MultiTransactionRecord request = new MultiTransactionRecord();
			request.add(Op.check("check", 1));
			request.add(Op.create("create", "create data".UTF8getBytes(), ZooDefs.Ids.CREATOR_ALL_ACL, (int) ZooDefs.Perms.ALL));
			request.add(Op.delete("delete", 17));
			request.add(Op.setData("setData", "set data".UTF8getBytes(), 19));

			MultiTransactionRecord decodedRequest = codeDecode(request);

			Assert.assertEquals(request, decodedRequest);
		}

        [Fact]
		public void testEmptyRoundTrip()
		{
			MultiTransactionRecord request = new MultiTransactionRecord();
			MultiTransactionRecord decodedRequest = codeDecode(request);

            Assert.assertEquals(request, decodedRequest);
		}

		private MultiTransactionRecord codeDecode(MultiTransactionRecord request) {
		    var ms = new MemoryStream();
            BigEndianBinaryWriter baos = new BigEndianBinaryWriter(ms);
			BinaryOutputArchive boa = BinaryOutputArchive.getArchive(baos);
			request.serialize(boa, "request");
		    ms.Position = 0;

		    BinaryInputArchive bia = BinaryInputArchive.getArchive(new BigEndianBinaryReader(ms));
			MultiTransactionRecord decodedRequest = new MultiTransactionRecord();
			decodedRequest.deserialize(bia, "request");
			return decodedRequest;
		}
	}

}