
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EShop.Basket.Data.JsonConverters;
public class ShoppingCartItemConverter : JsonConverter<ShoppingCartItem>
{
    public override ShoppingCartItem? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var jsonDocument = JsonDocument.ParseValue(ref reader);
        var root = jsonDocument.RootElement;
        var item = new ShoppingCartItem(root.GetProperty("id").GetGuid(),
            root.GetProperty("shoppingCartId").GetGuid(),
            root.GetProperty("productId").GetGuid(),
            root.GetProperty("productName").GetString(),
            root.GetProperty("price").GetDecimal(),
            root.GetProperty("quantity").GetInt32(),
            root.GetProperty("color").GetString());

        return item;
    }

    public override void Write(Utf8JsonWriter writer, ShoppingCartItem value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WriteString("id", value.Id);
        writer.WriteString("shoppingCartId", value.ShoppingCartId);
        writer.WriteString("productId", value.ProductId);
        writer.WriteString("price", value.Price.ToString());
        writer.WriteString("id", value.Id);
        writer.WriteString("id", value.Id);

        writer.WriteEndObject();
    }
}
