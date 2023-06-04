using System.Diagnostics.CodeAnalysis;
using MovieApi.Data.Interfaces;

namespace MovieApi.Data.DTO;

public class KaggleData :
	IProduction,
	IPopularity,
	IPublicity
{
	#region IProduction implementation

	/// <inheritdoc />
	[AllowNull]
	public string Title { get; set; }

	/// <inheritdoc />
	[AllowNull]
	public string Overview { get; set; }

	/// <inheritdoc />
	public DateTime ReleaseDate { get; set; }

	/// <inheritdoc />
	[AllowNull]
	public string OriginalLanguage { get; set; }

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
	[AllowNull]
	public string PosterUrl { get; set; }

	#endregion IPublicity implementation

	[AllowNull]
	public string Genres { get; set; }
}