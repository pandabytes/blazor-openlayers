namespace Blazor.OpenLayers.Components.Map;

public sealed record MapOptions
{
  public ViewOptions? ViewOptions { get; init; }

  public IReadOnlyDictionary<LayerType, LayerSource>? Layers { get; init; }

  public IReadOnlyDictionary<string, OverlayOptions> Overlays { get; internal init; } = new Dictionary<string, OverlayOptions>();
}
