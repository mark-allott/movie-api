using MovieApi.Data.Context;
using MovieApi.Data.Entities;
using MovieApi.Data.Interfaces;

namespace MovieApi.Data.Repositories
{
	public class ActorRepository :
		AbstractRepository<MovieContext, Actor>,
		IActorRepository
	{
		public ActorRepository(MovieContext context) :
			base(context)
		{
		}
	}
}