using System.IO;
using org.apache.jute;
using org.apache.utils;
using org.apache.zookeeper.data;
using Xunit;

namespace org.apache.zookeeper
{
    public sealed class MultiResponseTest : ClientBase
	{
        [Fact]
		public void testRoundTrip()
		{
			MultiResponse response = new MultiResponse();

			response.add(new OpResult.CheckResult());
			response.add(new OpResult.CreateResult("foo-bar"));
			response.add(new OpResult.DeleteResult());

			Stat s = new Stat();
			s.setCzxid(546);
			response.add(new OpResult.SetDataResult(s));

			MultiResponse decodedResponse = codeDecode(response);

			Assert.assertEquals(response, decodedResponse);
		}

        [Fact]
		public void testEmptyRoundTrip()
		{
			MultiResponse result = new MultiResponse();
			MultiResponse decodedResult = codeDecode(result);

            Assert.assertEquals(result, decodedResult);
		}

		private static MultiResponse codeDecode(MultiResponse request)
		{
            var ms = new MemoryStream();
            BigEndianBinaryWriter baos = new BigEndianBinaryWriter(ms);
			BinaryOutputArchive boa = BinaryOutputArchive.getArchive(baos);
            request.serialize(boa, "result");
		    ms.Position = 0;

		    BinaryInputArchive bia = BinaryInputArchive.getArchive(new BigEndianBinaryReader(ms));
            MultiResponse decodedRequest = new MultiResponse();
            decodedRequest.deserialize(bia, "result");
			return decodedRequest;
		}

	}

}