using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Logging;
using MovieApi.Data.DTO;
using MovieApi.Data.Interfaces;

namespace MovieApi.Data.Providers
{
	public class KaggleDataImporter :
		IDataImporter
	{
		private readonly ILogger<KaggleDataImporter> _logger;
		public List<KaggleData> Data { get; private set; }
		public string[]? Header { get; private set; }

		#region Ctor

		public KaggleDataImporter(ILogger<KaggleDataImporter> logger)
		{
			_logger = logger;
			Data = new List<KaggleData>();
			Header = new string[] { };
		}

		#endregion Ctor

		#region IDataImporter implementation

		/// <inheritdoc />
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="IOException"></exception>
		public bool ImportFromStream(Stream stream)
		{
			_ = stream ?? throw new ArgumentNullException(nameof(stream));
			if (!stream.CanRead)
				throw new IOException($"{nameof(stream)} parameter does not support reading");

			try
			{
				using (var sr = new StreamReader(stream))
				{
					//	If stream supports seeking, always rewind position
					if (sr.BaseStream.CanSeek)
						sr.BaseStream.Position = 0;

					using (var csv = new CsvReader(sr, GetConfiguration()))
					{
						//	Check file is readable and has headers - no headers, no data import!
						if (!csv.Read() || !csv.ReadHeader())
							return false;

						csv.Context.RegisterClassMap(CreateTypedClassMap(GetHeaders(csv)));
						Data = csv.GetRecords<KaggleData>().ToList();
						Header = csv.HeaderRecord;
					}
				}
			}
			catch (Exception e)
			{
				_logger.LogError(e, $"Error during data import");
				return false;
			}

			return Data.Any();
		}

		#endregion IDataImporter implementation

		#region internal methods

		/// <summary>
		/// Sets up the configuration of the CSV reader
		/// </summary>
		/// <returns>The required config object</returns>
		private CsvConfiguration GetConfiguration()
		{
			//	TODO - add extra config options if needed
			return new CsvConfiguration(CultureInfo.InvariantCulture)
			{
				LineBreakInQuotedFieldIsBadData = true,
				MissingFieldFound = null,
				BadDataFound = null
			};
		}

		/// <summary>
		/// Extracts the headers / column names from the file
		/// </summary>
		/// <param name="csv">The <see cref="CsvReader"/> being used</param>
		/// <returns>A list of the column header names, or an empty list</returns>
		private List<string> GetHeaders(CsvReader csv)
		{
			return csv.HeaderRecord?.ToList() ?? new List<string>();
		}

		/// <summary>
		/// Instantiates a <see cref="KaggleDataImportClassMap"/>
		/// </summary>
		/// <param name="list">A list of the column header names found</param>
		/// <returns>The <see cref="ClassMap"/> definition</returns>
		private ClassMap CreateTypedClassMap(List<string> list)
		{
			return new KaggleDataImportClassMap(list);
		}

		#endregion internal methods
	}
}