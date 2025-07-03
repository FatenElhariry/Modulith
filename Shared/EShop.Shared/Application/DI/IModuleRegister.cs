

using Carter;
using EShop.Shared.Application.DI;
using Microsoft.Extensions.Logging;

namespace EShop.ModuleBuilder.Application.DI
{
    public abstract class IModuleRegister
    {

        public virtual IServiceCollection InjectServices(IServiceCollection services, IConfiguration configuration, ILogger logger)
        {

            // application services - register application services, repositories, etc.
            
           

            return services;
        }

        public virtual IApplicationBuilder InjectMiddlewares(IApplicationBuilder app, IConfiguration configuration)
        {
            return app;
        }

        protected async Task initialiseDatabaseAsyc<TDbContext>(IApplicationBuilder app) where TDbContext : DbContext
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TDbContext>();
            await context.Database.MigrateAsync();
        }
    }
}
