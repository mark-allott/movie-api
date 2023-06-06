namespace MovieApi.Data.Helpers
{
	public static class QueryHelpers
	{
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
		public static IQueryable<T> SkipAndTake<T>(this IQueryable<T> query, int skipCount = 0, int takeCount = 0)
		{
			if (skipCount > 0)
				query = query.Skip(skipCount);

			if (takeCount > 0)
				query = query.Take(takeCount);

			return query;
		}
	}
}