using MovieApi.Data.Entities;

namespace MovieApi.Data.Interfaces
{
	/// <summary>
	/// Strongly-typed version of <see cref="ICommandRepository{TEntity}"/> to entity type <see cref="Actor"/>
	/// </summary>
	public interface IActorCommandRepository :
		ICommandRepository<Actor>
	{
	}
}