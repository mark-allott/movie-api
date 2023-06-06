using System;
using Microsoft.Extensions.Logging;
using MovieApi.Data.DTO;
using MovieApi.Data.Interfaces;

namespace MovieApi.Data.Providers
{
	public class KaggleDataProvider :
		IDataProvider
	{
		private readonly ILogger<KaggleDataProvider> _logger;
		private readonly KaggleDataImporter _importer;

		public KaggleDataProvider(ILogger<KaggleDataProvider> logger, KaggleDataImporter importer)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_importer = importer ?? throw new ArgumentNullException(nameof(importer));
		}

		/// <summary>
		/// Imports the data from the specified file in <param name="filePath"></param>
		/// </summary>
		/// <param name="filePath">A path to the data file</param>
		/// <returns>A flag indicating success or failure</returns>
		public bool ImportDataFromFile(string filePath)
		{
			using (var fs = new FileStream(filePath, FileMode.Open))
			{
				return _importer.ImportFromStream(fs);
			}
		}

		/// <inheritdoc />
		public List<GenreDto> GetGenreList()
		{
			if (!_importer.Data.Any())
				return new List<GenreDto>();

			var genres = _importer.Data
				.SelectMany(s => s.Genres.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
				.Distinct()
				.OrderBy(s => s)
				.Select(s => new GenreDto() { Name = s })
				.ToList();

			return genres;
		}

		/// <inheritdoc />
		public List<MovieDto> GetMovieList()
		{
			if (!_importer.Data.Any())
				return new List<MovieDto>();

			var movies = _importer.Data
				.Select(s => new MovieDto
				{
					Title = s.Title,
					Overview = s.Overview,
					ReleaseDate = s.ReleaseDate.Date,
					OriginalLanguage = s.OriginalLanguage,
					Popularity = s.Popularity,
					VoteCount = s.VoteCount,
					VoteAverage = s.VoteAverage,
					PosterUrl = s.PosterUrl,
					Genre = s.Genres
				})
				.ToList();

			return movies;
		}
	}
}