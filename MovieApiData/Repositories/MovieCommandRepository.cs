using MovieApi.Data.Context;
using MovieApi.Data.Entities;
using MovieApi.Data.Interfaces;

namespace MovieApi.Data.Repositories
{
	public class MovieCommandRepository :
		AbstractCommandRepository<MovieContext, Movie>,
		IMovieCommandRepository
	{
		public MovieCommandRepository(MovieContext context) :
			base(context)
		{
		}
	}
}