using Microsoft.OpenApi.Models;

namespace MovieApi.Configuration
{
	public static class WebApiConfiguration
	{
		#region ConfigureServices extension methods

		/// <summary>
		/// Adds the required web services to the service collection
		/// </summary>
		/// <param name="services"></param>
		/// <param name="config"></param>
		/// <returns></returns>
		public static IServiceCollection AddApplicationWebApi(this IServiceCollection services, IConfiguration config)
		{
			services.AddMvc();

			//	TODO - add rate limiting functionality if needed

			//	TODO - add header authentication scheme if needed to protect from drive-by querying of API

			return services;
		}

		/// <summary>
		/// Adds the required services for using Swagger to the service collection
		/// </summary>
		/// <param name="services"></param>
		/// <param name="config"></param>
		/// <returns></returns>
		public static IServiceCollection AddApplicationSwagger(this IServiceCollection services, IConfiguration config)
		{
			services.AddEndpointsApiExplorer()
				.AddSwaggerGen(o =>
				{
					var title = config["Application:Name"] ?? "MovieApi";

					o.SwaggerDoc("v1",
						new OpenApiInfo
						{
							Title = title,
							Version = "v1",
							Contact = new OpenApiContact
							{
								Name = "Mark Allott",
								Url = new Uri("https://github.com/mark-allott"),
							}
						});
				});

			return services;
		}

		#endregion ConfigureServices extension methods

		#region Configure extension methods

		public static IApplicationBuilder UseApplicationSwagger(this IApplicationBuilder app, IConfiguration config, IWebHostEnvironment environment)
		{
			if (!environment.IsDevelopment())
				return app;

			var title = config["Application:Name"] ?? "MovieApi";

			return app.UseSwagger()
				.UseSwaggerUI(cfg =>
				{
					cfg.DocumentTitle = title;
				});
		}

		#endregion Configure extension methods
	}
}