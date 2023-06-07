using MovieApi.Data.Entities;
using MovieApi.Data.Interfaces;

namespace MovieApi.Data.DTO
{
	public class MovieSearchResult :
		IProduction,
		IPopularity,
		IPublicity,
		IHaveGenre
	{
		#region Ctor

		public MovieSearchResult()
		{
		}

		public MovieSearchResult(Movie movie)
		{
			Title = movie.Title;
			Overview = movie.Overview;
			ReleaseDate = movie.ReleaseDate.Date;
			OriginalLanguage = movie.OriginalLanguage;
			Popularity = movie.Popularity;
			VoteCount = movie.VoteCount;
			VoteAverage = movie.VoteAverage;
			PosterUrl = movie.PosterUrl;
			Genre = string.Join(", ", movie.Genres.Select(s => s.Name));
		}

		#endregion Ctor

		#region IProduction implementation

		/// <inheritdoc />
		public string Title { get; set; } = string.Empty;

		/// <inheritdoc />
		public string Overview { get; set; } = string.Empty;

		/// <inheritdoc />
		public DateTime ReleaseDate { get; set; }

		/// <inheritdoc />
		public string OriginalLanguage { get; set; } = string.Empty;

		#endregion IProduction implementation

		#region IPopularity implementation

		/// <inheritdoc />
		public double Popularity { get; set; }

		/// <inheritdoc />
		public long VoteCount { get; set; }

		/// <inheritdoc />
		public double VoteAverage { get; set; }

		#endregion IPopularity implementation

		#region IPublicity implementation

		/// <inheritdoc />
		public string? PosterUrl { get; set; }

		#endregion IPublicity implementation

		#region IHaveGenre implementation

		public string Genre { get; set; } = string.Empty;

		#endregion IHaveGenre implementation
	}
}