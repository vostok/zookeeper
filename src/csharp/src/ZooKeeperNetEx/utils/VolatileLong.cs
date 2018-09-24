
using System.Threading;

namespace ZooKeeperNetEx.utils
{
    internal class VolatileLong
    {
        public VolatileLong(long value)
        {
            Value = value;
        }

        private long m_Value;

        public long Value
        {
            get => Volatile.Read(ref m_Value);
            set => Volatile.Write(ref m_Value, value);
        }
    }
}
