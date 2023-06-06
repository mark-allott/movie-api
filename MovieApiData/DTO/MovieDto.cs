using System.ComponentModel.DataAnnotations.Schema;
using MovieApi.Data.Interfaces;

namespace MovieApi.Data.DTO
{
	public class MovieDto :
		AbstractDto,
		IProduction,
		IPopularity,
		IPublicity,
		IHaveGenre
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

		#region IHaveGenre implementation

		[NotMapped]
		public string Genre { get; set; } = string.Empty;

		[NotMapped]
		public List<GenreDto> GenreList
		{
			get
			{
				return string.IsNullOrWhiteSpace(Genre)
					? new List<GenreDto>()
					: Genre.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
						.OrderBy(o => o)
						.Select(s => new GenreDto() { Name = s })
						.ToList();
			}
		}

		#endregion IHaveGenre implementation
	}
}