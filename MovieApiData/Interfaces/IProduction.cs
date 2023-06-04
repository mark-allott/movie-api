namespace MovieApi.Data.Interfaces
{
	public interface IProduction
	{
		/// <summary>
		/// Defines the title of the production
		/// </summary>
		string Title { get; set; }

		/// <summary>
		/// Allows a description / overview of the title to be displayed
		/// </summary>
		string Overview { get; set; }

		/// <summary>
		/// The date the title was generally released
		/// </summary>
		DateTime ReleaseDate { get; set; }

		/// <summary>
		/// The original language the film was released in
		/// </summary>
		string OriginalLanguage { get; set; }
	}
}