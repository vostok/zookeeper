namespace org.apache.zookeeper
{

	/// <summary>
	/// a class that represents the stats associated with quotas
	/// </summary>
	public sealed class StatsTrack
	{
	    private const string countStr = "count";
	    private const string byteStr = "bytes";

	    /// <summary>
		/// a default constructor for
		/// stats
		/// </summary>
		public StatsTrack() : this(null)
		{
		}
		/// <summary>
		/// the stat string should be of the form count=int,bytes=long
		/// if stats is called with null the count and bytes are initialized
		/// to -1. </summary>
		/// <param name="stats"> the stat string to be initialized with </param>
		public StatsTrack(string stats)
		{
			if (stats == null)
			{
				stats = "count=-1,bytes=-1";
			}
			string[] split = stats.Split(',');
			if (split.Length != 2)
			{
				throw new System.ArgumentException("invalid string " + stats);
			}
		    Count = int.Parse(split[0].Split('=')[1]);
		    Bytes = long.Parse(split[1].Split('=')[1]);
		}


	    /// <summary>
	    /// get the count of nodes allowed as part of quota
	    /// </summary>
	    /// <returns> the count as part of this string </returns>
	    public readonly int Count;


	    /// <summary>
	    /// get the count of bytes allowed as part of quota
	    /// </summary>
	    /// <returns> the bytes as part of this string </returns>
	    public readonly long Bytes;


        /// <summary>
        /// returns the string that maps to this stat tracking.
        /// </summary>
        public override string ToString()
		{
			return countStr + "=" + Count + "," + byteStr + "=" + Bytes;
		}
	}

}