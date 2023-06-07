using MovieApi.Data.DTO;
using MovieApi.Data.Entities;

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

		/// <summary>
		/// Common method to take an <see cref="IQueryable{T}"/> of <see cref="Movie"/> entities and convert into a suitable <see cref="List{T}"/> or <see cref="MovieSearchResult"/> using any paging
		/// </summary>
		/// <param name="movies">An <see cref="IQueryable{T}"/> of the movies</param>
		/// <param name="pageNumber">The page number to extract</param>
		/// <param name="pageSize">The page size to return</param>
		/// <returns>A <see cref="List{T}"/> of <see cref="MovieSearchResult"/> entries, of the appropriate paging size</returns>
		/// <remarks>Paging details are sanitised, but not returned as part of the result</remarks>
		public static List<MovieSearchResult> ConvertToSearchResult(this IQueryable<Movie> movies, int pageNumber,
			int pageSize)
		{
			return movies.SkipAndTake((pageNumber - 1) * pageSize, pageSize)
				.Select(s => new MovieSearchResult(s))
				.ToList();
		}

		/// <summary>
		/// Common method to ensure that the paging details are sensible
		/// </summary>
		/// <param name="pageNumber">The number of the page to be checked</param>
		/// <param name="pageSize">The size of the page to be checked</param>
		/// <returns>The sanitised values</returns>
		/// <remarks>
		/// There's no concept of upper bounds for these values, just minimums. <paramref
		/// name="pageNumber"/> will always be &gt;= 1, <paramref name="pageSize"/> will always be &gt;=
		/// 0. <paramref name="pageSize"/> of zero has special meaning to the consumers: return everything
		/// </remarks>
		public static (int pageNumber, int pageSize) SanitisePaging(int pageNumber, int pageSize)
		{
			pageNumber = Math.Max(1, pageNumber);
			pageSize = Math.Max(0, pageSize);
			return (pageNumber, pageSize);
		}
	}
}