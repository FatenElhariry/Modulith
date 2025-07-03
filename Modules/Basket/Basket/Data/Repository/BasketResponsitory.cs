

namespace EShop.Basket.Data.Repository;
public class BasketResponsitory( BasketDbContext dbContext) : IBasketRepository
{
    public async Task<ShoppingCart> CreateBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        dbContext.ShoppingCarts.Add(basket);

        await dbContext.SaveChangesAsync();

        return basket;
    }

    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
    {
        var query = await dbContext.ShoppingCarts.Include(x => x.Items).
            FirstOrDefaultAsync(x => x.UserName == userName, cancellationToken);

        if (query == null) return false;

        dbContext.ShoppingCarts.Remove(query);

        return (await dbContext.SaveChangesAsync(cancellationToken)) > 0;
    }

    public async Task<ShoppingCart> GetBasket(string userName, bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        var query = dbContext.ShoppingCarts.Include(x => x.Items);
        if (!asNoTracking)
            query.AsNoTracking();
        
        return await query.FirstOrDefaultAsync(x => x.UserName == userName, cancellationToken);
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}

