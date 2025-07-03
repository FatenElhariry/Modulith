using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EShop.Basket.Basket.JsonConverters;

public class ShoppingCartConverter : JsonConverter<ShoppingCart>
{
    public override ShoppingCart? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var jsonDocument = JsonDocument.ParseValue(ref reader);
        var rootElement = jsonDocument.RootElement;

        var id = rootElement.GetProperty(nameof(ShoppingCart.Id)).GetGuid();
        var userName = rootElement.GetProperty(nameof(ShoppingCart.UserName)).GetString()!;
        var itemsElement = rootElement.GetProperty(nameof(ShoppingCart.Items));

        var shoppingCart = ShoppingCart.Create(id, userName);

        var items = itemsElement.Deserialize<List<ShoppingCartItem>>(options);
        if (items != null)
        {
            var itemsField = typeof(ShoppingCart).GetField("_items", BindingFlags.NonPublic | BindingFlags.Instance);
            itemsField?.SetValue(shoppingCart, items);
        }

        return shoppingCart;
    }

    public override void Write(Utf8JsonWriter writer, ShoppingCart value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WriteString(nameof(ShoppingCart.Id), value.Id.ToString());
        writer.WriteString(nameof(ShoppingCart.UserName), value.UserName);

        writer.WritePropertyName(nameof(ShoppingCart.Items));
        JsonSerializer.Serialize(writer, value.Items, options);

        writer.WriteEndObject();
    }
}
