using MovieApi.Data.DTO;
using MovieApi.Data.Interfaces;

// ReSharper disable VirtualMemberCallInConstructor

namespace MovieApi.Data.Entities
{
	public class Actor :
		ActorDto,
		IIdentifiable
	{
		public Actor()
		{
			MovieActors = new HashSet<MovieActor>();
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
		public virtual ICollection<MovieActor> MovieActors { get; set; }

		/// <summary>
		/// Skip-link navigation to the movies in this genre
		/// </summary>
		public virtual ICollection<Movie> Movies { get; set; }

		#endregion Navigation links
	}
}