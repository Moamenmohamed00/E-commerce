using System.Reflection;
using ECommerce.Application.Common.Behaviors;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Common.Mappings;
using ECommerce.Application.Common.Models;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        // Register MediatR
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
            
            // Register pipeline behaviors
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        // Register FluentValidation
        services.AddValidatorsFromAssembly(assembly);

        // Register Mapster
        services.AddMapsterConfig();
        return services;
    }
}