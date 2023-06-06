using MovieApi.Data.DTO;

namespace MovieApi.Data.Interfaces
{
	public interface IDataProvider
	{
		/// <summary>
		/// Provides a list of the Genres listed in the data
		/// </summary>
		/// <returns>The list of unique Genres</returns>
		List<GenreDto> GetGenreList();

		/// <summary>
		/// Provides a list of the Movies listed in the data
		/// </summary>
		/// <returns>The list of movies</returns>
		List<MovieDto> GetMovieList();
	}
}