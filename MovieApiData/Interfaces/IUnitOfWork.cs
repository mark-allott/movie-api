using Microsoft.EntityFrameworkCore;

namespace MovieApi.Data.Interfaces
{
	public interface IUnitOfWork<TContext> where TContext : DbContext
	{
		/// <summary>
		/// Commits changes to the context to the DB
		/// </summary>
		/// <returns></returns>
		int SaveChanges();

		/// <summary>
		/// Async version of <see cref="SaveChanges"/>
		/// </summary>
		/// <param name="token"></param>
		/// <returns></returns>
		Task<int> SaveChangesAsync(CancellationToken token = default);

		#region Transaction support

		/// <summary>
		/// Starts a transaction scope
		/// </summary>
		/// <returns>The scoped transaction</returns>
		/// <remarks>Any commits/rollbacks are up to the consumer of the scoped transaction!</remarks>
		ITransactionScope BeginTransaction();

		/// <summary>
		/// Async version of <see cref="BeginTransaction"/>
		/// </summary>
		/// <param name="token"></param>
		/// <returns></returns>
		Task<ITransactionScope> BeginTransactionAsync(CancellationToken token = default);

		#endregion Transaction support
	}
}