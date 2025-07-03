using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Eshop.Shared.Messaging.Extensions;
public static class MassTransitExtensions
{
    public static IServiceCollection AddMassTransitExtensions(this IServiceCollection services, IConfiguration configuration, params Assembly[] assemblies)
    {
        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();

            x.SetInMemorySagaRepositoryProvider();

            x.AddConsumers(assemblies);

            x.AddSagaStateMachines(assemblies);

            x.AddSagas(assemblies);

            x.AddActivities(assemblies);

            //x.UsingInMemory((context, cfg) =>
            //{
            //    cfg.ConfigureEndpoints(context);
            //});


            x.UsingRabbitMq((context, config) =>
            {
                config.Host(new Uri(configuration["MessageBroker:Host"]!), host =>
                {
                    host.Username(configuration["MessgeBroker:UserName"]!);
                    host.Password(configuration["MessgeBroker:Password"]!);
                });
                config.ConfigureEndpoints(context);
            });
        });
        return services;
    }
}