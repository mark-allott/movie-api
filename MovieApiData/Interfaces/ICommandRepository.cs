namespace MovieApi.Data.Interfaces
{
	public interface ICommandRepository<in TEntity> where TEntity : class
	{
		/// <summary>
		///	Adds the new <paramref name="entity"/> to the context
		/// </summary>
		/// <param name="entity"></param>
		void Add(TEntity entity);

		/// <summary>
		/// Adds the <paramref name="entities"/> to the context
		/// </summary>
		/// <param name="entities"></param>
		void AddRange(IEnumerable<TEntity> entities);

		/// <summary>
		/// Updates the <paramref name="entity"/> in the context
		/// </summary>
		/// <param name="entity"></param>
		void Update(TEntity entity);

		/// <summary>
		/// Updates the <paramref name="entities"/> in the context
		/// </summary>
		/// <param name="entities"></param>
		void UpdateRange(IEnumerable<TEntity> entities);

		/// <summary>
		/// Removes the <paramref name="entity"/> from the context
		/// </summary>
		/// <param name="entity"></param>
		void Delete(TEntity entity);

		/// <summary>
		/// Removes the <paramref name="entities"/> from the context
		/// </summary>
		/// <param name="entities"></param>
		void DeleteRange(IEnumerable<TEntity> entities);
	}
}