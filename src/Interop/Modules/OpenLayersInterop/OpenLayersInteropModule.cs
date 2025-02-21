using Microsoft.JSInterop;

namespace Blazor.OpenLayers.Interop.Modules.OpenLayersInterop;

internal sealed class OpenLayersInteropModule : BaseJsModule
{
  private const string OpenLayersInteropObj = "OpenLayersInteropObj";

  protected override string ModulePath { get; }

  public OpenLayersInteropModule(IJSRuntime jSRuntime) : base(jSRuntime)
  {
    var pathComponents = new string[]
    {
      ModulePrefixPath,
      "js",
      nameof(Interop),
      nameof(Modules),
      nameof(OpenLayersInterop),
      "open-layers-interop.js",
    };
    ModulePath = string.Join('/', pathComponents);
  }

  public async Task CreateMapAsync(string mapId, MapOptions mapOptions, IReadOnlyDictionary<string, OverlayOptions> overlays)
  {
    await Module.InvokeVoidAsync($"{OpenLayersInteropObj}.createMap", mapId, mapOptions, overlays);
  }

  public async Task RemoveMapAsync(string mapId)
    => await Module.InvokeVoidAsync($"{OpenLayersInteropObj}.removeMap", mapId);

  // public async Task RegisterAsync(string mapId, Action<MapBrowserEventSlim> handler)
  // {
  //   var callbackInterop = new ActionCallbackInterop<MapBrowserEventSlim>(handler);
  //   CallbackInterops.Add(callbackInterop);
  //   await Module.InvokeVoidAsync($"{OpenLayersInteropObj}.registerSingleClickHandler", mapId, callbackInterop);
  // }
}
