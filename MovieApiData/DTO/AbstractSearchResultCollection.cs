namespace MovieApi.Data.DTO
{
	public abstract class AbstractSearchResultCollection<T> where T : class
	{
		protected AbstractSearchResultCollection()
		{
			Results = new List<T>();
		}

		protected AbstractSearchResultCollection(IEnumerable<T> results, int totalResults, int pageNumber, int pageSize) :
			this()
		{
			//	Add the supplied results to the result list
			Results.AddRange(results);

			//	Prevents against negative values being passed
			TotalResults = Math.Max(0, totalResults);

			//	PageSize zero indicates all possible results being returned
			if (pageSize == 0)
			{
				//	Set size and page numbering correctly for all results
				PageNumber = 1;
				PageSize = 0;
				PageCount = 1;
			}
			else
			{
				//	Only certainty is the page size, set that first
				PageSize = pageSize;

				//	PageCount is set to account for any partially pages
				PageCount = (int)Math.Ceiling(totalResults / (double)pageSize);

				//	Current page number can either be the one suggested, or at most the value of PageCount
				PageNumber = Math.Min(pageNumber, PageCount);
			}
		}

		public List<T> Results { get; }

		public int TotalResults { get; }

		public int PageNumber { get; }

		public int PageSize { get; }

		public int PageCount { get; }
	}
}