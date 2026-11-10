
using DemoCICD.API.DependencyInjection.Extensions;
using DemoCICD.Application.DependencyInjection.Extensions;
using DemoCICD.Persistence.DependencyInjection.Extensions;
using DemoCICD.Persistence.DependencyInjection.Options;
using Serilog;

namespace DemoCICD.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure API versioning first
            builder.Services.AddApiVersioning().AddApiExplorer();

            //builder.Services.AddSwaggerGen();
            builder.Services.AddSwagger();

            // configure serilog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .CreateLogger();

            builder.Logging.ClearProviders().AddSerilog();
            builder.Host.UseSerilog();

            //add configuration for the application
            builder.Services.AddConfigureApplication();

            builder.Services.ConfigureSqlServerRetryOption(builder.Configuration.GetSection(nameof(SqlServerRetryOptions)));
            builder.Services.AddConfigurePersistenceSqlServer();
            builder.Services.AddRepositoryBaseConfiguration();

            //Api
            builder.Services.AddControllers().AddApplicationPart(DemoCICD.Presentation.AssemblyReference.Assembly);


            var app = builder.Build();

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
