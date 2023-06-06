using System.ComponentModel.DataAnnotations.Schema;
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
	[Column("Release_Date")]
	public DateTime ReleaseDate { get; set; }

	/// <inheritdoc />
	[AllowNull, Column("Original_Language")]
	public string OriginalLanguage { get; set; }

	#endregion IProduction implementation

	#region IPopularity implementation

	/// <inheritdoc />
	public double Popularity { get; set; }

	/// <inheritdoc />
	[Column("Vote_Count")]
	public long VoteCount { get; set; }

	/// <inheritdoc />
	[Column("Vote_Average")]
	public double VoteAverage { get; set; }

	#endregion IPopularity implementation

	#region IPublicity implementation

	/// <inheritdoc />
	[AllowNull, Column("Poster_Url")]
	public string PosterUrl { get; set; }

	#endregion IPublicity implementation

	[AllowNull, Column("Genre")]
	public string Genres { get; set; }
}