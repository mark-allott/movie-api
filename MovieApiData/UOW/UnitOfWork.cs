using Microsoft.EntityFrameworkCore;
using MovieApi.Data.Interfaces;

namespace MovieApi.Data.UOW
{
	public class UnitOfWork<TContext> :
		IUnitOfWork<TContext>
		where TContext : DbContext
	{
		private readonly TContext _context;

		#region Ctor

		public UnitOfWork(TContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}

		#endregion Ctor

		#region IUnitOfWork<TContext> implementation

		/// <inheritdoc />
		public int SaveChanges()
		{
			return _context.SaveChanges();
		}

		/// <inheritdoc />
		public Task<int> SaveChangesAsync(CancellationToken token = default)
		{
			return _context.SaveChangesAsync(token);
		}

		/// <inheritdoc />
		public ITransactionScope BeginTransaction()
		{
			return new TransactionScope(_context.Database.BeginTransaction());
		}

		/// <inheritdoc />
		public async Task<ITransactionScope> BeginTransactionAsync(CancellationToken token = default)
		{
			return new TransactionScope(await _context.Database.BeginTransactionAsync(token));
		}

		#endregion IUnitOfWork<TContext> implementation
	}
}