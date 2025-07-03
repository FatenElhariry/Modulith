using EShop.Ordering.Data;
using EShop.Shared.Contract.CQRS;
using EShop.Shared.DDD;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.Orders.Dtos;

namespace EShop.Ordering.Ordering.Features.GetOrderById;

public record GetOrderByIdQuery(Guid Id): ICommand<GetOrderByIdResult>;
public record GetOrderByIdResult(OrderDto Order);

public class GetOrderByIdQueryValidation: AbstractValidator<GetOrderByIdQuery>
{
    public GetOrderByIdQueryValidation()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can't be empty.");
    }
}

public class GetOrderByIdHandler(OrderingDbContext dbContext) : ICommandHandler<GetOrderByIdQuery, GetOrderByIdResult>
{
    async Task<GetOrderByIdResult> IRequestHandler<GetOrderByIdQuery, GetOrderByIdResult>.Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
    {
        var order = await dbContext.
            Orders.
            Include(x => x.Items).
            FirstOrDefaultAsync(x => x.Id == query.Id);

        

        return new GetOrderByIdResult(order.Adapt<OrderDto>());
    }
}
