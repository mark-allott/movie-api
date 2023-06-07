using MovieApi.Data.DTO;
using MovieApi.Data.Interfaces;

// ReSharper disable VirtualMemberCallInConstructor

namespace MovieApi.Data.Entities
{
	public class Movie :
		MovieDto,
		IIdentifiable
	{
		public Movie()
		{
			MovieGenres = new HashSet<MovieGenre>();
			Genres = new HashSet<Genre>();
			Actors = new HashSet<Actor>();
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
		/// Skip-link navigation to the genre(s) associated with this movie
		/// </summary>
		public virtual ICollection<Genre> Genres { get; set; }

		/// <summary>
		/// Skip-link navigation to the Actor(s) associated with this movie
		/// </summary>
		public virtual ICollection<Actor> Actors { get; set; }

		#endregion Navigation links
	}
}