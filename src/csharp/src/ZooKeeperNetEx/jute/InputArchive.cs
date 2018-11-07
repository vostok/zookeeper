namespace org.apache.jute
{
    /**
 * Interface that all the Deserializers have to implement.
 *
 */

    internal interface InputArchive
    {
        bool readBool(string tag);
        int readInt(string tag);
        long readLong(string tag);
        string readString(string tag);
        byte[] readBuffer(string tag);
        void readRecord(Record r, string tag);
        Index startVector(string tag);
    }
}
