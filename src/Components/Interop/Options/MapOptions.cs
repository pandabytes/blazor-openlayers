namespace Blazor.OpenLayers.Components.Interop.Options;

public sealed class MapOptions
{
  public ViewOptions? ViewOptions { get; init; }

  public IReadOnlyDictionary<LayerType, LayerSource>? Layers { get; init; }
}
