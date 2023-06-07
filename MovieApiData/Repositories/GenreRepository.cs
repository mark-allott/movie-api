using MovieApi.Data.Context;
using MovieApi.Data.Entities;
using MovieApi.Data.Interfaces;

namespace MovieApi.Data.Repositories
{
	public class GenreRepository :
		AbstractRepository<MovieContext, Genre>,
		IGenreRepository
	{
		public GenreRepository(MovieContext context) :
			base(context)
		{
		}
	}
}