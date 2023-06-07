namespace MovieApi.Data.DTO;

public class GenreCollectionResult :
	AbstractSearchResultCollection<string>
{
	public GenreCollectionResult(IEnumerable<string> results, int totalResults, int pageNumber, int pageSize) :
		base(results, totalResults, pageNumber, pageSize)
	{
	}
}