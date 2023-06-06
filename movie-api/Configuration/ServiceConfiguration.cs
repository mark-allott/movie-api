using System.Reflection;
using MovieApi.Data.Context;
using MovieApi.Data.Interfaces;

namespace MovieApi.Configuration;

public static class ServiceConfiguration
{
	#region ConfigureServices extension methods

	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{
		var searchAssemblies = new Assembly[] { typeof(ServiceConfiguration).Assembly, typeof(MovieContext).Assembly };

		return services.AutoRegisterApplicationServices(searchAssemblies);
	}

	public static IServiceCollection AutoRegisterApplicationServices(this IServiceCollection services,
		params Assembly[] assemblies)
	{
		if (!assemblies.Any())
			throw new ArgumentException(nameof(assemblies));

		var interfaceTypes = new[] { typeof(IAutoRegisterService) };

		// Locate services with class inheritance of the interfaceTypes which are not abstract classes,
		// then locate all classes whose interfaces that are not in the interfaceTypes list and present
		// for auto-registration
		var autoServices = assemblies
			.SelectMany(s => s.ExportedTypes)
			.Where(q => q.IsClass &&
									!q.IsAbstract &&
									!ReferenceEquals(null, q.BaseType))
			.Select(s => new
			{
				ExportedType = s,
				ImplementedTypes = s.GetInterfaces()
					.Where(q => interfaceTypes.Contains(q))
					.ToList()
			})
			.Where(q => q.ImplementedTypes.Any())
			.Select(s => new
			{
				s.ExportedType,
				Interfaces = s.ExportedType.GetInterfaces()
					.Where(q => !interfaceTypes.Contains(q))
					.ToList()
			})
			.Where(q => q.Interfaces.Any())
			.ToList();

		//	Loop through each service and register it as transient
		autoServices.ForEach(s =>
		{
			services.AddTransient(s.ExportedType);

			//	Loop through all interfaces and register a transient service for the interface to the concrete class
			s.Interfaces.ForEach(i =>
			{
				services.AddTransient(i, s.ExportedType);
			});
		});
		return services;
	}

	#endregion ConfigureServices extension methods
}