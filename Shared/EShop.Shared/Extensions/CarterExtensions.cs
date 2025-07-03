using System.Reflection;

namespace EShop.Shared.Extensions;

public static class CarterExtensions
{
    public static IServiceCollection AddCarterEndpointFromAssemblies(this IServiceCollection services, params Assembly[] assemblies)
    {

        return services;
    }
}
