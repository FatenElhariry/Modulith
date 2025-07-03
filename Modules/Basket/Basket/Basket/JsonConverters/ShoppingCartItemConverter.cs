using System.Text.Json;
using System.Text.Json.Serialization;

namespace EShop.Basket.Basket.JsonConverters;

public class ShoppingCartItemConverter : JsonConverter<ShoppingCartItem>
{
    public override ShoppingCartItem? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var jsonDocument = JsonDocument.ParseValue(ref reader);
        var rootElement = jsonDocument.RootElement;

        var id = rootElement.GetProperty(nameof(ShoppingCartItem.Id)).GetGuid();
        var shoppingCartId = rootElement.GetProperty(nameof(ShoppingCartItem.ShoppingCartId)).GetGuid();
        var productId = rootElement.GetProperty(nameof(ShoppingCartItem.ProductId)).GetGuid();
        var quantity = rootElement.GetProperty(nameof(ShoppingCartItem.Quantity)).GetInt32();
        var color = rootElement.GetProperty(nameof(ShoppingCartItem.Color)).GetString()!;
        var price = rootElement.GetProperty(nameof(ShoppingCartItem.Price)).GetDecimal();
        var productName = rootElement.GetProperty(nameof(ShoppingCartItem.ProductName)).GetString()!;

        return new ShoppingCartItem(id, shoppingCartId, productId,productName, price, quantity, color);
    }

    public override void Write(Utf8JsonWriter writer, ShoppingCartItem value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WriteString(nameof(ShoppingCartItem.Id), value.Id.ToString());
        writer.WriteString(nameof(ShoppingCartItem.ShoppingCartId), value.ShoppingCartId.ToString());
        writer.WriteString(nameof(ShoppingCartItem.ProductId), value.ProductId.ToString());
        writer.WriteNumber(nameof(ShoppingCartItem.Quantity), value.Quantity);
        writer.WriteString(nameof(ShoppingCartItem.Color), value.Color);
        writer.WriteNumber(nameof(ShoppingCartItem.Price), value.Price);
        writer.WriteString(nameof(ShoppingCartItem.ProductName), value.ProductName);

        writer.WriteEndObject();
    }
}
