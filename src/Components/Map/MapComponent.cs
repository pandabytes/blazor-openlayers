namespace Blazor.OpenLayers.Components.Map;

public partial class MapComponent : BaseScopeComponent
{
  private readonly IDictionary<string, OverlayComponent> _overlays = new Dictionary<string, OverlayComponent>();

  [InjectScope, AutoImportJsModule]
  private OpenLayersInteropModule OpenLayersInterop { get; init; } = null!;

  [Parameter, EditorRequired]
  public string Id { get; init; } = string.Empty;

  [Parameter, EditorRequired]
  public MapOptions Options { get; init; } = null!;

  [Parameter]
  public string Style { get; init; } = "width: 100%; height: 400px";

  [Parameter]
  public RenderFragment? Overlays { get; set; }

  protected override async Task OnAfterRenderAsync(bool firstRender)
  {
    await base.OnAfterRenderAsync(firstRender);
    
    if (firstRender)
    {
      var overlaysOpts = _overlays.ToDictionary(p => p.Key, p => p.Value.Options );
      var options = Options with { Overlays = overlaysOpts };
      await OpenLayersInterop.CreateMapAsync(Id, options);
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

  internal void AddOverlay(OverlayComponent overlay)
  {
    _overlays.Add(overlay.CompositeId, overlay);
  }

  internal void RemoveOverlay(OverlayComponent overlay)
  {
    _overlays.Remove(overlay.CompositeId);
  }
}
