namespace Blazor.OpenLayers.Components.Interop.Options;

public sealed class MapOptions
{
  public required string Target { get; init; }

  public ViewOptions? ViewOptions { get; init; }

  public IReadOnlyDictionary<LayerType, LayerSource>? Layers { get; init; }
}
