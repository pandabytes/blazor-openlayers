namespace Blazor.OpenLayers.Components.Overlay;

public sealed record PanOptions
{
  public double? Duration { get; init; }

  public FuncCallbackInterop<double, double>? Easing { get; init; }
}

public sealed record PanIntoViewOptions
{
  public PanOptions? Animation { get; init; }

  public double? Margin { get; init; }
}
