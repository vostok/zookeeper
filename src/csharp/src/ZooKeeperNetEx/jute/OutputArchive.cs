namespace org.apache.jute
{
    using System.Collections.Generic;

    /**
 * Interface that alll the serializers have to implement.
 *
 */

    internal interface OutputArchive
    {
        void writeBool(bool b, string tag);
        void writeInt(int i, string tag);
        void writeLong(long l, string tag);
        void writeString(string s, string tag);
        void writeBuffer(byte[] buf, string tag);
        void writeRecord(Record r, string tag);
        void startVector<T>(List<T> v, string tag);
        void endVector<T>(List<T> v, string tag);
    }
}
