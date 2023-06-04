using Microsoft.EntityFrameworkCore;
using MovieApi.Data.Entities;

namespace MovieApi.Data.Interfaces
{
	public interface IMovieContext
	{
		/// <summary>
		/// The list of movies in the DB
		/// </summary>
		DbSet<Movie>? Movies { get; set; }

		/// <summary>
		/// The list of genres in the DB
		/// </summary>
		DbSet<Genre>? Genres { get; set; }
	}
}