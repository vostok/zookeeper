using org.apache.utils;
using org.apache.zookeeper;
using System.Collections.Generic;

namespace org.apache.jute
{
    internal class BinaryOutputArchive : OutputArchive
    {
        private readonly BigEndianBinaryWriter writer;

        public static BinaryOutputArchive getArchive(BigEndianBinaryWriter writer)
        {
            return new BinaryOutputArchive(writer);
        }

        /** Creates a new instance of BinaryOutputArchive */

        private BinaryOutputArchive(BigEndianBinaryWriter writer)
        {
            this.writer = writer;
        }

        public void writeBool(bool b, string tag)
        {
            writer.Write(b);
        }

        public void writeInt(int i, string tag)
        {
            writer.Write(i);
        }

        public void writeLong(long l, string tag)
        {
            writer.Write(l);
        }

        public void writeString(string s, string tag)
        {
            if (s == null)
            {
                writeInt(-1, "len");
                return;
            }
            byte[] bb = s.UTF8getBytes();
            writeInt(bb.Length, "len");
            writer.Write(bb);
        }

        public void writeBuffer(byte[] barr, string tag)
        {
            if (barr == null)
            {
                writer.Write(-1);
                return;
            }
            writer.Write(barr.Length);
            writer.Write(barr);
        }

        public void writeRecord(Record r, string tag)
        {
            if (r == null) return;

            r.serialize(this, tag);
        }
        public void startVector<T>(List<T> v, string tag)
        {
            if (v == null)
            {
                writeInt(-1, tag);
                return;
            }
            writeInt(v.Count, tag);
        }

        public void endVector<T>(List<T> v, string tag) { }
    }
}
