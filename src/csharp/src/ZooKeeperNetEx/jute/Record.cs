namespace org.apache.jute
{
    /**
 * Interface that is implemented by generated classes.
 * 
 */

    internal interface Record
    {
        void serialize(OutputArchive archive, string tag);
        void deserialize(InputArchive archive, string tag);
    }
}
