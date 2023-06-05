using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data.Interfaces;

namespace MovieApi.Data.Repositories
{
	/// <summary>
	/// Concrete abstract class for querying a repository
	/// </summary>
	/// <typeparam name="TContext">The data context</typeparam>
	/// <typeparam name="TEntity">An entity within the context</typeparam>
	/// <remarks>This is effectively a read-only repo</remarks>
	public abstract class AbstractQueryRepository<TContext, TEntity> :
		IQueryRepository<TEntity>
		where TContext : DbContext
		where TEntity : class
	{
		protected readonly TContext Context;

		protected DbSet<TEntity> Entities { get; }

		#region Ctor

		protected AbstractQueryRepository(TContext context)
		{
			Context = context ?? throw new ArgumentNullException(nameof(context));
			Entities = Context.Set<TEntity>();
		}

		#endregion Ctor

		#region IQueryRepository<TEntity> implementation

		/// <inheritdoc />
		public TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> filterPredicate)
		{
			return Queryable.FirstOrDefault(filterPredicate);
		}

		/// <inheritdoc />
		public TEntity? SingleOrDefault(Expression<Func<TEntity, bool>> filterPredicate)
		{
			return Queryable.SingleOrDefault(filterPredicate);
		}

		/// <inheritdoc />
		public TEntity? Find(params object[] keys)
		{
			return Entities.Find(keys);
		}

		/// <summary>
		/// Common method used to skip / take from the <paramref name="query"/> results, returning the
		/// required entities
		/// </summary>
		/// <param name="query">The <see cref="IQueryable{T}"/> being scanned</param>
		/// <param name="skipCount">
		/// The number of entities to skip in order to return the required results
		/// </param>
		/// <param name="takeCount">
		/// The number of entities to return in total, after any <paramref name="skipCount"/> are processed
		/// </param>
		/// <returns>A filtered version of the <paramref name="query"/></returns>
		/// <remarks>
		/// If <paramref name="skipCount"/> is zero, or negative, no results are skipped. Similarly, if
		/// <paramref name="takeCount"/> is zero, or negative, then all remaining results are returned
		/// </remarks>
		protected IQueryable<TEntity> SkipAndTake(IQueryable<TEntity> query, int skipCount, int takeCount)
		{
			if (skipCount > 0)
				query = query.Skip(skipCount);

			if (takeCount > 0)
				query = query.Take(takeCount);

			return query;
		}

		/// <inheritdoc />
		public IEnumerable<TEntity> Search<TKey>(Expression<Func<TEntity, bool>> searchPredicate, Expression<Func<TEntity, TKey>>? orderByPredicate = null,
			bool orderByDescending = false, int takeCount = 0, int skipCount = 0)
		{
			var query = Queryable.Where(searchPredicate);

			if (orderByPredicate is not null)
			{
				query = orderByDescending
					? query.OrderByDescending(orderByPredicate)
					: query.OrderBy(orderByPredicate);
			}

			return SkipAndTake(query, skipCount, takeCount).ToList();
		}

		/// <inheritdoc />

		public IEnumerable<TEntity> Search<TKey>(Expression<Func<TEntity, bool>> searchPredicate, int takeCount = 0, int skipCount = 0)
		{
			var query = Queryable.Where(searchPredicate);

			return SkipAndTake(query, skipCount, takeCount).ToList();
		}

		/// <inheritdoc />
		public IEnumerable<TEntity> All<TKey>(Expression<Func<TEntity, TKey>>? orderByPredicate = null, bool orderByDescending = false, int takeCount = 0,
			int skipCount = 0)
		{
			var query = Queryable;

			if (orderByPredicate is not null)
			{
				query = orderByDescending
					? query.OrderByDescending(orderByPredicate)
					: query.OrderBy(orderByPredicate);
			}
			return SkipAndTake(query, skipCount, takeCount).ToList();
		}

		/// <inheritdoc />
		public Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filterPredicate, CancellationToken token = default)
		{
			return Queryable.FirstOrDefaultAsync(filterPredicate, token);
		}

		/// <inheritdoc />
		public Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> filterPredicate, CancellationToken token = default)
		{
			return Queryable.SingleOrDefaultAsync(filterPredicate, token);
		}

		/// <inheritdoc />
		public ValueTask<TEntity?> FindAsync(CancellationToken token = default, params object[] keys)
		{
			return Entities.FindAsync(token, keys);
		}

		/// <inheritdoc />
		public async Task<IEnumerable<TEntity>> SearchAsync<TKey>(Expression<Func<TEntity, bool>> searchPredicate, Expression<Func<TEntity, TKey>>? orderByPredicate = null, bool orderByDescending = false,
			int takeCount = 0, int skipCount = 0, CancellationToken token = default)
		{
			var query = Queryable.Where(searchPredicate);

			if (orderByPredicate is not null)
			{
				query = orderByDescending
					? query.OrderByDescending(orderByPredicate)
					: query.OrderBy(orderByPredicate);
			}

			return await SkipAndTake(query, skipCount, takeCount).ToListAsync(token);
		}

		/// <inheritdoc />
		public async Task<IEnumerable<TEntity>> SearchAsync<TKey>(Expression<Func<TEntity, bool>> searchPredicate, int takeCount = 0, int skipCount = 0,
			CancellationToken token = default)
		{
			var query = Queryable.Where(searchPredicate);

			return await SkipAndTake(query, skipCount, takeCount).ToListAsync(token);
		}

		/// <inheritdoc />
		public async Task<IEnumerable<TEntity>> AllAsync<TKey>(Expression<Func<TEntity, TKey>>? orderByPredicate = null, bool orderByDescending = false, int takeCount = 0,
			int skipCount = 0, CancellationToken token = default)
		{
			var query = Queryable;

			if (orderByPredicate is not null)
			{
				query = orderByDescending
					? query.OrderByDescending(orderByPredicate)
					: query.OrderBy(orderByPredicate);
			}

			return await SkipAndTake(query, skipCount, takeCount).ToListAsync(token);
		}

		/// <inheritdoc />
		public IQueryable<TEntity> Queryable
		{
			get
			{
				return Entities.AsQueryable();
			}
		}

		#endregion IQueryRepository<TEntity> implementation
	}
}