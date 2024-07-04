namespace Blazor.OpenLayers.Components.Overlay;

public sealed record OverlayOptions
{
  public string? Id { get; init; }

  /// <summary>
  /// Id of the rendered overlay element on the DOM.
  /// </summary>
  //
  // Require upgrading to .NET 8 to serialize internal members.
  // For now, we expose the setter internally.
  public string? ElementId { get; internal init; }

  public IReadOnlyList<int>? Offset { get; init; }

  public Coordinate? Position { get; init; }

  public Positioning? Positioning { get; init; }

  public bool? StopEvent { get; init; }

  public bool? InsertFirst { get; init; }

  public PanIntoViewOptions? AutoPan { get; init; }

  public string? ClassName { get; init; }
}
