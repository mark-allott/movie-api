namespace MovieApi.Data.Interfaces
{
	/// <summary>
	/// Combination of the <see cref="ICommandRepository{TEntity}"/> and <see
	/// cref="IQueryRepository{TEntity}"/> interfaces
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	public interface IRepository<TEntity> :
		ICommandRepository<TEntity>,
		IQueryRepository<TEntity>
		where TEntity : class
	{
	}
}