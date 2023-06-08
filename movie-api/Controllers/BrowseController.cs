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

		[Route("movies/{page:int?}/{pageSize:int?}")]
		[HttpGet]
		[ProducesDefaultResponseType(typeof(MovieSearchResultCollection))]
		public IActionResult BrowseMovies([FromRoute] int? page = 1, [FromRoute] int? pageSize = 10)
		{
			try
			{
				var result = _searchService.Browse(page.GetValueOrDefault(1), pageSize.GetValueOrDefault(10));
				return new JsonResult(result);
			}
			catch (Exception e)
			{
				Logger.LogError(e, $"Error when executing {nameof(BrowseMovies)}");
			}

			return BadRequest();
		}

		[Route("actors/{page:int?}/{pageSize:int?}")]
		[HttpGet]
		[ProducesDefaultResponseType(typeof(ActorResultCollection))]
		public IActionResult BrowseActors([FromRoute] int? page = 1, [FromRoute] int? pageSize = 10)
		{
			try
			{
				var result = _searchService.GetActors(page.GetValueOrDefault(1), pageSize.GetValueOrDefault(10));
				return new JsonResult(result);
			}
			catch (Exception e)
			{
				Logger.LogError(e, $"Error when executing {nameof(BrowseActors)}");
			}

			return BadRequest();
		}
	}
}