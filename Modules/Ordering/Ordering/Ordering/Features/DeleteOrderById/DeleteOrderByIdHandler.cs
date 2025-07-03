using EShop.Ordering.Data;
using EShop.Shared.Contract.CQRS;
using EShop.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace EShop.Ordering.Ordering.Features.DeleteOrderById;

public record DeleteOrderByIdCommand(Guid Id): ICommand<DeleteOrderByIdResult>;

public record DeleteOrderByIdResult(bool Success);

public class DeleteOrderByIdHandler(OrderingDbContext dbContext) : ICommandHandler<DeleteOrderByIdCommand, DeleteOrderByIdResult>
{
    public async Task<DeleteOrderByIdResult> Handle(DeleteOrderByIdCommand command, CancellationToken cancellationToken)
    {
        var entity = await dbContext.
            Orders.
            Include(x => x.Items).
            SingleOrDefaultAsync(x => x.Id == command.Id);

        if (entity == null)
            throw new NotFoundException("Order", command.Id);

        dbContext.Orders.Remove(entity);
        return new DeleteOrderByIdResult(true);
    }
}
