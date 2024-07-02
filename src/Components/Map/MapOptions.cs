namespace Blazor.OpenLayers.Components.Map;

public sealed record MapOptions
{
  public required ViewOptions ViewOptions { get; init; }

  public required IReadOnlyDictionary<LayerType, LayerSource> Layers { get; init; }
}
