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
		private readonly IActorQueryRepository _actorRepository;

		#region Ctor

		public SearchService(ILogger<SearchService> logger, IQueryRepository<Genre> genreRepository,
			IQueryRepository<Movie> movieRepository, IActorQueryRepository actorRepository)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_genreRepository = genreRepository ?? throw new ArgumentNullException(nameof(genreRepository));
			_movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
			_actorRepository = actorRepository ?? throw new ArgumentNullException(nameof(actorRepository));
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
				(pageNumber, pageSize) = SanitisePaging(pageNumber, pageSize);

				//	Assemble where clause
				var expr = GetTitleSearchExpression(title, useSqlLike);

				//	Common expression for getting movies matching the title
				var moviesForTitle = _movieRepository.UntrackedQueryable
					.Where(expr);

				//	Find total result count
				var totalCount = moviesForTitle.Count();

				//	Assemble the collection of MovieSearchResult entities
				var collection = moviesForTitle
					.Include(i => i.Genres)
					.ConvertToSearchResult(pageNumber, pageSize);

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
				var collection = _genreRepository.UntrackedQueryable
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
				(pageNumber, pageSize) = SanitisePaging(pageNumber, pageSize);

				//	Force naming to lowercase
				genres = SanitiseGenreNamesForMatching(genres);

				//	Common expression for finding all movies for a genre
				var moviesForGenres = _genreRepository.UntrackedQueryable
					.Include(i => i.Movies)
					.Where(q => !genres.Any() || genres.Contains(q.Name.ToLower()))
					.SelectMany(m => m.Movies);

				var totalCount = moviesForGenres
					.Distinct()
					.Count();

				//	Assemble the collection of MovieSearchResult entities
				var collection = moviesForGenres
					.Include(i => i.Genres)
					.Distinct()
					.ConvertToSearchResult(pageNumber, pageSize);

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
				(pageNumber, pageSize) = SanitisePaging(pageNumber, pageSize);

				var allMovies = _movieRepository.UntrackedQueryable;

				var totalCount = allMovies.Count();

				var collection = allMovies
					.Include(i => i.Genres)
					.ConvertToSearchResult(pageNumber, pageSize);

				var response = new MovieSearchResultCollection(collection, totalCount, pageNumber, pageSize);
				return response;
			}
			finally
			{
				sw.Stop();
				_logger.LogInformation($"{nameof(Browse)}(string[]) completed in {sw.Elapsed:ss\\.fff}s");
			}
		}

		/// <inheritdoc />
		public MovieSearchResultCollection SearchByTitleAndGenre(MovieSearchByTitleAndGenreRequest request)
		{
			var sw = System.Diagnostics.Stopwatch.StartNew();
			try
			{
				//	Sanitising inputs
				var (pageNumber, pageSize) = SanitisePaging(request.Page, request.PageSize);
				var genres = SanitiseGenreNamesForMatching(request.Genres);

				//	Placeholder for the appropriate query to match against the genres specified
				IQueryable<Movie> moviesMatchingGenres;

				if (genres.Any())
				{
					var commonQuery = _genreRepository.UntrackedQueryable
						.Where(q => genres.Contains(q.Name.ToLower()));

					if (request.MatchAllGenres)
					{
						//	How many genres do we match against in total
						var genresToMatch = commonQuery.Count();

						//	Next, grab all IDs for movies which match all genre selections
						var movieIds = commonQuery
							.Include(i => i.MovieGenres)
							.SelectMany(m => m.MovieGenres)
							.GroupBy(mg => mg.Movie,
								mg => mg.Genre,
								(mwg, mgs) => new { Movie = mwg, MatchedGenres = mgs.Count() })
							.Where(q => q.MatchedGenres == genresToMatch)
							.Select(s => s.Movie.Id)
							.Distinct()
							.ToList();

						//	Go back to the movie repo with the list of IDs for the movies which match
						moviesMatchingGenres = _movieRepository.UntrackedQueryable
							.Where(q => movieIds.Contains(q.Id));
					}
					else
					{
						//	Genres specified, but we match against any of them
						moviesMatchingGenres = commonQuery
							.Include(i => i.Movies)
							.SelectMany(m => m.Movies)
							.Distinct();
					}
				}
				else
				{
					//	No genres specified, so return all possible movies for next step
					moviesMatchingGenres = _movieRepository.UntrackedQueryable;
				}

				//	Assemble where clause for movie title search
				var expr = GetTitleSearchExpression(request.Title, request.UseSqlLikeOperator);

				// Common expression for extracting the movies matching the title and genres
				var matchingMovies = moviesMatchingGenres
					.Where(expr)
					.AsQueryable();

				//	Find total result count
				var totalCount = matchingMovies.Count();

				// Assemble the collection of MovieSearchResult entities, adding in all genres for the
				// movies (not just the matching ones)
				var collection = matchingMovies
					.Include(i => i.Genres)
					.ConvertToSearchResult(pageNumber, pageSize);

				var response = new MovieSearchResultCollection(collection, totalCount, pageNumber, pageSize);
				return response;
			}
			finally
			{
				sw.Stop();
				_logger.LogInformation($"{nameof(SearchByTitleAndGenre)} completed in {sw.Elapsed:ss\\.fff}s");
			}
		}

		/// <inheritdoc />
		public ActorResultCollection GetActors(int pageNumber = 1, int pageSize = 0)
		{
			var sw = System.Diagnostics.Stopwatch.StartNew();
			try
			{
				(pageNumber, pageSize) = SanitisePaging(pageNumber, pageSize);

				var totalCount = _actorRepository.UntrackedQueryable.Count();

				var collection = _actorRepository.UntrackedQueryable
					.Select(s => s.Name)
					.SkipAndTake((pageNumber - 1) * pageSize, pageSize)
					.ToList();

				var response = new ActorResultCollection(collection, totalCount, pageNumber, pageSize);
				return response;
			}
			finally
			{
				sw.Stop();
				_logger.LogInformation($"{nameof(GetActors)} completed in {sw.Elapsed:ss\\.fff}s");
			}
		}

		/// <inheritdoc />
		public ActorResultCollection SearchActorsByName(ActorSearchByNameRequest request)
		{
			var sw = System.Diagnostics.Stopwatch.StartNew();
			try
			{
				//	Sanitising inputs
				var (pageNumber, pageSize) = SanitisePaging(request.Page, request.PageSize);

				//	Assemble where clause for movie title search
				var expr = GetNameSearchExpression(request.Name, request.UseSqlLikeOperator);

				// Common expression for extracting the movies matching the title and genres
				var matchingActors = _actorRepository.UntrackedQueryable
					.Where(expr)
					.AsQueryable();

				var totalMatched = matchingActors.Count();

				var collection = matchingActors
					.Select(s => s.Name)
					.SkipAndTake((pageNumber - 1) * pageSize, pageSize)
					.ToList();

				var response = new ActorResultCollection(collection, totalMatched, pageNumber, pageSize);
				return response;
			}
			finally
			{
				sw.Stop();
				_logger.LogInformation($"{nameof(SearchActorsByName)} completed in {sw.Elapsed:ss\\.fff}s");
			}
		}

		#endregion ISearchService implementation

		#region Helper methods

		/// <summary>
		/// Assembles a where clause expression for searching against movie titles
		/// </summary>
		/// <param name="title">The title to search for</param>
		/// <param name="useSqlLike">Specify if searching can use the SQL <c>LIKE</c> command</param>
		/// <returns>The appropriate clause for a search</returns>
		private static Expression<Func<Movie, bool>> GetTitleSearchExpression(string title, bool useSqlLike = false)
		{
			//	Remove extra spaces
			title = title.Trim();
			return string.IsNullOrWhiteSpace(title)
				? movie => true
				: useSqlLike
					? movie => EF.Functions.Like(movie.Title, title)
					: movie => movie.Title.ToLower().StartsWith(title.ToLower()) ||
										 movie.Title.ToLower().Contains(title.ToLower());
		}

		private static (int pageNumber, int pageSize) SanitisePaging(int pageNumber, int pageSize)
		{
			return QueryHelpers.SanitisePaging(pageNumber, pageSize);
		}

		/// <summary>
		/// Performs sanitising of an array of genre names
		/// </summary>
		/// <param name="genreNames">The names to be sanitised</param>
		/// <returns>An array of genre names, <c>Trim()</c>'d and in lowercase, suitable for searching with</returns>
		private static string[] SanitiseGenreNamesForMatching(params string[] genreNames)
		{
			return genreNames
				.Select(s => s.ToLower().Trim())
				.Where(s => !string.IsNullOrWhiteSpace(s))
				.Distinct()
				.OrderBy(o => o)
				.ToArray();
		}

		/// <summary>
		/// Assembles a where clause expression for searching against actor names
		/// </summary>
		/// <param name="name">The actor name to search for</param>
		/// <param name="useSqlLike">Specify if searching can use the SQL <c>LIKE</c> command</param>
		/// <returns>The appropriate clause for a search</returns>
		private static Expression<Func<Actor, bool>> GetNameSearchExpression(string name, bool useSqlLike = false)
		{
			//	Remove extra spaces
			name = name.Trim();
			return string.IsNullOrWhiteSpace(name)
				? a => true
				: useSqlLike
					? a => EF.Functions.Like(a.Name, name)
					: a => a.Name.ToLower().StartsWith(name.ToLower()) ||
										 a.Name.ToLower().Contains(name.ToLower());
		}

		#endregion Helper methods
	}
}