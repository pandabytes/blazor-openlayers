using Microsoft.JSInterop;

namespace Blazor.OpenLayers.Components.Interop.Modules.OpenLayersInterop;

internal sealed class OpenLayersInteropModule : BaseJsModule
{
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

}
