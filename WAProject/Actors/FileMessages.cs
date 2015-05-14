using System;

namespace WAProject
{
	class FileMessages
	{
		/// <summary>
		/// Message class to request to read a file
		/// </summary>
		public class ReadFile
		{
			public string FileName { get; private set; }

			public ReadFile(string fileName) {
				FileName = fileName;
			}
		}

		/// <summary>
		/// Message class to exchange the read row from the file
		/// </summary>
		public class RowFile
		{
			public string[] Row { get; private set; }
			public string FileName { get; private set; }

			public RowFile(string[] row, string fileName) 
			{
				Row = row;
				FileName = fileName;
			}
		}

		/// <summary>
		/// First row.
		/// </summary>
		public class FirstRow : RowFile {}

		public class StoreType0 : RowFile {}

		public class StoreType1 : RowFile {}

		/// <summary>
		/// End of file.
		/// </summary>
		public class EndOfFile
		{
			public string FileName { get; private set; }
			public long rowProcessed { get; private set; }

			public EndOfFile(string fileName, long rows) {
				FileName = fileName;
				rowProcessed = rows;
			}
		}
	}
}

