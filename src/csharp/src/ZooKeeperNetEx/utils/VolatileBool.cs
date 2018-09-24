
using System.Threading;

namespace ZooKeeperNetEx.utils
{
    internal class VolatileBool
    {
        public VolatileBool(bool value)
        {
            Value = value;
        }

        private bool m_Value;

        public bool Value
        {
            get => Volatile.Read(ref m_Value);
            set => Volatile.Write(ref m_Value, value);
        }
    }
}
