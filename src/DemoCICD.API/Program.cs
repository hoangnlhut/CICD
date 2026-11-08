
using DemoCICD.Application.DependencyInjection.Extensions;
using DemoCICD.Persistence.DependencyInjection.Extensions;
using DemoCICD.Persistence.DependencyInjection.Options;
using Serilog;

namespace DemoCICD.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSwaggerGen();

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
 
            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();   // Serves the generated JSON document
                app.UseSwaggerUI(); // Serves the interactive web UI
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
