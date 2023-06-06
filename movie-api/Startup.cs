using MovieApi.Configuration;
using MovieApi.Data.Providers;

namespace MovieApi
{
	internal class Startup
	{
		public IWebHostEnvironment Environment { get; }
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration, IWebHostEnvironment environment)
		{
			Environment = environment ?? throw new ArgumentNullException(nameof(environment));
			Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			_ = app ?? throw new ArgumentNullException(nameof(app));
			_ = env ?? throw new ArgumentNullException(nameof(env));

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseCors(b => b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
			app.UseAuthorization();
			app.UseApplicationSwagger(Configuration, Environment);

			app.UseEndpoints(e =>
			{
				e.MapControllerRoute("default", "{controller}/{action=Index}/{id?}");
			});
		}

		public void ConfigureServices(IServiceCollection services)
		{
			_ = services ?? throw new ArgumentNullException(nameof(services));

			//	Add default service definitions
			services.AddOptions();

			services.AddApplicationData(Configuration)
				.AddApplicationWebApi(Configuration)
				.AddApplicationSwagger(Configuration)
				.AddAuthorization();

			services.AddScoped(typeof(KaggleDataProvider));
			services.AddScoped(typeof(KaggleDataImporter));
		}
	}
}