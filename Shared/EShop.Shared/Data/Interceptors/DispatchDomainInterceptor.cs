﻿using EShop.Shared.DDD;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EShop.Shared.Data.Interceptors;

public class DispatchDomainInterceptor: SaveChangesInterceptor
{
    private readonly IMediator _mediator;
    public DispatchDomainInterceptor(IMediator mediator)
    {
        _mediator = mediator;
    }
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();    
        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        await DispatchDomainEvents(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private async Task DispatchDomainEvents(DbContext? context)
    {
        if (context == null) return;

        var aggregates = context.ChangeTracker.Entries<IAggregate<Guid>>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity);

        var domainEvents = aggregates.SelectMany(x => x.DomainEvents).ToList();

        aggregates.ToList().ForEach(aggregate => aggregate.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
        {
            await _mediator.Publish(domainEvent);
        }

    }
}
