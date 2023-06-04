using MovieApi.Data.Entities;

namespace MovieApi.Data.Interfaces
{
	public interface IMovieQueryRepository :
		IQueryRepository<Movie>
	{
	}
}