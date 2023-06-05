using Microsoft.EntityFrameworkCore.Storage;
using MovieApi.Data.Interfaces;

namespace MovieApi.Data.UOW
{
	internal sealed class TransactionScope :
		ITransactionScope
	{
		private readonly IDbContextTransaction _dbContextTransaction;

		#region Ctor

		internal TransactionScope(IDbContextTransaction dbContextTransaction)
		{
			_dbContextTransaction = dbContextTransaction ?? throw new ArgumentNullException(nameof(dbContextTransaction));
		}

		#endregion Ctor

		#region ITransactionScope implementation

		#region IDisposable implementation

		/// <inheritdoc />
		public void Dispose()
		{
			_dbContextTransaction.Dispose();
		}

		#endregion IDisposable implementation

		/// <inheritdoc />
		public void Commit()
		{
			_dbContextTransaction.Commit();
		}

		/// <inheritdoc />
		public void Rollback()
		{
			_dbContextTransaction.Rollback();
		}

		/// <inheritdoc />
		public Task CommitAsync(CancellationToken token = default)
		{
			return _dbContextTransaction.CommitAsync(token);
		}

		/// <inheritdoc />
		public Task RollbackAsync(CancellationToken token = default)
		{
			return _dbContextTransaction.RollbackAsync(token);
		}

		#endregion ITransactionScope implementation
	}
}