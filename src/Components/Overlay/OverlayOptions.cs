namespace Blazor.OpenLayers.Components.Overlay;

public sealed record OverlayOptions
{
  public required string ElementId { get; init; } = string.Empty;

  public Coordinate? Position { get; init; }
}
