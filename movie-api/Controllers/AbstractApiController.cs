using Microsoft.AspNetCore.Mvc;

namespace MovieApi.Controllers
{
	[ApiController]
	[Route("api/[controller]/")]
	public abstract class AbstractApiController :
		ControllerBase
	{
		protected readonly ILogger Logger;

		#region Ctor

		protected AbstractApiController(ILoggerFactory loggerFactory)
		{
			Logger = loggerFactory.CreateLogger(GetType().Name);
		}

		#endregion Ctor
	}
}