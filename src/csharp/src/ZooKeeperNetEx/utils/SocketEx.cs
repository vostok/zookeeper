using System.Net.Sockets;

namespace org.apache.utils
{
    internal static class SocketEx
    {
        public static void read(this Socket socket, ByteBuffer byteBuffer)
        {
            int size = byteBuffer.remaining();
            byte[] data = new byte[size];
            int index = 0;
            while (index < size)
            {
                SocketError socketErrorCode;
                int read = socket.Receive(data, index, size - index, SocketFlags.None, out socketErrorCode);
                if (socketErrorCode != SocketError.WouldBlock && socketErrorCode != SocketError.Success)
                {
                    throw new SocketException((int)socketErrorCode);
                }
                if (read == 0) break;
                index += read;
            }
            byteBuffer.Stream.Write(data, 0, index);
        }

        public static void write(this Socket socket, ByteBuffer byteBuffer)
        {
            byte[] data = byteBuffer.Stream.ToArray();
            int bytesToWrite = byteBuffer.remaining();
            int bytesWritten = (int)byteBuffer.Stream.Position;
            do
            {
                SocketError socketErrorCode;
                int actuallyWritten = socket.Send(data, bytesWritten, bytesToWrite, SocketFlags.None, out socketErrorCode);
                if (socketErrorCode != SocketError.WouldBlock && socketErrorCode != SocketError.Success)
                {
                    throw new SocketException((int)socketErrorCode);
                }
                if (actuallyWritten == 0) break;
                byteBuffer.Stream.Position += actuallyWritten;
                bytesWritten += actuallyWritten;
                bytesToWrite -= actuallyWritten;

            } while (bytesToWrite > 0);
        }
    }
}
