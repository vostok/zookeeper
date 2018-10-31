
using System;
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

        public bool TrySetValue(int preconditionValue, int newValue)
        {
            return Interlocked.CompareExchange(ref m_Value, newValue, preconditionValue) == preconditionValue;
        }

        public void SetValue(int preconditionValue, int newValue)
        {
            int actualValue = Interlocked.CompareExchange(ref m_Value, newValue, preconditionValue);
            if (actualValue != preconditionValue)
                throw new InvalidOperationException("Expected=" + preconditionValue + "Actual=" + actualValue);
        }

        public int Increment() {
            return Interlocked.Increment(ref m_Value);
        }

        public int Decrement()
        {
            return Interlocked.Decrement(ref m_Value);
        }
    }
}
