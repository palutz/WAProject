using System;
using System.Collections.Generic;

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
		public class FirstRow : RowFile {
			public FirstRow(string[] row, string fileName) : base(row, fileName) {}
		}

		public class StoreType0 : RowFile {
			public StoreType0(string[] row, string fileName) : base(row, fileName) {}
		}

		public class StoreType1 : RowFile {
			public StoreType1(string[] row, string fileName) : base(row, fileName) {}
		}

		public class ResultMapType1 {
			public List<int[]> MapResult { get; private set; }

			public ResultMapType1(List<int[]> mapped)
			{
				MapResult = mapped;
			}
		}

		public class ResultMapType0 :ResultMapType1 {
			public ResultMapType0(List<int[]> mapped) : base(mapped) {}
		}

		public class MapProcessEnded {
			public List<int[]> MapResultType0 { get; private set; }
			public List<int[]> MapResultType1 { get; private set; }
			public int[] MaxValues { get; private set; }
			public int[] MinValues { get; private set; }

			public MapProcessEnded(List<int[]> mapType0, List<int[]> mapType1, int[] minValues, int[] maxValues)
			{
				MapResultType0 = mapType0;
				MapResultType1 = mapType1;
				MaxValues = maxValues;
				MinValues = minValues;
			}
		}

		public class ReduceType0 {
			public List<int[]> MapType0 { get; private set; }
			public int[] MaxValues { get; private set; }
			public int[] MinValues { get; private set; }

			public ReduceType0(List<int[]> mapType0, int[] minValues, int[] maxValues)
			{
				MapType0 = mapType0;
				MaxValues = maxValues;
				MinValues = minValues;
			}
		}

		public class ReduceType1 {
			public List<int[]> MapType1 { get; private set; }

			public ReduceType1(List<int[]> mapType1)
			{
				MapType1 = mapType1;
			}
		}

		public class ReduceStore {
			public int[] storeValue { get; private set; }

			public ReduceStore(int[] values)
			{
				storeValue = values;
			}
		}

		public class ReduceType0End {
			public string FileName { get; private set; }

			public ReduceType0End(string fileName) 
			{
				FileName = fileName;
			}
		}

		public class ReduceType1End : ReduceType0End {

			public ReduceType1End(string fileName) : base(fileName) 
			{
			}
		}

		public class CheckReduceEnded { }

		public class ReduceResult { 
			public List<string> ReduceRes { get; private set; }

			public ReduceResult(List<String> res)
			{
				ReduceRes = res;
			}
		}

		public class AskMaxMinValue {
			public string FileName { get; private set; }

			public AskMaxMinValue(string fileName) 
			{
				FileName = fileName;
			}
		}

		public class ResultMaxMinValue {
			public int[] MaxValue { get; private set; }
			public int[] MinValue { get; private set; }

			public ResultMaxMinValue(int[] maxValues, int[] minValues)
			{
				MaxValue = maxValues;
				MinValue = minValues;
			}
		}

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

