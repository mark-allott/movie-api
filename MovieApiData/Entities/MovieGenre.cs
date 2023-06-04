using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApi.Data.Entities
{
	/// <summary>
	/// Acts as the link table between the movie and one genre, permitting a movie to have multiple
	/// genres when the table includes many genre entries for the single movie
	/// </summary>
	public class MovieGenre
	{
		/// <summary>
		/// The PK in the <see cref="Movie"/> entity
		/// </summary>
		public long MovieId { get; set; }

		/// <summary>
		/// The PK in the <see cref="Genre"/> entity
		/// </summary>
		public long GenreId { get; set; }

		[ForeignKey(nameof(MovieId))]
		public virtual Movie Movie { get; set; } = new();

		[ForeignKey(nameof(GenreId))]
		public virtual Genre Genre { get; set; } = new();
	}
}