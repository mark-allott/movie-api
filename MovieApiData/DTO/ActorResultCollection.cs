namespace MovieApi.Data.DTO;

public class ActorResultCollection :
	AbstractSearchResultCollection<string>
{
	public ActorResultCollection(IEnumerable<string> results, int totalResults, int pageNumber, int pageSize) :
		base(results, totalResults, pageNumber, pageSize)
	{
	}
}