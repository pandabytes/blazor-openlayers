namespace Blazor.OpenLayers.Coordinates;

[JsonConverter(typeof(CoordinateConverter))]
public sealed record Coordinate(double Longitude, double Latitude);
