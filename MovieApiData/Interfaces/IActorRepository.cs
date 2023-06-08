using MovieApi.Data.Entities;

namespace MovieApi.Data.Interfaces
{
	/// <summary>
	/// Combination of the <see cref="ICommandRepository{TEntity}"/> and <see
	/// cref="IQueryRepository{TEntity}"/> interfaces, strongly-typed to entity <see cref="Actor"/>
	/// </summary>
	public interface IActorRepository :
		ICommandRepository<Actor>,
		IQueryRepository<Actor>
	{
	}
}