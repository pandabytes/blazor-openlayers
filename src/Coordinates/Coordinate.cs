namespace Blazor.OpenLayers.Coordinates;

[JsonConverter(typeof(CoordinateConverter))]
public sealed record Coordinate(double X, double Y);
