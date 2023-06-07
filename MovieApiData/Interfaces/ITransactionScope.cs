namespace MovieApi.Data.Interfaces
{
	public interface ITransactionScope :
		IDisposable
	{
		/// <summary>
		/// Commit changes to the context within the scope of this transaction
		/// </summary>
		void Commit();

		/// <summary>
		/// Abandon changes to the context within the scope of this transaction
		/// </summary>
		void Rollback();

		/// <summary>
		/// Async support for <see cref="Commit"/>
		/// </summary>
		/// <param name="token"></param>
		/// <returns></returns>
		Task CommitAsync(CancellationToken token = default);

		/// <summary>
		/// Async support for <see cref="Rollback"/>
		/// </summary>
		/// <param name="token"></param>
		/// <returns></returns>
		Task RollbackAsync(CancellationToken token = default);
	}
}