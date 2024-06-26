namespace Blazor.OpenLayers.Components;

public partial class MapComponent : BaseScopeComponent
{
  [Parameter, EditorRequired]
  public string Id { get; init; } = string.Empty;

  [Parameter, EditorRequired]
  public string Style { get; init; } = string.Empty;
}
