using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EShop.ModuleBuilder.Application.DI
{
    public abstract class IModuleRegister
    {
        public virtual IServiceCollection InjectServices(IServiceCollection services, IConfiguration configuration)
        {

            return services;
        }

        public virtual IApplicationBuilder InjectMiddlewares(IApplicationBuilder app, IConfiguration configuration)
        {
            return app;
        }
    }
}
