using MovieApi.Data.Context;
using MovieApi.Data.Entities;
using MovieApi.Data.Interfaces;

namespace MovieApi.Data.Repositories
{
	public class GenreQueryRepository :
		AbstractQueryRepository<MovieContext, Genre>,
		IGenreQueryRepository
	{
		public GenreQueryRepository(MovieContext context) :
			base(context)
		{
		}
	}
}