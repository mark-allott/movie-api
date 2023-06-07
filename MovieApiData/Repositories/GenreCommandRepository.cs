using MovieApi.Data.Context;
using MovieApi.Data.Entities;
using MovieApi.Data.Interfaces;

namespace MovieApi.Data.Repositories
{
	public class GenreCommandRepository :
		AbstractCommandRepository<MovieContext, Genre>,
		IGenreCommandRepository
	{
		public GenreCommandRepository(MovieContext context) :
			base(context)
		{
		}
	}
}