
using System.Threading;

namespace ZooKeeperNetEx.utils
{
    internal class VolatileInt
    {
        public VolatileInt(int value)
        {
            Value = value;
        }

        private int m_Value;

        public int Value
        {
            get => Volatile.Read(ref m_Value);
            set => Volatile.Write(ref m_Value, value);
        }
    }
}
