using System;
using System.IO;

namespace org.apache.utils
{
	/// <summary>
	/// Equivalent of System.IO.BinaryWriter, but with big endianness
	/// </summary>
	internal class BigEndianBinaryWriter
	{
		#region Fields not directly related to properties
		/// <summary>
		/// Buffer used for temporary storage during conversion from primitives
		/// </summary>
		private readonly byte[] buffer = new byte[16];
        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new binary writer with big endian converter, writing
        /// to the given stream.
        /// </summary>
        /// <param name="stream">Stream to write data to</param>
        internal BigEndianBinaryWriter (Stream stream)
		{
			if (stream==null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanWrite)
			{
				throw new ArgumentException("Stream isn't writable", "stream");
			}
			this.stream = stream;
		}
		#endregion

		#region Properties

	    private readonly Stream stream;

        #endregion

        #region Public methods

        /// <summary>
        /// Writes a boolean value to the stream. 1 byte is written.
        /// </summary>
        /// <param name="value">The value to write</param>
        internal void Write (bool value)
		{
			BigEndianBitConverter.CopyBytes(value, buffer, 0);
			WriteInternal(buffer, 1);
		}

        /// <summary>
        /// Writes a 32-bit signed integer to the stream, using the bit converter
        /// for this writer. 4 bytes are written.
        /// </summary>
        /// <param name="value">The value to write</param>
        internal void Write (int value)
		{
            BigEndianBitConverter.CopyBytes(value, buffer, 0);
			WriteInternal(buffer, 4);
		}

        /// <summary>
        /// Writes a 64-bit signed integer to the stream, using the bit converter
        /// for this writer. 8 bytes are written.
        /// </summary>
        /// <param name="value">The value to write</param>
        internal void Write (long value)
		{
            BigEndianBitConverter.CopyBytes(value, buffer, 0);
			WriteInternal(buffer, 8);
		}

        /// <summary>
        /// Writes an array of bytes to the stream.
        /// </summary>
        /// <param name="value">The values to write</param>
        internal void Write (byte[] value)
		{
			if (value == null)
			{
				throw (new ArgumentNullException("value"));
			}
			WriteInternal(value, value.Length);
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Writes the specified number of bytes from the start of the given byte array,
		/// after checking whether or not the writer has been disposed.
		/// </summary>
		/// <param name="bytes">The array of bytes to write from</param>
		/// <param name="length">The number of bytes to write</param>
		private void WriteInternal (byte[] bytes, int length)
		{
			stream.Write(bytes, 0, length);
		}
		#endregion
	}
}
