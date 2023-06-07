using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data.Context;
using MovieApi.Data.Interfaces;
using MovieApi.Data.Repositories;
using MovieApi.Data.UOW;

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
			//	Check data connections are defined and assign appropriately
			var c = config.GetConnectionString("Default");

			if (string.IsNullOrWhiteSpace(c))
				throw new ArgumentNullException("No connection string for database", null as Exception);

			//	Define the context, using SqlServer and the connection string we found
			services.AddDbContext<MovieContext>(o =>
			{
				o.UseSqlServer(c);
			});

			//	Register generic interfaces to concrete instances
			services.AddScoped<IMovieContext, MovieContext>();

			services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

			return services.AutoRegisterCustomRepositories(typeof(MovieContext).Assembly);
		}

		/// <summary>
		/// Perform auto-registration of the defined repos based on the defined abstract repository and interface types in the <paramref name="assemblies"/>
		/// </summary>
		/// <param name="services">The current service registrations</param>
		/// <param name="assemblies">The assemblies to scan</param>
		/// <returns>Updated service registrations</returns>
		/// <exception cref="ArgumentException"></exception>
		public static IServiceCollection AutoRegisterCustomRepositories(this IServiceCollection services,
			params Assembly[] assemblies)
		{
			if (!assemblies.Any())
				throw new ArgumentException(nameof(assemblies));

			var abstractTypes = new[]
			{
				typeof(AbstractCommandRepository<,>),
				typeof(AbstractQueryRepository<,>),
				typeof(AbstractRepository<,>),
			};

			var interfaceTypes = new[]
			{
				typeof(ICommandRepository<>),
				typeof(IQueryRepository<>),
				typeof(IRepository<>),
			};

			var repos = assemblies
				.SelectMany(s => s.ExportedTypes)
				.Where(q => q.IsClass &&
										!q.IsAbstract &&
										!ReferenceEquals(null, q.BaseType) &&
										q.BaseType.IsGenericType &&
										abstractTypes.Contains(q.BaseType.GetGenericTypeDefinition()))
				.Select(s => new
				{
					ExportedType = s,
					ImplementedTypes = s.GetInterfaces()
						.Where(q => q.IsGenericType &&
												interfaceTypes.Contains(q.GetGenericTypeDefinition()))
						.ToList()
				})
				.Where(q => q.ImplementedTypes.Any())
				.ToList();

			foreach (var repo in repos)
			{
				repo.ImplementedTypes
					.ForEach(i => services.AddScoped(i, repo.ExportedType));

				repo.ExportedType
					.GetInterfaces()
					.Where(q => !q.IsGenericType &&
											q.GetInterfaces()
												.Any(i => i.IsGenericType && interfaceTypes.Contains(i.GetGenericTypeDefinition())))
					.ToList()
					.ForEach(i => services.AddScoped(i, repo.ExportedType));
			}

			return services;
		}
	}
}