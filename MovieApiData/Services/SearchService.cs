using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovieApi.Data.DTO;
using MovieApi.Data.Entities;
using MovieApi.Data.Helpers;
using MovieApi.Data.Interfaces;

namespace MovieApi.Data.Services
{
	public class SearchService :
		ISearchService
	{
		private readonly ILogger<SearchService> _logger;
		private readonly IQueryRepository<Genre> _genreRepository;
		private readonly IQueryRepository<Movie> _movieRepository;

		#region Ctor

		public SearchService(ILogger<SearchService> logger, IQueryRepository<Genre> genreRepository,
			IQueryRepository<Movie> movieRepository)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_genreRepository = genreRepository ?? throw new ArgumentNullException(nameof(genreRepository));
			_movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
		}

		#endregion Ctor

		#region ISearchService implementation

		/// <inheritdoc />
		public MovieSearchResultCollection SearchByTitle(string title, int pageNumber = 1, int pageSize = 0,
			bool useSqlLike = false)
		{
			var sw = System.Diagnostics.Stopwatch.StartNew();
			try
			{
				//	Assemble where clause
				Expression<Func<Movie, bool>> expr = string.IsNullOrEmpty(title)
					? movie => true
					: useSqlLike
						? movie => EF.Functions.Like(movie.Title, title)
						: movie => movie.Title.ToLower().StartsWith(title.ToLower()) ||
											 movie.Title.ToLower().Contains(title.ToLower());

				//	Find total result count
				var totalCount = _movieRepository.Queryable
					.Where(expr)
					.Count();

				//	Assemble the collection of MovieSearchResult entities
				var collection = _movieRepository.Queryable
					.AsNoTracking()
					.Include(i => i.Genres)
					.Where(expr)
					.SkipAndTake((pageNumber - 1) * pageSize, pageSize)
					.Select(m => new MovieSearchResult(m))
					.ToList();

				var response = new MovieSearchResultCollection(collection, totalCount, pageNumber, pageSize);
				return response;
			}
			finally
			{
				sw.Stop();
				_logger.LogInformation($"{nameof(SearchByTitle)} completed in {sw.Elapsed:ss\\.fff}s");
			}
		}

		/// <inheritdoc />
		public GenreCollectionResult GetGenres()
		{
			var sw = System.Diagnostics.Stopwatch.StartNew();
			try
			{
				var collection = _genreRepository.Queryable
					.AsNoTracking()
					.Select(s => s.Name)
					.ToList();

				var response = new GenreCollectionResult(collection, collection.Count, 1, 0);
				return response;
			}
			finally
			{
				sw.Stop();
				_logger.LogInformation($"{nameof(GetGenres)} completed in {sw.Elapsed:ss\\.fff}s");
			}
		}

		/// <inheritdoc />
		public MovieSearchResultCollection SearchByGenre(int pageNumber = 1, int pageSize = 0, params string[] genres)
		{
			var sw = System.Diagnostics.Stopwatch.StartNew();
			try
			{
				//	Force naming to lowercase
				genres = genres.Where(s => !string.IsNullOrEmpty(s))
				.Select(s => s.ToLower())
				.ToArray();

				//	match genres in the datatset against the incoming list
				var matchedGenres = _genreRepository.Queryable
					.Include(i => i.Movies)
					.AsNoTracking()
					.Where(q => genres.Contains(q.Name.ToLower()))
					.ToList();

				var totalCount = _genreRepository.Queryable
					.Include(i => i.Movies)
					.AsNoTracking()
					.Where(q => genres.Contains(q.Name.ToLower()))
					.SelectMany(m => m.Movies)
					.Distinct()
					.Count();

				//	Assemble the collection of MovieSearchResult entities
				var collection = _genreRepository.Queryable
					.AsNoTracking()
					.Include(i => i.Movies)
					.AsNoTracking()
					.Where(q => genres.Contains(q.Name.ToLower()))
					.SelectMany(m => m.Movies)
					.Include(i => i.Genres)
					.Distinct()
					.SkipAndTake((pageNumber - 1) * pageSize, pageSize)
					.Select(s => new MovieSearchResult(s))
					.ToList();

				var response = new MovieSearchResultCollection(collection, totalCount, pageNumber, pageSize);
				return response;
			}
			finally
			{
				sw.Stop();
				_logger.LogInformation($"{nameof(SearchByGenre)}(string[]) completed in {sw.Elapsed:ss\\.fff}s");
			}
		}

		/// <inheritdoc />
		public MovieSearchResultCollection SearchByGenre(int pageNumber = 1, int pageSize = 0, params int[] genreIds)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public MovieSearchResultCollection Browse(int pageNumber = 1, int pageSize = 10)
		{
			var sw = System.Diagnostics.Stopwatch.StartNew();
			try
			{
				var totalCount = _movieRepository.Queryable.Count();

				var collection = _movieRepository.Queryable
					.AsNoTracking()
					.Include(i => i.Genres)
					.SkipAndTake((pageNumber - 1) * pageSize, pageSize)
					.Select(s => new MovieSearchResult(s))
					.ToList();

				var response = new MovieSearchResultCollection(collection, totalCount, pageNumber, pageSize);
				return response;
			}
			finally
			{
				sw.Stop();
				_logger.LogInformation($"{nameof(Browse)}(string[]) completed in {sw.Elapsed:ss\\.fff}s");
			}
		}

		#endregion ISearchService implementation
	}
}