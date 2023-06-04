using MovieApi.Data.Interfaces;

namespace MovieApi.Data.DTO
{
	public class MovieDto :
		AbstractDto,
		IProduction,
		IPopularity,
		IPublicity
	{
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
	}
}