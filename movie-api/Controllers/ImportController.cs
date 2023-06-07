using Microsoft.AspNetCore.Mvc;
using MovieApi.Data.Interfaces;
using MovieApi.Data.Providers;

namespace MovieApi.Controllers
{
	public class ImportController :
		AbstractApiController
	{
		private readonly IWebHostEnvironment _environment;
		private readonly IDataLoadService _dataLoadService;

		#region Ctor

		public ImportController(ILoggerFactory loggerFactory, IWebHostEnvironment environment, IDataLoadService dataLoadService) :
			base(loggerFactory)
		{
			_environment = environment ?? throw new ArgumentNullException(nameof(environment));
			_dataLoadService = dataLoadService ?? throw new ArgumentNullException(nameof(dataLoadService));
		}

		#endregion Ctor

		[Route(nameof(LoadKaggleDataset))]
		[HttpPost]
		public IActionResult LoadKaggleDataset()
		{
			var sw = new System.Diagnostics.Stopwatch();
			sw.Start();
			try
			{
				bool result = false;
				var provider = Request.HttpContext.RequestServices.GetService(typeof(KaggleDataProvider)) as KaggleDataProvider;

				if (provider is null)
					return new NotFoundResult();

				//	Assemble the location for the sanitised CSV file and perform an import of the data
				var filepath = Path.Combine(_environment.WebRootPath, "data", "mymoviedb_cleaned.csv");
				result = provider.ImportDataFromFile(filepath);

				if (!result)
					return new JsonResult(new { Result = false, Message = "Loading of Kaggle data failed" });

				var genres = provider.GetGenreList();
				var movies = provider.GetMovieList();

				result = genres.Any() && movies.Any();

				result = result &&
				         _dataLoadService.LoadGenres(genres) &&
				         _dataLoadService.LoadMovies(movies);

				var msg = result
					? $"Data loaded successfully with {genres.Count} Genres and {movies.Count} Movies"
					: "Processing of Kaggle data failed";

				Logger.LogInformation($"{nameof(LoadKaggleDataset)}: Result={result}, Message={msg}");
				return new JsonResult(new { Result = result, Message = msg });
			}
			finally
			{
				sw.Stop();
				Logger.LogInformation($"{nameof(LoadKaggleDataset)} Elapsed time = {sw.Elapsed}");
			}
		}
	}
}