using Microsoft.AspNetCore.Mvc;
using MovieApi.Data.DTO;
using MovieApi.Data.Interfaces;

namespace MovieApi.Controllers
{
	public class BrowseController :
		AbstractApiController
	{
		private readonly ISearchService _searchService;

		public BrowseController(ILoggerFactory loggerFactory, ISearchService searchService) :
			base(loggerFactory)
		{
			_searchService = searchService ?? throw new ArgumentNullException(nameof(searchService));
		}

		[Route("{page:int?}/{pageSize:int?}")]
		[HttpGet]
		[ProducesDefaultResponseType(typeof(MovieSearchResultCollection))]
		public IActionResult BrowseMovies([FromRoute] int? page = 1, [FromRoute] int? pageSize = 10)
		{
			var result = _searchService.Browse(page.GetValueOrDefault(1), pageSize.GetValueOrDefault(10));
			return new JsonResult(result);
		}
	}
}