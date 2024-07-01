namespace Blazor.OpenLayers.Components.Interop.Modules.OpenLayersInterop;

public class BaseEventSlim
{
  public string MapId { get; init; } = string.Empty;
}

public class MapEventSlim : BaseEventSlim
{
  public string Type { get; init; } = string.Empty;
}

public class MapBrowserEventSlim : MapEventSlim
{
  public Coordinate Coordinate { get; init; } = null!;

  public bool Dragging { get; init; }
}
