
using DemoCICD.Application.DependencyInjection.Extensions;

namespace DemoCICD.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            // Add Swagger/OpenAPI services
            builder.Services.AddSwaggerGen();

            //add configuration for the application
            builder.Services.AddConfigureApplication();


            var app = builder.Build();
 
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
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
