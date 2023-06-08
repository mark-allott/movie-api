namespace MovieApi.Data.DTO
{
	/// <summary>
	/// Defines a request object for searching for actor by name
	/// </summary>
	public class ActorSearchByNameRequest
	{
		/// <summary>
		/// A string to match against the title of the movie
		/// </summary>
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// Allows paging of the results returned
		/// </summary>
		public int Page { get; set; } = 1;

		/// <summary>
		/// Allows setting of the page size
		/// </summary>
		/// <remarks>A page size of zero indicates returning all results</remarks>
		public int PageSize { get; set; } = 0;

		/// <summary>
		/// Changes the way that the Title property is used in searching to match the <c>LIKE</c>
		/// operator, including use of wildcards
		/// </summary>
		public bool UseSqlLikeOperator { get; set; } = false;
	}
}