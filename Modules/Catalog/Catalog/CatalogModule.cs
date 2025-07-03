using EShop.Shared.Application.DI;
using EShop.Shared.Data.Interceptors;
using FluentValidation;

namespace Eshop.Catalog
{
    public class CatalogModule : IModuleRegister
    {
        public override IApplicationBuilder  InjectMiddlewares(IApplicationBuilder app, IConfiguration configuration)
        {
            base.InjectMiddlewares(app, configuration);
            initialiseDatabaseAsyc<CatalogDbContext>(app).GetAwaiter().GetResult();
            return app;
        }

        public override IServiceCollection  InjectServices(IServiceCollection services, IConfiguration configuration, ILogger logger)
        {
            base.InjectServices(services, configuration, logger);
            // application services - register application services, repositories, etc.
            
            // Data - interface services , repositories, etc.
            services.AddScoped<AuditableDataInterceptor>(); // Ensure you have the necessary using directives for EF Core
            services.AddScoped<DispatchDomainInterceptor>(); // Ensure you have the necessary using directives for EF Core



            // Data - register infrastructure services, repositories, etc.
            var connectionString = configuration.GetConnectionString("DefaultConnection"); // Replace with your actual connection string
            services.AddDbContext<CatalogDbContext>((sp , options) =>
            {
                options.UseSqlServer(connectionString);
                options.AddInterceptors(sp.GetRequiredService<AuditableDataInterceptor>()); // Ensure you have the necessary using directives for EF Core
                options.AddInterceptors(sp.GetRequiredService<DispatchDomainInterceptor>()); // Ensure you have the necessary using directives for EF Core
            }); // Ensure you have the necessary using directives for EF Core

            return services;
        }
      
    }
}
