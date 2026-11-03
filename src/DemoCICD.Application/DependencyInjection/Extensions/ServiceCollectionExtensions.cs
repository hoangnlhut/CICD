using DemoCICD.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace DemoCICD.Application.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConfigureApplication(this IServiceCollection services)
        {
            // Implementation for configuring MediaR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));

            //add configuration for the application
            // Register MediatR and tell it to scan the current assembly for handlers
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipeLineBehavior<,>));

            //add fluent validation services
            services.AddValidatorsFromAssembly(AssemblyReference.Assembly, includeInternalTypes: true);

            return services;
        }
    }
}
