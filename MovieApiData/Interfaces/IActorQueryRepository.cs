using MovieApi.Data.Entities;

namespace MovieApi.Data.Interfaces
{
	/// <summary>
	/// Strongly-typed version of <see cref="IQueryRepository{TEntity}"/> to entity type <see cref="Actor"/>
	/// </summary>
	public interface IActorQueryRepository :
		IQueryRepository<Actor>
	{
	}
}