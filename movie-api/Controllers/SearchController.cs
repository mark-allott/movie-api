using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Data.DTO;
using MovieApi.Data.Interfaces;

namespace MovieApi.Controllers
{
	public class SearchController :
		AbstractApiController
	{
		private readonly ISearchService _searchService;

		#region Ctor

		public SearchController(ILoggerFactory loggerFactory, ISearchService searchService) :
			base(loggerFactory)
		{
			_searchService = searchService ?? throw new ArgumentNullException(nameof(searchService));
		}

		#endregion Ctor

		[Route(template: "movie/{title:required}/{page:int?}/{pageSize:int?}")]
		[HttpGet]
		[ProducesDefaultResponseType(typeof(MovieSearchResultCollection))]
		public IActionResult GetMoviesByTitle([FromRoute] string title, [FromRoute] int? page = 1, [FromRoute] int? pageSize = 0)
		{
			try
			{
				var result = _searchService.SearchByTitle(title, page.GetValueOrDefault(1), pageSize.GetValueOrDefault(0));
				return new JsonResult(result);
			}
			catch (Exception e)
			{
				Logger.LogError(e, $"Error when executing {nameof(GetMoviesByTitle)} with {nameof(title)}='{title}', {nameof(page)}={page}, {nameof(pageSize)}={pageSize}");
			}

			return BadRequest();
		}

		[Route("movie")]
		[HttpPost]
		[ProducesDefaultResponseType(typeof(MovieSearchResultCollection))]
		public IActionResult GetMoviesByTitle([FromBody] MovieSearchByTitleRequest byTitleRequest)
		{
			try
			{
				var result = _searchService
					.SearchByTitle(byTitleRequest.Title, byTitleRequest.Page, byTitleRequest.PageSize, byTitleRequest.UseSqlLikeOperator);
				return new JsonResult(result);
			}
			catch (Exception e)
			{
				Logger.LogError(e, $"Error when executing {nameof(GetMoviesByTitle)} with payload: {JsonSerializer.Serialize(byTitleRequest)}");
			}
			return BadRequest();
		}

		[Route("genres")]
		[HttpGet]
		[ProducesDefaultResponseType(typeof(GenreCollectionResult))]
		public IActionResult GetGenres()
		{
			try
			{
				var result = _searchService.GetGenres();
				return new JsonResult(result);
			}
			catch (Exception e)
			{
				Logger.LogError(e, $"Error when executing {nameof(GetGenres)}");
			}

			return BadRequest();
		}

		[Route("genre/{genreName:required}/{page:int?}/{pageSize:int?}")]
		[HttpGet]
		[ProducesDefaultResponseType(typeof(MovieSearchResultCollection))]
		public IActionResult GetMoviesByGenreName([FromRoute] string genreName, [FromRoute] int? page = 1, [FromRoute] int? pageSize = 0)
		{
			try
			{
				var result = _searchService.SearchByGenre(page.GetValueOrDefault(1), pageSize.GetValueOrDefault(0), genreName);
				return new JsonResult(result);
			}
			catch (Exception e)
			{
				Logger.LogError(e, $"Error when executing {nameof(GetMoviesByGenreName)} with {nameof(genreName)}='{genreName}', {nameof(page)}={page}, {nameof(pageSize)}={pageSize}");
			}

			return BadRequest();
		}

		[Route("movies")]
		[HttpPost]
		[ProducesDefaultResponseType(typeof(MovieSearchResultCollection))]
		public IActionResult GetMoviesByTitleAndGenre([FromBody] MovieSearchByTitleAndGenreRequest request)
		{
			try
			{
				var result = _searchService.SearchByTitleAndGenre(request);
				return new JsonResult(result);
			}
			catch (Exception e)
			{
				Logger.LogError(e, $"Error when executing {nameof(GetMoviesByTitle)} with payload: {JsonSerializer.Serialize(request)}");
			}
			return BadRequest();
		}
	}
}