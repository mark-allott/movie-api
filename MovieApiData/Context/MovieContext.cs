using Microsoft.EntityFrameworkCore;
using MovieApi.Data.Entities;
using MovieApi.Data.Interfaces;

namespace MovieApi.Data.Context
{
	public class MovieContext :
		DbContext,
		IMovieContext
	{
		public MovieContext(DbContextOptions<MovieContext> options) :
			base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			//	MovieGenre is a link table - create a hint for EF to use a compound PK using the two FKs it will use
			modelBuilder.Entity<MovieGenre>()
				.HasKey(k => new { k.MovieId, k.GenreId });

			//	Define the associations between Movie <-> MovieGenre <-> Genre
			modelBuilder.Entity<Movie>()
				.HasMany(m => m.Genres)
				.WithMany(m => m.Movies)
				.UsingEntity<MovieGenre>(j => j.HasKey(nameof(MovieGenre.MovieId), nameof(MovieGenre.GenreId)));
		}

		#region IMovieContext implementation

		/// <inheritdoc />
		public virtual DbSet<Movie>? Movies { get; set; }

		/// <inheritdoc />
		public virtual DbSet<Genre>? Genres { get; set; }

		#endregion IMovieContext implementation

		/// <summary>
		///	Definition for the many-to-many link table between Movies and Genres
		/// </summary>
		public DbSet<MovieGenre>? MovieGenres { get; set; }
	}
}