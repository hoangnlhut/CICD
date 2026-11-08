using DemoCICD.Domain.Abstractions;
using DemoCICD.Domain.Abstractions.Repositories;
using DemoCICD.Domain.Entities.Identity;
using DemoCICD.Persistence.DependencyInjection.Options;
using DemoCICD.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DemoCICD.Persistence.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConfigurePersistenceSqlServer(this IServiceCollection services)
        {
            services.AddDbContextPool<DbContext, ApplicationDbContext>((serviceProvider, optionsBuilder) =>
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                var options = serviceProvider.GetRequiredService<IOptionsMonitor<SqlServerRetryOptions>>();

                var connectionString = configuration.GetConnectionString("ConnectionStrings");
                Console.WriteLine($"Connection String: {connectionString}");

                optionsBuilder
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .UseLazyLoadingProxies() // => If UseLazyLoadingProxies, all of the navigation properties should be VIRTUAL.
                .UseSqlServer(
                    connectionString: connectionString,
                    sqlServerOptionsAction: optionsBuilder =>
                       optionsBuilder.ExecutionStrategy(
                       dependencies => new SqlServerRetryingExecutionStrategy(
                       dependencies: dependencies,
                       maxRetryCount: options.CurrentValue.MaxRetryCount,
                       maxRetryDelay: options.CurrentValue.MaxRetryDelay,
                       errorNumbersToAdd: options.CurrentValue.ErrorNumbersToAdd))
                       .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.GetName().Name));
            });

            services.AddIdentityCore<AppUser>()
                    .AddRoles<AppRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
               options.Lockout.AllowedForNewUsers = true;
               options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2); //default 5
               options.Lockout.MaxFailedAccessAttempts = 3; //default 5
               options.Password.RequireDigit = false;
               options.Password.RequireLowercase = false;
               options.Password.RequireNonAlphanumeric = false;
               options.Password.RequireUppercase = false;
               options.Password.RequiredLength = 8;
               options.Password.RequiredUniqueChars = 1;
            });

            return services;
        }

        public static OptionsBuilder<SqlServerRetryOptions> ConfigureSqlServerRetryOption(this IServiceCollection services, IConfigurationSection section)
        {
            return services.AddOptions<SqlServerRetryOptions>()
                .Bind(section)
                .ValidateDataAnnotations()
                .ValidateOnStart();
        }

        public static IServiceCollection AddRepositoryBaseConfiguration(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>));
            services.AddTransient<IUnitOfWork, EFUnitOfWork>();

            return services;
        }
    }
}
