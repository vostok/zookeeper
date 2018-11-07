using System.IO;
using org.apache.jute;
using org.apache.utils;
using Xunit;

namespace org.apache.zookeeper
{
    public class BinaryInputArchiveTest
    {

        [Fact]
        public void testReadStringCheckLength()
        {
            byte[] buf = {byte.MaxValue >> 1, byte.MaxValue, byte.MaxValue, byte.MaxValue};
            BigEndianBinaryReader isz = new BigEndianBinaryReader(new MemoryStream(buf));
            BinaryInputArchive ia = BinaryInputArchive.getArchive(isz);
            try
            {
                ia.readString("");
                Assert.fail("Should have thrown an IOException");
            }
            catch (IOException e)
            {
                Assert.assertTrue("Not 'Unreasonable length' exception: " + e,
                    e.Message.StartsWith(BinaryInputArchive.UNREASONBLE_LENGTH));
            }
        }
    }
}