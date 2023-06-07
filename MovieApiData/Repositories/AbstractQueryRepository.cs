using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data.Helpers;
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

			return query.SkipAndTake(skipCount, takeCount)
				.ToList();
		}

		/// <inheritdoc />

		public IEnumerable<TEntity> Search<TKey>(Expression<Func<TEntity, bool>> searchPredicate, int takeCount = 0, int skipCount = 0)
		{
			var query = Queryable.Where(searchPredicate);

			return query.SkipAndTake(skipCount, takeCount)
				.ToList();
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
			return query.SkipAndTake(skipCount, takeCount)
				.ToList();
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

			return await query.SkipAndTake(skipCount, takeCount)
				.ToListAsync(token);
		}

		/// <inheritdoc />
		public async Task<IEnumerable<TEntity>> SearchAsync<TKey>(Expression<Func<TEntity, bool>> searchPredicate, int takeCount = 0, int skipCount = 0,
			CancellationToken token = default)
		{
			var query = Queryable.Where(searchPredicate);

			return await query.SkipAndTake(skipCount, takeCount)
				.ToListAsync(token);
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

			return await query.SkipAndTake(skipCount, takeCount)
				.ToListAsync(token);
		}

		/// <inheritdoc />
		public IQueryable<TEntity> Queryable
		{
			get
			{
				return Entities.AsQueryable();
			}
		}

		/// <inheritdoc />
		public IQueryable<TEntity> UntrackedQueryable
		{
			get
			{
				return Queryable.AsNoTracking();
			}
		}

		#endregion IQueryRepository<TEntity> implementation
	}
}