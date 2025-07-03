using EShop.ModuleBuilder.Application.DI;
using EShop.Ordering.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EShop.Ordering;

public class OrderingModule : IModuleRegister
{
    public override IApplicationBuilder InjectMiddlewares(IApplicationBuilder app, IConfiguration configuration)
    {
        return app;
    }
    public override IServiceCollection InjectServices(IServiceCollection services, IConfiguration configuration, ILogger logger)
    {
        logger.LogInformation("Injecting OrderingModule DbContext services.");
        services.AddDbContext<OrderingDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(OrderingDbContext).Assembly.FullName);
            });
        });


        return services;
    }
}
