
using DemoCICD.Application.Behaviors;
using FluentValidation;
using MediatR;

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
            // Register MediatR and tell it to scan the current assembly for handlers
            builder.Services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(DemoCICD.Application.AssemblyReference.Assembly));
            builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipeLineBehavior<,>));

            //add fluent validation services
            builder.Services.AddValidatorsFromAssembly(DemoCICD.Application.AssemblyReference.Assembly, includeInternalTypes: true);


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
