namespace MovieApi.Data.DTO
{
	/// <summary>
	/// Defines a request object for searching for movies by title, genre(s) amd actor(s)
	/// </summary>
	public class MovieSearchRequest :
		MovieSearchByTitleAndGenreRequest
	{
		/// <summary>
		/// Allows consumers to specify a list of actors to be matched with the title
		/// </summary>
		public string[] Actors { get; set; } = Array.Empty<string>();

		/// <summary>
		/// Allows consumers to specify whether the actors match any of those listed, or all of those listed
		/// </summary>
		/// <remarks>
		/// Equivalent to performing a SQL <c>IN</c> operation when <c>false</c>, or <c>AND</c> when <c>true</c>
		/// </remarks>
		public bool MatchAllActors { get; set; } = false;

	}
}