using EShop.Basket.Basket.JsonConverters;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EShop.Basket.Data.Repository;
public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache) : IBasketRepository
{
    private readonly JsonSerializerOptions _options = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters = { new ShoppingCartConverter(), new ShoppingCartItemConverter() }
    };

    public async Task<ShoppingCart> GetBasket(string userName, bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        if (!asNoTracking)
            return await repository.GetBasket(userName, asNoTracking, cancellationToken);

        var cachedBasket = await cache.GetStringAsync(userName, cancellationToken);
        if (cachedBasket != null)
            return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket, _options);
        
        var basket = await repository.GetBasket(userName, asNoTracking, cancellationToken);
        if (basket != null)
            await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);
        
        return basket ?? ShoppingCart.Create(Guid.NewGuid(), userName);
    }
    public Task<ShoppingCart> CreateBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        return repository.CreateBasket(basket, cancellationToken);
    }

    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
    {
        await repository.DeleteBasket(userName, cancellationToken);

        await cache.RemoveAsync(userName, cancellationToken);

        return true;
    }


    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return repository.SaveChangesAsync(cancellationToken);
    }
}

