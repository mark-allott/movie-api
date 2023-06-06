using Microsoft.Extensions.Logging;
using MovieApi.Data.Context;
using MovieApi.Data.DTO;
using MovieApi.Data.Entities;
using MovieApi.Data.Interfaces;

namespace MovieApi.Data.Services
{
	public class DataLoadService :
		IDataLoadService
	{
		private readonly ILogger<DataLoadService> _logger;
		private readonly IRepository<Genre> _genreRepository;
		private readonly IRepository<Movie> _movieRepository;
		private readonly IUnitOfWork<MovieContext> _unitOfWork;

		private readonly Dictionary<string, Genre> _genreMap = new Dictionary<string, Genre>();

		#region Ctor

		public DataLoadService(ILogger<DataLoadService> logger, IRepository<Genre> genreRepository,
			IRepository<Movie> movieRepository, IUnitOfWork<MovieContext> unitOfWork)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_genreRepository = genreRepository ?? throw new ArgumentNullException(nameof(genreRepository));
			_movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
			_unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
		}

		#endregion Ctor

		#region IDataLoadService implementation

		/// <inheritdoc />
		public bool LoadGenres(List<GenreDto> genres)
		{
			if (!genres.Any())
				return false;

			foreach (var genre in genres)
			{
				//	Skip any already located
				if (_genreMap.ContainsKey(genre.Name))
					continue;

				var genreEntity = _genreRepository.Queryable.FirstOrDefault(q => q.Name == genre.Name);

				if (genreEntity is null)
				{
					genreEntity = new Genre() { Name = genre.Name };
					_genreRepository.Add(genreEntity);
					_unitOfWork.SaveChanges();
				}

				_genreMap[genre.Name] = genreEntity;
			}

			return _genreMap.Any();
		}

		/// <inheritdoc />
		public bool LoadMovies(List<MovieDto> movies)
		{
			if (!movies.Any())
				return false;

			var changes = 0;
			var genreLinks = 0;

			// Really should have some kind of key for the movie entries that's unique and would permit
			// finding existing entries and updating them instead of creating new ones!

			//	First job: empty the Movies and MovieGenre tables
			_movieRepository.ExecuteDelete();

			var batch = new List<Movie>();

			//	Iterate over the
			foreach (var movie in movies)
			{
				var genres = movie.GenreList
					.Select(s => _genreMap.TryGetValue(s, out var g) ? g : null)
					.Where(q => !ReferenceEquals(null, q))
					.Cast<Genre>()
					.ToList();

				batch.Add(new Movie
				{
					Title = movie.Title,
					Overview = movie.Overview,
					ReleaseDate = movie.ReleaseDate,
					OriginalLanguage = movie.OriginalLanguage,
					Popularity = movie.Popularity,
					VoteCount = movie.VoteCount,
					VoteAverage = movie.VoteAverage,
					PosterUrl = movie.PosterUrl,
					Genres = genres
				});

				if (batch.Count < 25)
					continue;

				var (changeCount, genreCount) = SaveBatch(batch);
				changes += changeCount;
				genreLinks += genreCount;
			}

			if (batch.Any())
			{
				var result = SaveBatch(batch);
				changes += result.changeCount;
				genreLinks += result.genreCount;
			}

			return changes == movies.Count + genreLinks;
		}

		#endregion IDataLoadService implementation

		#region Helpers

		/// <summary>
		/// Helper method to store batched inserts and return the number of changed items
		/// </summary>
		/// <param name="movies">The batch of movies to return</param>
		/// <returns>A tuple of change counts</returns>
		private (int changeCount, int genreCount) SaveBatch(List<Movie> movies)
		{
			if (!movies.Any())
				return (0, 0);

			_movieRepository.AddRange(movies);
			var changes = _unitOfWork.SaveChanges();
			var genreLinks = movies.Sum(m => m.Genres.Count);

			movies.Clear();
			return (changes, genreLinks);
		}

		#endregion Helpers
	}
}