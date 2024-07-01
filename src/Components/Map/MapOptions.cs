namespace Blazor.OpenLayers.Components.Map;

public sealed record MapOptions
{
  public ViewOptions? ViewOptions { get; init; }

  public IReadOnlyDictionary<LayerType, LayerSource>? Layers { get; init; }

  public IReadOnlyDictionary<string, OverlayOptions> Overlays { get; init;} = new Dictionary<string, OverlayOptions>();
}
