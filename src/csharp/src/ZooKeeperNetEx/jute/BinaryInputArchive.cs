using org.apache.utils;
using org.apache.zookeeper;
using System.IO;

namespace org.apache.jute
{
    internal class BinaryInputArchive : InputArchive
    {
        public const string UNREASONBLE_LENGTH = "Unreasonable length = ";
        private readonly BigEndianBinaryReader reader;

        public static BinaryInputArchive getArchive(BigEndianBinaryReader reader)
        {
            return new BinaryInputArchive(reader);
        }

        private class BinaryIndex : Index
        {
            private int nelems;

            internal BinaryIndex(int nelems)
            {
                this.nelems = nelems;
            }
            public bool done()
            {
                return (nelems <= 0);
            }
            public void incr()
            {
                nelems--;
            }
        }
        
        /** Creates a new instance of BinaryInputArchive */

        private BinaryInputArchive(BigEndianBinaryReader reader)
        {
            this.reader = reader;
        }

        public bool readBool(string tag)
        {
            return reader.ReadBoolean();
        }

        public int readInt(string tag)
        {
            return reader.ReadInt32();
        }

        public long readLong(string tag)
        {
            return reader.ReadInt64();
        }

        public string readString(string tag)
        {
            int len = reader.ReadInt32();
            if (len == -1) return null;
            checkLength(len);
            var b = reader.ReadBytesOrThrow(len);
            return b.UTF8bytesToString();
        }

        public byte[] readBuffer(string tag)
        {
            int len = readInt(tag);
            if (len == -1) return null;
            checkLength(len);
            var arr = reader.ReadBytesOrThrow(len);
            return arr;
        }

        // Since this is a rough sanity check, add some padding to maxBuffer to
        // make up for extra fields, etc. (otherwise e.g. clients may be able to
        // write buffers larger than we can read from disk!)
        private void checkLength(int len)
        {
        if (len < 0 || len > ClientCnxn.packetLen + 1024) {
                throw new IOException(UNREASONBLE_LENGTH + len);
            }
        }

        public void readRecord(Record r, string tag)
        {
            r.deserialize(this, tag);
        }
        public Index startVector(string tag)
        {
            int len = readInt(tag);
            if (len == -1)
            {
                return null;
            }
            return new BinaryIndex(len);
        }
    }
}
