﻿using System.Linq.Expressions;
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
			//	Assemble where clause
			Expression<Func<Movie, bool>> expr = string.IsNullOrEmpty(title)
				? movie => true
				: useSqlLike
					? movie => EF.Functions.Like(movie.Title, title)
					: movie => movie.Title.StartsWith(title, StringComparison.InvariantCultureIgnoreCase) ||
										 movie.Title.Contains(title, StringComparison.InvariantCultureIgnoreCase);

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

		/// <inheritdoc />
		public IEnumerable<object> SearchByGenre(params string[] genres)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public IEnumerable<object> SearchByGenre(params int[] genreIds)
		{
			throw new NotImplementedException();
		}

		#endregion ISearchService implementation
	}
}