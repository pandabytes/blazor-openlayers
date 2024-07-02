namespace Blazor.OpenLayers.Components.Map;

public sealed record MapOptions
{
  public required ViewOptions ViewOptions { get; init; }

  public required IReadOnlyDictionary<LayerType, LayerSource> Layers { get; init; }

  public IReadOnlyDictionary<string, OverlayOptions> Overlays { get; internal init; } = new Dictionary<string, OverlayOptions>();
}
