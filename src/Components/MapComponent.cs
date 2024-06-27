namespace Blazor.OpenLayers.Components;

public partial class MapComponent : BaseScopeComponent
{
  [InjectScope, AutoImportJsModule]
  private readonly OpenLayersInteropModule _openLayersInteropModule = null!;

  [Parameter, EditorRequired]
  public string Id { get; init; } = string.Empty;

  [Parameter]
  public string Style { get; init; } = "width: 100%; height: 400px";

  protected override async Task OnAfterRenderAsync(bool firstRender)
  {
    await base.OnAfterRenderAsync(firstRender);
    
    if (firstRender)
    {
      await _openLayersInteropModule.CreateMapAsync(Id, new()
      {
        Target = Id,
        ViewOptions = new() { Center = new(0, 0), Zoom = 2 },
        Layers = new Dictionary<LayerType, LayerSource>
        {
          { LayerType.Tile, LayerSource.OSM }
        }
      });
    }
  }
}
