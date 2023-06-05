using MovieApi.Data.Entities;

namespace MovieApi.Data.Interfaces
{
	/// <summary>
	/// Strongly-typed version of <see cref="ICommandRepository{TEntity}"/> to entity type <see cref="Movie"/>
	/// </summary>
	public interface IMovieCommandRepository :
		ICommandRepository<Movie>
	{
	}
}