namespace Blazor.OpenLayers.Components;

public partial class MapComponent : BaseScopeComponent
{
  [InjectScope, AutoImportJsModule]
  private readonly OpenLayersInteropModule _openLayersInteropModule = null!;

  [Parameter, EditorRequired]
  public string Id { get; init; } = string.Empty;

  [Parameter, EditorRequired]
  public MapOptions Options { get; init; } = null!;

  [Parameter]
  public string Style { get; init; } = "width: 100%; height: 400px";

  protected override async Task OnAfterRenderAsync(bool firstRender)
  {
    await base.OnAfterRenderAsync(firstRender);
    
    if (firstRender)
    {
      await _openLayersInteropModule.CreateMapAsync(Id, Options);
    }
  }

  protected override void OnParametersSet()
  {
    base.OnParametersSet();

    if (string.IsNullOrWhiteSpace(Id))
    {
      throw new ArgumentException($"{nameof(Id)} cannot be null or empty.");
    }

    if (Options is null)
    {
      throw new ArgumentException($"{nameof(Options)} cannot be null.");
    }
  }
}
