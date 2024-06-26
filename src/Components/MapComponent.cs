namespace Blazor.OpenLayers.Components;

public partial class MapComponent : BaseScopeComponent
{
  [InjectScope, AutoImportJsModule]
  private readonly OpenLayersInteropModule _openLayersInteropModule = null!;

  [Parameter, EditorRequired]
  public string Id { get; init; } = string.Empty;

  [Parameter]
  public string Style { get; init; } = string.Empty;

  protected override async Task OnInitializedAsync()
  {
    await base.OnInitializedAsync();

    System.Console.WriteLine(_openLayersInteropModule.ModuleStatus);
  }

  protected override async Task OnAfterRenderAsync(bool firstRender)
  {
    await base.OnAfterRenderAsync(firstRender);
    System.Console.WriteLine(_openLayersInteropModule.ModuleStatus);
  }
}
