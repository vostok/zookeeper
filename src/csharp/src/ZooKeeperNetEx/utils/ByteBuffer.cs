using System.IO;

namespace org.apache.utils
{
    internal class ByteBuffer
    {
        public readonly MemoryStream Stream;
        private Mode mode;

        private ByteBuffer() : this(new MemoryStream())
        {
        }

        public ByteBuffer(MemoryStream stream)
        {
            Stream = stream;
        }

        public static ByteBuffer allocate(int capacity)
        {
            var buffer = new ByteBuffer {Stream = {Capacity = capacity}, mode = Mode.Write};
            return buffer;
        }
        
        public void flip()
        {
            mode = Mode.Read;
            Stream.SetLength(Stream.Position);
            Stream.Position = 0;
        }

        public void clear()
        {
            mode = Mode.Write;
            Stream.Position = 0;
        }

        public int limit()
        {
            if (mode == Mode.Write)
                return Stream.Capacity;
            return (int) Stream.Length;
        }

        public int remaining()
        {
            return limit() - (int) Stream.Position;
        }

        public bool hasRemaining()
        {
            return remaining() > 0;
        }

        //'Mode' is only used to determine whether to return data length or capacity from the 'limit' method:
        private enum Mode
        {
            Read,
            Write
        }
    }
}