using System.Reflection;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Application.Common.Mappings;

public static class MappingConfig
{
    public static IServiceCollection AddMapsterConfig(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;

        var assembly = Assembly.GetExecutingAssembly();
        
        // Scan for traditional IRegister implementations
        config.Scan(assembly);

        // Manually configure IMapFrom<> types without instantiating them
        var types = assembly.GetExportedTypes()
            .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
            .ToList();

        foreach (var type in types)
        {
            var interfaceType = type.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>));
            var entityType = interfaceType.GetGenericArguments()[0];
            
            config.NewConfig(entityType, type);
        }
        services.AddSingleton(config);

        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}