using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace MovieApi.Data.Interfaces
{
	public interface IQueryRepository<TEntity> where TEntity : class
	{
		/// <summary>
		/// Locates the first entity matching <paramref name="filterPredicate"/>, or returns null
		/// </summary>
		/// <param name="filterPredicate">The filter clause for the query</param>
		/// <returns>The <typeparamref name="TEntity"/>, or the default</returns>
		TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> filterPredicate);

		/// <summary>
		/// Locates a single value matching <paramref name="filterPredicate"/>, or a default value. If
		/// more than one match is located, an exception shall be thrown
		/// </summary>
		/// <param name="filterPredicate"></param>
		/// <returns>The <typeparamref name="TEntity"/>, or the default</returns>
		TEntity? SingleOrDefault(Expression<Func<TEntity, bool>> filterPredicate);

		/// <summary>
		/// The entity matching the <paramref name="keys"/> search, or null
		/// </summary>
		/// <param name="keys">The key values to locate</param>
		/// <returns>The <typeparamref name="TEntity"/>, or the default</returns>
		TEntity? Find(params object[] keys);

		/// <summary>
		/// Returns an <see cref="IEnumerable{T}"/> of entities matching the <paramref name="searchPredicate"/>
		/// </summary>
		/// <typeparam name="TKey">The search key</typeparam>
		/// <param name="searchPredicate">The search filter</param>
		/// <param name="orderByPredicate">An optional order predicate</param>
		/// <param name="orderByDescending">An optional hint to ordering of the results</param>
		/// <param name="takeCount">The number of entities to take (0 denotes all available)</param>
		/// <param name="skipCount">The number of entities to skip over when returning results (0 denotes start from beginning)</param>
		/// <returns>An <see cref="IEnumerable{T}"/> of results matching the requirements</returns>
		IEnumerable<TEntity> Search<TKey>(Expression<Func<TEntity, bool>> searchPredicate,
			Expression<Func<TEntity, TKey>>? orderByPredicate = null, bool orderByDescending = false, int takeCount = 0,
			int skipCount = 0);

		/// <summary>
		/// Alternate search, does not support ordering of results
		/// </summary>
		/// <typeparam name="TKey">The search key</typeparam>
		/// <param name="searchPredicate">The search filter</param>
		/// <param name="takeCount">The number of entities to take (0 denotes all available)</param>
		/// <param name="skipCount">The number of entities to skip over when returning results (0 denotes start from beginning)</param>
		/// <returns>An <see cref="IEnumerable{T}"/> of results matching the requirements</returns>
		IEnumerable<TEntity> Search<TKey>(Expression<Func<TEntity, bool>> searchPredicate,
			int takeCount = 0, int skipCount = 0);

		/// <summary>
		/// Returns all entities, optionally ordered by <paramref name="orderByPredicate"/>  and <paramref name="orderByDescending"/>
		/// </summary>
		/// <typeparam name="TKey">The search key</typeparam>
		/// <param name="orderByPredicate">An optional order predicate</param>
		/// <param name="orderByDescending">An optional hint to ordering of the results</param>
		/// <param name="takeCount">The number of entities to take (0 denotes all available)</param>
		/// <param name="skipCount">The number of entities to skip over when returning results (0 denotes start from beginning)</param>
		/// <returns>An <see cref="IEnumerable{T}"/> of results matching the requirements</returns>
		IEnumerable<TEntity> All<TKey>(Expression<Func<TEntity, TKey>>? orderByPredicate = null, bool orderByDescending = false,
			int takeCount = 0, int skipCount = 0);

		#region async calls

		/// <summary>
		/// Locates the first entity matching <paramref name="filterPredicate"/>, or returns null
		/// </summary>
		/// <param name="filterPredicate">The filter clause for the query</param>
		/// <param name="token">A <see cref="CancellationToken"/></param>
		/// <returns>The <typeparamref name="TEntity"/>, or the default</returns>
		/// <remarks>Async supported version of <see cref="FirstOrDefault"/></remarks>
		Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filterPredicate, CancellationToken token = default);

		/// <summary>
		/// Locates the first entity matching <paramref name="filterPredicate"/>, or returns null
		/// </summary>
		/// <param name="filterPredicate">The filter clause for the query</param>
		/// <param name="token">A <see cref="CancellationToken"/></param>
		/// <returns>The <typeparamref name="TEntity"/>, or the default</returns>
		/// <remarks>Async supported version of <see cref="SingleOrDefault"/></remarks>
		Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> filterPredicate, CancellationToken token = default);

		/// <summary>
		/// The entity matching the <paramref name="keys"/> search, or null
		/// </summary>
		/// <param name="token">A <see cref="CancellationToken"/></param>
		/// <param name="keys">The key values to locate</param>
		/// <returns>The <typeparamref name="TEntity"/>, or the default</returns>
		/// <remarks>Async supported version of <see cref="Find"/></remarks>
		ValueTask<TEntity?> FindAsync(CancellationToken token = default, params object[] keys);

		/// <summary>
		/// Returns an <see cref="IEnumerable{T}"/> of entities matching the <paramref name="searchPredicate"/>
		/// </summary>
		/// <typeparam name="TKey">The search key</typeparam>
		/// <param name="searchPredicate">The search filter</param>
		/// <param name="orderByPredicate">An optional order predicate</param>
		/// <param name="orderByDescending">An optional hint to ordering of the results</param>
		/// <param name="takeCount">The number of entities to take (0 denotes all available)</param>
		/// <param name="skipCount">The number of entities to skip over when returning results (0 denotes start from beginning)</param>
		/// <param name="token">A <see cref="CancellationToken"/></param>
		/// <returns>An <see cref="IEnumerable{T}"/> of results matching the requirements</returns>
		/// <remarks>Async supported version of <see cref="Search{TKey}(System.Linq.Expressions.Expression{System.Func{TEntity,bool}},System.Linq.Expressions.Expression{System.Func{TEntity,TKey}}?,bool,int,int)"/></remarks>
		Task<IEnumerable<TEntity>> SearchAsync<TKey>(Expression<Func<TEntity, bool>> searchPredicate,
			Expression<Func<TEntity, TKey>>? orderByPredicate = null, bool orderByDescending = false, int takeCount = 0,
			int skipCount = 0, CancellationToken token = default);

		/// <summary>
		/// Alternate search, does not support ordering of results
		/// </summary>
		/// <typeparam name="TKey">The search key</typeparam>
		/// <param name="searchPredicate">The search filter</param>
		/// <param name="takeCount">The number of entities to take (0 denotes all available)</param>
		/// <param name="skipCount">The number of entities to skip over when returning results (0 denotes start from beginning)</param>
		/// <param name="token">A <see cref="CancellationToken"/></param>
		/// <returns>An <see cref="IEnumerable{T}"/> of results matching the requirements</returns>
		/// <remarks>Async supported version of <see cref="Search{TKey}(Expression{Func{TEntity, bool}},int,int"/></remarks>
		Task<IEnumerable<TEntity>> SearchAsync<TKey>(Expression<Func<TEntity, bool>> searchPredicate,
			int takeCount = 0, int skipCount = 0, CancellationToken token = default);

		/// <summary>
		/// Returns all entities, optionally ordered by <paramref name="orderByPredicate"/>  and <paramref name="orderByDescending"/>
		/// </summary>
		/// <typeparam name="TKey">The search key</typeparam>
		/// <param name="orderByPredicate">An optional order predicate</param>
		/// <param name="orderByDescending">An optional hint to ordering of the results</param>
		/// <param name="takeCount">The number of entities to take (0 denotes all available)</param>
		/// <param name="skipCount">The number of entities to skip over when returning results (0 denotes start from beginning)</param>
		/// <param name="token">A <see cref="CancellationToken"/></param>
		/// <returns>An <see cref="IEnumerable{T}"/> of results matching the requirements</returns>
		/// <remarks>Async supported version of <see cref="All{TKey}"/></remarks>
		Task<IEnumerable<TEntity>> AllAsync<TKey>(Expression<Func<TEntity, TKey>>? orderByPredicate = null,
			bool orderByDescending = false, int takeCount = 0, int skipCount = 0, CancellationToken token = default);

		#endregion async calls

		/// <summary>
		/// Provides access to the underlying <see cref="IQueryable{T}"/> so more complex queries might be performed
		/// </summary>
		IQueryable<TEntity> Queryable { get; }

		/// <summary>
		/// Provides access to the <see cref="IQueryable{T}"/>, but applies <see cref="EntityFrameworkQueryableExtensions.AsNoTracking{TEntity}"/>
		/// </summary>
		IQueryable<TEntity> UntrackedQueryable { get; }
	}
}