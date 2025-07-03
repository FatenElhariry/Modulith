using Carter;
using EShop.ModuleBuilder.Application.DI;
using EShop.Shared.Application.DI;
using FluentValidation;
using System.Reflection;
using Eshop.Shared.Messaging.Extensions;

namespace API.Application
{
    public static class Extensions
    {

        public static IServiceCollection InjectModules(this IServiceCollection services, IConfiguration configuration, ILogger logger)
        {
            var assemblies = Helper.GetApplicationAssemply();
            // Recursive call to ensure all modules are registered
            services.InjectCarterModules(configuration, assemblies.ToArray())
                .InjectMediatoR(configuration, assemblies.ToArray()) // Register MediatR with all assemblies
                .InjectFluentValidation(configuration, assemblies.ToArray()); // Register MediatR with all assemblies again to ensure all handlers are registered


            services.AddMassTransitExtensions(configuration, Helper.GetApplicationAssemply().ToArray());


            // Register StackExchange.Redis
            services.AddStackExchangeRedisCache(config =>
            {
                config.Configuration = configuration.GetConnectionString("RedisConnection");
            });

            foreach (var assembly in assemblies)
            {
                var moduleType = assembly.GetTypes()
                    .FirstOrDefault(t => typeof(IModuleRegister).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
                if (moduleType == null) continue; // Skip if no module type found
                var modulebuilder = Activator.CreateInstance(moduleType, false); // Ensure the interface is implemented
                if (modulebuilder is IModuleRegister moduleRegister)
                {
                    logger.LogInformation($"Injecting module: {moduleType.Name} from assembly: {assembly.GetName().Name}");
                    services = moduleRegister.InjectServices(services, configuration, logger);
                }
            }


            return services;
        }

        static IServiceCollection InjectFluentValidation(this IServiceCollection services, IConfiguration configuration, Assembly[] assemblies)
        {
            // Add FluentValidation configuration
            
            services.AddValidatorsFromAssemblies(assemblies);
            return services;
        }
        private static IServiceCollection InjectMediatoR(this IServiceCollection services, IConfiguration configuration, Assembly[] assemblies)
        {
            // Add MediatR configuration
            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssemblies(assemblies.ToArray());
                cfg.AddOpenBehavior(typeof(EShop.Shared.Behaviors.ValidationBehavior<,>));
                cfg.AddOpenBehavior(typeof(EShop.Shared.Behaviors.LoggingBehavior<,>));
            });
            return services;
        }

        private static IServiceCollection InjectCarterModules(this IServiceCollection services, IConfiguration configuration, Assembly[] assemblies)
        {
            var moduleTypes = assemblies.SelectMany(a => a.GetTypes())
                    .Where(t => typeof(ICarterModule).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                    .ToList();

            services.AddCarter(configurator: config =>
            {
                config.WithModules(moduleTypes.ToArray());
            });
            return services;
        }

        public static IApplicationBuilder AddApplicationModules(this IApplicationBuilder app, IConfiguration configuration)
        {
            var assemblies = Helper.GetApplicationAssemply();
            foreach (var assembly in assemblies)
            {
                var moduleType = assembly.GetTypes()
                    .FirstOrDefault(t => typeof(IModuleRegister).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
                if (moduleType == null) continue; // Skip if no module type found
                var modulebuilder = Activator.CreateInstance(moduleType, false); // Ensure the interface is implemented
                if (modulebuilder is IModuleRegister moduleRegister)
                {
                    app = moduleRegister.InjectMiddlewares(app, configuration);
                }
            }
            return app;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register application services here
            // Example: services.AddScoped<IMyService, MyService>();
            return services;
        }
        public static IApplicationBuilder UseApplicationMiddlewares(this IApplicationBuilder app)
        {
            // Configure application middlewares here
            // Example: app.UseMiddleware<MyMiddleware>();
            return app;
        }
    }
}
