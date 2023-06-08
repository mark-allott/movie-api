using MovieApi.Data.Context;
using MovieApi.Data.Entities;
using MovieApi.Data.Interfaces;

namespace MovieApi.Data.Repositories
{
	public class ActorQueryRepository :
		AbstractQueryRepository<MovieContext, Actor>,
		IActorQueryRepository
	{
		public ActorQueryRepository(MovieContext context) :
			base(context)
		{
		}
	}
}