using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace EShop.Basket.Basket.Processor;
public class OutboxProcessor(IServiceProvider serviceProvider, IBus bus, ILogger<OutboxProcessor> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (stoppingToken.IsCancellationRequested)
        {
			try
			{
                using var scope = serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<BasketDbContext>();

                var outboxMessages = await dbContext.
                    OutboxMessages.
                    Where(x => x.ProcessedOn == null).
                    ToListAsync(stoppingToken);

                foreach (var outboxMessage in outboxMessages)
                {

                    var eventType = Type.GetType(outboxMessage.Type);

                    if (eventType == null)
                    {
                        logger.LogInformation("Can't resolve type {Type}", outboxMessage.Type);
                        continue;
                    }

                    var eventMessage = JsonSerializer.Deserialize(outboxMessage.Content, eventType);

                    if (eventMessage == null)
                    {
                        logger.LogInformation("Can't deserialize the content of the message {Content}", outboxMessage.Content);
                        continue;
                    }

                    await bus.Publish(eventMessage, stoppingToken);

                    outboxMessage.ProcessedOn = DateTime.UtcNow;

                }
                await dbContext.SaveChangesAsync(stoppingToken);
            }
			catch (Exception)
			{

				throw;
			}

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }


    }
}
