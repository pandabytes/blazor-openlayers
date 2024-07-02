namespace Blazor.OpenLayers.Components.Overlay;

public sealed record OverlayOptions
{
  public Coordinate? Position { get; init; }

  public Positioning? Positioning { get; init; }

  public bool? StopEvent { get; init; }
}
