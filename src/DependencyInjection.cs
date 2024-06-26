using Microsoft.Extensions.DependencyInjection;

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
}
