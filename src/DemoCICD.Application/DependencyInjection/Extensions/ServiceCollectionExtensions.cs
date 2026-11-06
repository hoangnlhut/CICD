using DemoCICD.Application.Behaviors;
using DemoCICD.Application.Mappers;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DemoCICD.Application.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConfigureApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));
            services.AddApplicationValidation();
            services.AddApplicationMapping();
            return services;
        }

        private static IServiceCollection AddApplicationValidation(this IServiceCollection services)
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipeLineBehavior<,>));
            services.AddValidatorsFromAssembly(Contract.AssemblyReference.Assembly, includeInternalTypes: true);
            return services;
        }

        private static IServiceCollection AddApplicationMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ServiceProfile));
            return services;
        }
    }
}
