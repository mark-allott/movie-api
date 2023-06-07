using MovieApi.Data.Entities;

namespace MovieApi.Data.Interfaces
{
	/// <summary>
	/// Combination of the <see cref="ICommandRepository{TEntity}"/> and <see
	/// cref="IQueryRepository{TEntity}"/> interfaces, strongly-typed to entity <see cref="Genre"/>
	/// </summary>
	public interface IGenreRepository :
		ICommandRepository<Genre>,
		IQueryRepository<Genre>
	{
	}
}