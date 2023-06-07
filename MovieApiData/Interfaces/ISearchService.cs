using MovieApi.Data.DTO;

namespace MovieApi.Data.Interfaces
{
	public interface ISearchService :
		IAutoRegisterService
	{
		/// <summary>
		/// Performs a search of the movie data by title
		/// </summary>
		/// <param name="title">The title, or partial title of the movie</param>
		/// <param name="pageNumber">Allows skipping pages of information in the returned results</param>
		/// <param name="pageSize">Allows defining a size of the page of results</param>
		/// <param name="useSqlLike"></param>
		/// <returns>
		/// A <see cref="MovieSearchResultCollection"/> object containing the results of the search
		/// </returns>
		/// <remarks>
		/// A <paramref name="pageSize"/> value of 0 (default) is an indication to return all possible results
		/// </remarks>
		MovieSearchResultCollection SearchByTitle(string title, int pageNumber = 1, int pageSize = 0, bool useSqlLike = false);

		/// <summary>
		/// Provides a method by which to return all possible Genres in the dataset
		/// </summary>
		/// <returns>A <see cref="GenreCollectionResult"/> containing the list of genres</returns>
		GenreCollectionResult GetGenres();

		/// <summary>
		/// Performs a search of the movie data by genre(s)
		/// </summary>
		/// <param name="pageNumber"></param>
		/// <param name="pageSize"></param>
		/// <param name="genres">One or more genre names to search by</param>
		/// <returns></returns>
		MovieSearchResultCollection SearchByGenre(int pageNumber = 1, int pageSize = 0, params string[] genres);

		/// <summary>
		/// Performs a search of the movie data by specific genre IDs
		/// </summary>
		/// <param name="genreIds">The IDs for the genres to search for</param>
		/// <returns></returns>
		IEnumerable<object> SearchByGenre(params int[] genreIds);
	}
}