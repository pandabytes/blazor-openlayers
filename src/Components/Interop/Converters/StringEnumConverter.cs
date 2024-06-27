using System.Text.Json;
using System.Text.Json.Serialization;

namespace Blazor.OpenLayers.Components.Interop.Converters;

internal sealed class StringEnumConverter<TEnum> : JsonConverter<TEnum?> where TEnum : StringEnum
{
  public override TEnum? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    var value = reader.GetString();
    return (value is null) ? null : StringEnum.Get<TEnum>(value);
  }

  public override void Write(Utf8JsonWriter writer, TEnum? value, JsonSerializerOptions options)
  {
    if (value is null)
    {
      writer.WriteNullValue();
      return;
    }

    writer.WriteStringValue(value.Value);
  }

  public override void WriteAsPropertyName(Utf8JsonWriter writer, TEnum? value, JsonSerializerOptions options)
  {
    if (value is null)
    {
      return;
    }

    writer.WritePropertyName(value.Value);
  }

  public override TEnum? ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    => Read(ref reader, typeToConvert, options);
}
