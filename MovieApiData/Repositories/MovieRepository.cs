using MovieApi.Data.Context;
using MovieApi.Data.Entities;
using MovieApi.Data.Interfaces;

namespace MovieApi.Data.Repositories
{
	public class MovieRepository :
		AbstractRepository<MovieContext, Movie>,
		IMovieRepository
	{
		public MovieRepository(MovieContext context) :
			base(context)
		{
		}
	}
}