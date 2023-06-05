using Microsoft.EntityFrameworkCore;
using MovieApi.Data.Interfaces;

namespace MovieApi.Data.Repositories
{
	public abstract class AbstractCommandRepository<TContext, TEntity> :
		ICommandRepository<TEntity>
		where TContext : DbContext
		where TEntity : class
	{
		protected readonly TContext Context;
		protected DbSet<TEntity> Entities { get; }

		#region Ctor

		protected AbstractCommandRepository(TContext context)
		{
			Context = context ?? throw new ArgumentNullException(nameof(context));
			Entities = Context.Set<TEntity>();
		}

		#endregion Ctor

		#region ICommandRepository<TEntity> implementation

		/// <inheritdoc />
		public void Add(TEntity entity)
		{
			Entities.Add(entity);
		}

		/// <inheritdoc />
		public void AddRange(IEnumerable<TEntity> entities)
		{
			Entities.AddRange(entities);
		}

		/// <inheritdoc />
		public void Update(TEntity entity)
		{
			Entities.Update(entity);
		}

		/// <inheritdoc />
		public void UpdateRange(IEnumerable<TEntity> entities)
		{
			Entities.UpdateRange(entities);
		}

		/// <inheritdoc />
		public void Delete(TEntity entity)
		{
			Entities.Remove(entity);
		}

		/// <inheritdoc />
		public void DeleteRange(IEnumerable<TEntity> entities)
		{
			Entities.RemoveRange(entities);
		}

		#endregion ICommandRepository<TEntity> implementation
	}
}