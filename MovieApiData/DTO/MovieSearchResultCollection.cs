namespace MovieApi.Data.DTO
{
	public class MovieSearchResultCollection :
		AbstractSearchResultCollection<MovieSearchResult>
	{
		public MovieSearchResultCollection(IEnumerable<MovieSearchResult> results, int totalResults, int pageNumber, int pageSize) :
			base(results, totalResults, pageNumber, pageSize)
		{
		}
	}
}