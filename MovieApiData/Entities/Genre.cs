using MovieApi.Data.DTO;
using MovieApi.Data.Interfaces;

// ReSharper disable VirtualMemberCallInConstructor

namespace MovieApi.Data.Entities
{
	public class Genre :
		GenreDto,
		IIdentifiable
	{
		public Genre()
		{
			MovieGenres = new HashSet<MovieGenre>();
			Movies = new HashSet<Movie>();
		}

		#region IIdentifiable implementation

		/// <inheritdoc />
		public long Id { get; set; }

		#endregion IIdentifiable implementation

		#region Navigation links

		/// <summary>
		/// Maps to the link table between genre and movie
		/// </summary>
		public virtual ICollection<MovieGenre> MovieGenres { get; set; }

		/// <summary>
		/// Skip-link navigation to the movies in this genre
		/// </summary>
		public virtual ICollection<Movie> Movies { get; set; }

		#endregion Navigation links
	}
}