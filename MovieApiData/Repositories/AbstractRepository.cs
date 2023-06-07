using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data.Helpers;
using MovieApi.Data.Interfaces;

namespace MovieApi.Data.Repositories
{
	public abstract class AbstractRepository<TContext, TEntity> :
		AbstractCommandRepository<TContext, TEntity>,
		IRepository<TEntity>
		where TContext : DbContext
		where TEntity : class
	{
		protected AbstractRepository(TContext context) :
			base(context)
		{
		}

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
		public IEnumerable<TEntity> Search<TKey>(Expression<Func<TEntity, bool>> searchPredicate,
			Expression<Func<TEntity, TKey>>? orderByPredicate = null, bool orderByDescending = false, int takeCount = 0,
			int skipCount = 0)
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

		public IEnumerable<TEntity> Search<TKey>(Expression<Func<TEntity, bool>> searchPredicate, int takeCount = 0,
			int skipCount = 0)
		{
			var query = Queryable.Where(searchPredicate);

			return query.SkipAndTake(skipCount, takeCount)
				.ToList();
		}

		/// <inheritdoc />
		public IEnumerable<TEntity> All<TKey>(Expression<Func<TEntity, TKey>>? orderByPredicate = null,
			bool orderByDescending = false, int takeCount = 0, int skipCount = 0)
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
		public async Task<IEnumerable<TEntity>> SearchAsync<TKey>(Expression<Func<TEntity, bool>> searchPredicate,
			Expression<Func<TEntity, TKey>>? orderByPredicate = null, bool orderByDescending = false, int takeCount = 0,
			int skipCount = 0, CancellationToken token = default)
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
		public async Task<IEnumerable<TEntity>> SearchAsync<TKey>(Expression<Func<TEntity, bool>> searchPredicate,
			int takeCount = 0, int skipCount = 0, CancellationToken token = default)
		{
			var query = Queryable.Where(searchPredicate);

			return await query.SkipAndTake(skipCount, takeCount)
				.ToListAsync(token);
		}

		/// <inheritdoc />
		public async Task<IEnumerable<TEntity>> AllAsync<TKey>(Expression<Func<TEntity, TKey>>? orderByPredicate = null,
			bool orderByDescending = false, int takeCount = 0, int skipCount = 0, CancellationToken token = default)
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