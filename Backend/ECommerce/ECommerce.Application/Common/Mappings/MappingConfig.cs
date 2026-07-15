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


        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);

        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}