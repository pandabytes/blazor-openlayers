using System.Text.Json.Serialization;
using Blazor.OpenLayers.Components.Interop.Converters;

namespace Blazor.OpenLayers.Components.Interop.Options;

public sealed record Coordinate(double X, double Y);

public sealed class ViewOptions
{
  [JsonConverter(typeof(CoordinateConverter))]
  public Coordinate? Center { get; init; }

  public int? Zoom { get; init; }
}
