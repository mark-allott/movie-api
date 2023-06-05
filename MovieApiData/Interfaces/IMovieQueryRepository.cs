using MovieApi.Data.Entities;

namespace MovieApi.Data.Interfaces
{
	/// <summary>
	/// Strongly-typed version of <see cref="IQueryRepository{TEntity}"/> to entity type <see cref="Movie"/>
	/// </summary>
	public interface IMovieQueryRepository :
		IQueryRepository<Movie>
	{
	}
}