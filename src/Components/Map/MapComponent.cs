namespace Blazor.OpenLayers.Components.Map;

public partial class MapComponent : BaseScopeComponent
{
  private readonly IDictionary<string, IDictionary<string, MapSubComponent>> _subComponents = 
    new Dictionary<string, IDictionary<string, MapSubComponent>>();

  [InjectScope, AutoImportJsModule]
  private OpenLayersInteropModule OpenLayersInterop { get; init; } = null!;

  [Parameter, EditorRequired]
  public string Id { get; init; } = string.Empty;

  [Parameter, EditorRequired]
  public MapOptions Options { get; init; } = null!;

  [Parameter]
  public string Class { get; init; } = string.Empty;

  [Parameter]
  public string Style { get; init; } = "width: 100%; height: 400px";

  [Parameter]
  public RenderFragment? Overlays { get; set; }

  /// <inheritdoc />
  protected override async Task OnAfterRenderAsync(bool firstRender)
  {
    await base.OnAfterRenderAsync(firstRender);
    
    if (firstRender)
    {
      var overlayOpts = GetSubComponents<OverlayComponent>()
        .ToDictionary(p => p.Key, p => p.Value.Options);

      await OpenLayersInterop.CreateMapAsync(Id, Options, overlayOpts);
    }
  }

  /// <inheritdoc />
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

  /// <inheritdoc />
  protected async override ValueTask DisposeAsyncCore()
  {
    await OpenLayersInterop.RemoveMapAsync(Id);
    await base.DisposeAsyncCore();
  }

  internal void AddSubComponent(MapSubComponent subComponent)
  {
    var typeName = subComponent.GetType().FullName!;
    if (!_subComponents.ContainsKey(typeName))
    {
      _subComponents.Add(typeName, new Dictionary<string, MapSubComponent>());
    }

    _subComponents[typeName].TryAdd(subComponent.CompositeId, subComponent);
  }

  internal void RemoveSubComponent(MapSubComponent subComponent)
  {
    var typeName = subComponent.GetType().FullName!;
    if (!_subComponents.ContainsKey(typeName))
    {
      return;
    }
    _subComponents[typeName].Remove(subComponent.CompositeId);
  }

  private IDictionary<string, TComp> GetSubComponents<TComp>() where TComp : MapSubComponent
  {
    var typeName = typeof(TComp).FullName!;
    if (!_subComponents.TryGetValue(typeName, out var components))
    {
      return new Dictionary<string, TComp>();
    }

    return components.ToDictionary(pair => pair.Key, pair => (TComp)pair.Value);
  }
}
