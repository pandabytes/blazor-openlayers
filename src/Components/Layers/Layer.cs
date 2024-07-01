namespace Blazor.OpenLayers.Components.Layers;

[JsonConverter(typeof(StringEnumConverter<LayerType>))]
public sealed class LayerType : StringEnum
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
  private LayerType(string value) : base(value) {}

  public static readonly LayerType Tile = new("Tile");
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

[JsonConverter(typeof(StringEnumConverter<LayerSource>))]
public sealed class LayerSource : StringEnum
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
  private LayerSource(string value) : base(value) {}

  public static readonly LayerSource OSM = new("OSM");
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
