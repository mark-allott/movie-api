using Microsoft.EntityFrameworkCore;
using MovieApi.Data.Context;

namespace MovieApi.Configuration
{
	public static class DataConfiguration
	{
		/// <summary>
		/// Perform configuration for the application to connect to its data
		/// </summary>
		/// <param name="services"></param>
		/// <param name="config"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"></exception>
		public static IServiceCollection AddApplicationData(this IServiceCollection services, IConfiguration config)
		{
			var c = config.GetConnectionString("Default");

			//	Check data connections are defined and assign appropriately
			if (string.IsNullOrWhiteSpace(c))
				throw new ArgumentNullException("No connection string for database", null as Exception);

			services.AddDbContext<MovieContext>(o =>
			{
				o.UseSqlServer(c);
			});
			return services;
		}
	}
}