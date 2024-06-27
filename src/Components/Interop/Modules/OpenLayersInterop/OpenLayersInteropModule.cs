using Microsoft.JSInterop;

namespace Blazor.OpenLayers.Components.Interop.Modules.OpenLayersInterop;

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
      nameof(Components),
      nameof(Interop),
      nameof(Modules),
      nameof(OpenLayersInterop),
      "open-layers-interop.js",
    };
    ModulePath = string.Join('/', pathComponents);
  }

  public async Task CreateMapAsync(string mapId, MapOptions mapOptions)
  {
    await Module.InvokeVoidAsync($"{OpenLayersInteropObj}.createMap", mapId, mapOptions);
  }
}
