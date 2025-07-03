using EShop.Ordering.Data;
using EShop.Shared.Contract.CQRS;
using MediatR;
using Ordering.Orders.Dtos;

namespace EShop.Ordering.Ordering.Features.GetOrders;

public record GetOrdersResult(IEnumerable<OrderDto> Orders);
public record GetOrdersQuery(int PageIndex, int PageSize): IQuery<GetOrdersResult>;


public class GetOrdersHandler(OrderingDbContext dbContext) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    Task<GetOrdersResult> IRequestHandler<GetOrdersQuery, GetOrdersResult>.Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        return null;
    }
}
