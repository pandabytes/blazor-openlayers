using System.Reflection;
using System.Text.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace Blazor.OpenLayers;

/// <summary>
/// Provide dependency injection methods to
/// setup this library.
/// </summary>
public static class DependencyInjection
{
  /// <summary>
  /// Register FamilyTreeJS dependencies.
  /// </summary>
  public static IServiceCollection AddOpenLayers(this IServiceCollection services)
  {
    return services
      .AddScoped<OpenLayersInteropModule>();
  }

  /// <summary>
  /// Configure the <see cref="IJSRuntime"/>'s JSON options
  /// to not serialize "null" values. This will affect globally.
  /// </summary>
  public static WebAssemblyHost ConfigureIJSRuntimeJsonOptions(this WebAssemblyHost webHost)
  {
    var jsRuntime = webHost.Services.GetRequiredService<IJSRuntime>();
    var options = GetJsonSerializerOptions(jsRuntime);

    options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    return webHost;
  }

  /// <summary>
  /// See https://github.com/dotnet/aspnetcore/issues/12685#issuecomment-603050776
  /// </summary>
  /// <param name="jsRuntime"></param>
  /// <exception cref="ArgumentException"></exception>
  private static JsonSerializerOptions GetJsonSerializerOptions(IJSRuntime jsRuntime)
  {
    var property = jsRuntime.GetType().GetProperty("JsonSerializerOptions", BindingFlags.NonPublic | BindingFlags.Instance);
    if (property?.GetValue(jsRuntime, null) is not JsonSerializerOptions options)
    {
      throw new ArgumentException($"Unable to get {nameof(JsonSerializerOptions)} from {nameof(IJSRuntime)}.");
    }
    return options;
  }
}
