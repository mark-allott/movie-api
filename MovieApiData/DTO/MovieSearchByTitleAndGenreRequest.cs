namespace MovieApi.Data.DTO
{
	/// <summary>
	/// Defines a request object for searching for movies by title and genre(s)
	/// </summary>
	public class MovieSearchByTitleAndGenreRequest :
		MovieSearchByTitleRequest
	{
		/// <summary>
		/// Allows consumers to specify a list of genres to be matched with the title
		/// </summary>
		public string[] Genres { get; set; } = Array.Empty<string>();

		/// <summary>
		/// Allows consumers to specify whether the genres match any of those listed, or all of those listed
		/// </summary>
		/// <remarks>
		/// Equivalent to performing a SQL <c>IN</c> operation when <c>false</c>, or <c>AND</c> when <c>true</c>
		/// </remarks>
		public bool MatchAllGenres { get; set; } = false;

	}
}