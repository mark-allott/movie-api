namespace MovieApi.Data.Interfaces
{
	public interface ISearchService :
		IAutoRegisterService
	{
		/// <summary>
		/// Performs a search of the movie data by title
		/// </summary>
		/// <param name="title">The title, or partial title of the movie</param>
		/// <returns></returns>
		IEnumerable<object> SearchByTitle(string title);

		/// <summary>
		/// Performs a search of the movie data by genre(s)
		/// </summary>
		/// <param name="genres">One or more genre names to search by</param>
		/// <returns></returns>
		IEnumerable<object> SearchByGenre(params string[] genres);

		/// <summary>
		/// Performs a search of the movie data by specific genre IDs
		/// </summary>
		/// <param name="genreIds">The IDs for the genres to search for</param>
		/// <returns></returns>
		IEnumerable<object> SearchByGenre(params int[] genreIds);
	}
}