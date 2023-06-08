using MovieApi.Data.Context;
using MovieApi.Data.Entities;
using MovieApi.Data.Interfaces;

namespace MovieApi.Data.Repositories
{
	public class ActorCommandRepository :
		AbstractCommandRepository<MovieContext, Actor>,
		IActorCommandRepository
	{
		public ActorCommandRepository(MovieContext context) :
			base(context)
		{
		}
	}
}