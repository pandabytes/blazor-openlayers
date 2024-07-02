using System.Text.Json;

namespace Blazor.OpenLayers.Coordinates;

internal sealed class CoordinateConverter : JsonConverter<Coordinate?>
{
  public override Coordinate? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    var numbers = JsonSerializer.Deserialize<double[]>(ref reader, options) ??
      throw new JsonException($"Failed to parse {reader.GetString()} to {nameof(Coordinate)} object.");
    
    if (numbers.Length != 2)
    {
      throw new JsonException("Expected array to only have 2 number values.");
    }

    return new Coordinate(numbers[0], numbers[1]);
  }

  public override void Write(Utf8JsonWriter writer, Coordinate? value, JsonSerializerOptions options)
  {
    if (value is null)
    {
      writer.WriteNullValue();
      return;
    }

    writer.WriteStartArray();
    writer.WriteNumberValue(value.Longitude);
    writer.WriteNumberValue(value.Latitude);
    writer.WriteEndArray();
  }
}
