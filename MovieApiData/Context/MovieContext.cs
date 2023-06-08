﻿using Microsoft.EntityFrameworkCore;
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
				.UsingEntity<MovieGenre>(j => j.HasKey(nameof(Entities.MovieGenre.MovieId), nameof(Entities.MovieGenre.GenreId)));

			//	MovieActor is a link table - create a hint for EF to use a compound PK using the two FKs it will use
			modelBuilder.Entity<MovieActor>()
				.HasKey(k => new { k.MovieId, k.ActorId });

			//	Define the associations between Movie <-> MovieActor <-> Actor
			modelBuilder.Entity<Movie>()
				.HasMany(m => m.Cast)
				.WithMany(m => m.Movies)
				.UsingEntity<MovieActor>(j => j.HasKey(nameof(Entities.MovieActor.MovieId), nameof(Entities.MovieActor.ActorId)));
		}

		#region IMovieContext implementation

		/// <inheritdoc />
		public virtual DbSet<Movie>? Movies { get; set; }

		/// <inheritdoc />
		public virtual DbSet<Genre>? Genres { get; set; }

		/// <inheritdoc />
		public virtual DbSet<Actor>? Actors { get; set; }

		#endregion IMovieContext implementation

		/// <summary>
		///	Definition for the many-to-many link table between Movies and Genres
		/// </summary>
		public DbSet<MovieGenre>? MovieGenre { get; set; }

		/// <summary>
		///	Definition for the many-to-many link table between Movies and Actors
		/// </summary>
		public DbSet<MovieActor>? MovieActor { get; set; }
	}
}