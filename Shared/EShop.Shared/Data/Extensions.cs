using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Shared.Data
{
    public static class Extensions
    {
        public static IApplicationBuilder UseMigration<TContext>(this IApplicationBuilder app)
            where TContext : DbContext
        {
            // app.ApplicationServices
            // This method can be used to apply migrations at application startup
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TContext>();
                 context.Database.MigrateAsync().GetAwaiter().GetResult();
            }
            return app;
        }
    }
}
