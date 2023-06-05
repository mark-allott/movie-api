using MovieApi.Data.Entities;

namespace MovieApi.Data.Interfaces
{
	/// <summary>
	/// Strongly-typed version of <see cref="ICommandRepository{TEntity}"/> to entity type <see cref="Genre"/>
	/// </summary>
	public interface IGenreCommandRepository :
		ICommandRepository<Genre>
	{
	}
}