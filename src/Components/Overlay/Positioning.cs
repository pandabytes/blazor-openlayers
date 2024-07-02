namespace Blazor.OpenLayers.Components.Overlay;

[JsonConverter(typeof(StringEnumConverter<Positioning>))]
public sealed class Positioning : StringEnum
{
  private Positioning(string value) : base(value) {}

  public static readonly Positioning BottomLeft = new("bottom-left");

  public static readonly Positioning BottomCenter = new("bottom-center");

  public static readonly Positioning BottomRight = new("bottom-right");

  public static readonly Positioning CenterLeft = new("center-left");

  public static readonly Positioning CenterCenter = new("center-center");

  public static readonly Positioning CenterRight = new("center-right");

  public static readonly Positioning TopLeft = new("top-left");

  public static readonly Positioning TopCenter = new("top-center");

  public static readonly Positioning TopRight = new("top-right");
}
