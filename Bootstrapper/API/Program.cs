using API.Application;
using API.Application.Middlewares;
using Carter;
using EShop.Shared.Exceptions.Handlers;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Scalar.AspNetCore;
using Serilog;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddOpenApi();

            var configuration = builder.Configuration;

            var logger = builder.Logging.Services.BuildServiceProvider().GetRequiredService<ILogger<Program>>();

            builder.Host.UseSerilog((context, config) =>
            {
                config.ReadFrom.Configuration(context.Configuration);
                //config.Enrich.WithProperty("Application", "EShop.API");
                //config.Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName);
                
                //config.WriteTo.Console(Serilog.Events.LogEventLevel.Information, "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] [{CorrelationId}] {Message:lj}{NewLine}{Exception}");
                //config.WriteTo.MSSqlServer(configuration.GetConnectionString("DefaultConnection"),
                //    configuration.GetValue<MSSqlServerSinkOptions>));
                // config.WriteTo.MySink(); // Custom sink to override messages
            });
            //builder.Logging.ClearProviders();
            //builder.Logging.AddConsole();
            //builder.Host.UseDefaultServiceProvider


            builder.Services.InjectModules(configuration, logger);

            builder.Services.AddExceptionHandler<CustomExceptionHandler>();


            var app = builder.Build();

            app.UseMiddleware<CorrelationIdMiddleware>();
            app.UseSerilogRequestLogging();
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.AddApplicationModules(configuration);
            app.MapCarter(); // Ensure Carter is mapped before injecting middlewares

            app.MapGet("/", () => "Hello World!");
            app.UseExceptionHandler(exceptionHandlerApp =>
            {
                return;
                exceptionHandlerApp.Run(async context =>
                {
                    var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

                    if (exception == null) return;

                    var problemDetails = new ProblemDetails
                    {
                        Title = "An error occurred",
                        Detail = exception.Message,
                        Status = 500,
                        Type = "https://httpstatuses.org/500"
                    };

                    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
                    logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);

                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/problem+json";

                    await context.Response.WriteAsJsonAsync(problemDetails);
                });

            });
            app.Run();
        }
    }
}
