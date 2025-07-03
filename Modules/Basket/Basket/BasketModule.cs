using EShop.Basket.Basket.Processor;
using EShop.Basket.Data.Repository;
using EShop.ModuleBuilder.Application.DI;
using EShop.Shared.Data.Interceptors;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EShop.Basket
{
    public class BasketModule : IModuleRegister
    {
        public override IServiceCollection InjectServices(IServiceCollection services, IConfiguration configuration, ILogger logger)
        {
             base.InjectServices(services, configuration, logger);
            // Data - interface services , repositories, etc.
            services.AddScoped<ISaveChangesInterceptor, AuditableDataInterceptor>(); // Ensure you have the necessary using directives for EF Core
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainInterceptor>(); // Ensure you have the necessary using directives for EF Core

            logger.LogInformation("Injecting BasketModule DbContext services.");
            services.AddDbContext<BasketDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>()); // Ensure you have the necessary using directives for EF Core

                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(BasketDbContext).Assembly.FullName);
                });
            });

            services.AddScoped<IBasketRepository, BasketResponsitory>();
            services.Decorate<IBasketRepository, CachedBasketRepository>();

            services.AddHostedService<OutboxProcessor>();
            //services.AddScoped<IBasketRepository>(provider =>
            //{
            //    var basketRepository = provider.GetRequiredService<BasketResponsitory>();
            //    return new CachedBasketRepository(basketRepository,
            //        provider.GetRequiredService<IDistributedCache>());
            //});

            return services;    

        }

        public override IApplicationBuilder InjectMiddlewares(IApplicationBuilder app, IConfiguration configuration)
        {
            base.InjectMiddlewares(app, configuration);
            return app;
        }

    }
}
