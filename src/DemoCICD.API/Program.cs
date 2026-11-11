
using Asp.Versioning;
using DemoCICD.API.DependencyInjection.Extensions;
using DemoCICD.API.Middleware;
using DemoCICD.Application.DependencyInjection.Extensions;
using DemoCICD.Persistence.DependencyInjection.Extensions;
using DemoCICD.Persistence.DependencyInjection.Options;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Serilog;

namespace DemoCICD.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // configure serilog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

            builder.Logging.ClearProviders().AddSerilog();
            builder.Host.UseSerilog();


            //add configuration for the application
            builder.Services.AddConfigureApplication();

            // Configure Options and SQL
            builder.Services.ConfigureSqlServerRetryOption(builder.Configuration.GetSection(nameof(SqlServerRetryOptions)));
            builder.Services.AddConfigurePersistenceSqlServer();
            builder.Services.AddRepositoryBaseConfiguration();

            //Api
            builder.Services.AddControllers().AddApplicationPart(DemoCICD.Presentation.AssemblyReference.Assembly);

            builder.Services.AddTransient<ExceptionHandlingMiddleware>();

            // Configure API versioning first
            builder.Services
                .AddSwaggerGenNewtonsoftSupport()
                .AddFluentValidationRulesToSwagger()
                .AddEndpointsApiExplorer()
                .AddSwagger();

            builder.Services
                .AddApiVersioning(options => 
                {
                    // Default API version
                    options.DefaultApiVersion = new ApiVersion(1, 0);

                    // Require clients to specify the API version
                    options.AssumeDefaultVersionWhenUnspecified = false;

                    // Report supported API versions
                    options.ReportApiVersions = true;

                    // URL segment versioning
                    options.ApiVersionReader = new UrlSegmentApiVersionReader();
                })
                .AddApiExplorer(options =>
                {
                    // Swagger document names: v1, v2, v3
                    options.GroupNameFormat = "'v'VVV";

                    // Replace {version:apiVersion} with actual version
                    options.SubstituteApiVersionInUrl = true;
                });

            var app = builder.Build();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            if (builder.Environment.IsDevelopment() || builder.Environment.IsStaging())
            {
                app.ConfigureSwagger();
            }

            try
            {
                await app.RunAsync();
                Log.Information("Stop Cleanly");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "An unhanded exception occurred during bootstrapping");
                await app.StopAsync();
            }
            finally
            {
                Log.CloseAndFlush();
                await app.DisposeAsync();
            }
        }
    }
}
