namespace MovieApi.Data.Interfaces
{
	public interface IDataImporter
	{
		/// <summary>
		/// Reads data from the specified <paramref name="stream"/>
		/// </summary>
		/// <param name="stream">An open stream object, reading for reading</param>
		/// <returns>A true/false flag indicating whether the import ran successfully</returns>
		bool ImportFromStream(Stream stream);
	}
}