using System;

namespace org.apache.zookeeper.common
{
	/// <summary>
	/// Path related utilities
	/// </summary>
	public static class PathUtils
	{

		/// <summary>
		/// validate the provided znode path string </summary>
		/// <param name="path"> znode path string </param>
		/// <param name="isSequential"> if the path is being created
		/// with a sequential flag </param>
        /// <exception cref="ArgumentException"> if the path is invalid </exception>
		public static void validatePath(string path, bool isSequential)
		{
			validatePath(isSequential? path + "1": path);
		}

        /// <summary>
        /// Validate the provided znode path string </summary>
        /// <param name="path"> znode path string </param>
        /// <exception cref="ArgumentException"> if the path is invalid </exception>
        public static void validatePath(string path)
		{
			if (path == null) 
            {
			    throw new ArgumentException("Path cannot be null", "path");
			}
			if (path.Length == 0)
			{
                throw new ArgumentException("Path length must be > 0", "path");
			}
			if (path[0] != '/')
			{
                throw new ArgumentException("Path must start with / character", "path");
			}
			if (path.Length == 1)
			{ // done checking - it's the root
				return;
			}
			if (path[path.Length - 1] == '/')
			{
                throw new ArgumentException("Path must not end with / character", "path");
			}

			string reason = null;
			char lastc = '/';
			char[] chars = path.ToCharArray();
			char c;
			for (int i = 1; i < chars.Length; lastc = chars[i], i++)
			{
				c = chars[i];

				if (c == 0)
				{
					reason = "null character not allowed @" + i;
					break;
				}
			    if (c == '/' && lastc == '/')
			    {
			        reason = "empty node name specified @" + i;
			        break;
			    }
			    if (c == '.' && lastc == '.')
			    {
			        if (chars[i - 2] == '/' && ((i + 1 == chars.Length) || chars[i + 1] == '/'))
			        {
			            reason = "relative paths not allowed @" + i;
			            break;
			        }
			    }
			    else if (c == '.')
			    {
			        if (chars[i - 1] == '/' && ((i + 1 == chars.Length) || chars[i + 1] == '/'))
			        {
			            reason = "relative paths not allowed @" + i;
			            break;
			        }
			    }
			    else if (c > '\u0000' && c < '\u001f' || c > '\u007f' && c < '\u009F' || c > '\ud800' && c < '\uf8ff' || c > '\ufff0' && c < '\uffff')
			    {
			        reason = "invalid charater @" + i;
			        break;
			    }
			}

			if (reason != null)
			{
                throw new ArgumentException("Invalid path string \"" + path + "\" caused by " + reason, "path");
			}
		}
	}

}