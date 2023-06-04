using System.Linq.Expressions;

namespace MovieApi.Data.Interfaces
{
	public interface IQueryRepository<TEntity> where TEntity : class
	{
		TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filterPredicate);

		TEntity SingleOrDefault(Expression<Func<TEntity, bool>> filterPredicate);

		TEntity Find(params object[] keys);

		IEnumerable<TEntity> Search<TKey>(Expression<Func<TEntity, bool>> searchPredicate,
			Expression<Func<TEntity, TKey>>? orderByPredicate = null, bool orderByDescending = false, long takeCount = 0,
			long skipCount = 0);

		IEnumerable<TEntity> Search<TKey>(Expression<Func<TEntity, bool>> searchPredicate,
			long takeCount = 0, long skipCount = 0);

		IEnumerable<TEntity> All<TKey>(Expression<Func<TEntity, TKey>>? orderByPredicate = null, bool orderByDescending = false,
			long takeCount = 0, long skipCount = 0);

		#region async calls

		Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filterPredicate, CancellationToken token = default);

		Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> filterPredicate, CancellationToken token = default);

		Task<TEntity> FindAsync(CancellationToken token = default, params object[] keys);

		Task<IEnumerable<TEntity>> SearchAsync<TKey>(Expression<Func<TEntity, bool>> searchPredicate,
			Expression<Func<TEntity, TKey>>? orderByPredicate = null, bool orderByDescending = false, long takeCount = 0,
			long skipCount = 0, CancellationToken token = default);

		Task<IEnumerable<TEntity>> SearchAsync<TKey>(Expression<Func<TEntity, bool>> searchPredicate,
			long takeCount = 0, long skipCount = 0, CancellationToken token = default);

		Task<IEnumerable<TEntity>> AllAsync<TKey>(Expression<Func<TEntity, TKey>>? orderByPredicate = null,
			bool orderByDescending = false, long takeCount = 0, long skipCount = 0, CancellationToken token = default);

		#endregion async calls

		IQueryable<TEntity> Queryable { get; }
	}
}