using MovieApi.Data.Entities;

namespace MovieApi.Data.Interfaces
{
	/// <summary>
	/// Combination of the <see cref="ICommandRepository{TEntity}"/> and <see
	/// cref="IQueryRepository{TEntity}"/> interfaces, strongly-typed for <see cref="Movie"/>
	/// </summary>
	public interface IMovieRepository :
		ICommandRepository<Movie>,
		IQueryRepository<Movie>
	{
	}
}