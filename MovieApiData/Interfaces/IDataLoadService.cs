using MovieApi.Data.DTO;

namespace MovieApi.Data.Interfaces
{
	/// <summary>
	/// Provides abstract loading mechanisms for the data held in the movie dataset
	/// </summary>
	public interface IDataLoadService :
		IAutoRegisterService
	{
		/// <summary>
		/// Loads a list of genres into the the dataset
		/// </summary>
		/// <param name="genres">The list of genres to be added</param>
		/// <returns>A flag indicating success or failure</returns>
		bool LoadGenres(List<GenreDto> genres);

		bool LoadMovies(List<MovieDto> movies);
	}
}