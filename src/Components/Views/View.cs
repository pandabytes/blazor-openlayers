namespace Blazor.OpenLayers.Components.Views;

public sealed record ViewOptions
{
  public Coordinate? Center { get; init; }

  public int? Zoom { get; init; }
}
