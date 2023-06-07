using MovieApi.Data.Context;
using MovieApi.Data.Entities;
using MovieApi.Data.Interfaces;

namespace MovieApi.Data.Repositories
{
	public class MovieQueryRepository :
		AbstractQueryRepository<MovieContext, Movie>,
		IMovieQueryRepository
	{
		public MovieQueryRepository(MovieContext context) :
			base(context)
		{
		}
	}
}