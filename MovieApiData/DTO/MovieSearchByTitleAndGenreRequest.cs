namespace MovieApi.Data.DTO
{
	/// <summary>
	/// Defines a request object for searching for movies by title and genre(s)
	/// </summary>
	public class MovieSearchByTitleAndGenreRequest :
		MovieSearchByTitleRequest
	{
		public string[] Genres { get; set; } = Array.Empty<string>();
	}
}