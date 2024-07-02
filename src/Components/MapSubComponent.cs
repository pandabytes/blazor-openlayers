namespace Blazor.OpenLayers.Components;

public abstract class MapSubComponent : ComponentBase, IDisposable
{
  [Parameter, EditorRequired]
  public string Id { get; init; } = string.Empty;

  [Parameter]
  public string Style { get; init; } = string.Empty;

  [Parameter]
  public string Class { get; init; } = string.Empty;

  [CascadingParameter]
  protected MapComponent Map { get; init; } = null!;

  public virtual string CompositeId => $"{Map.Id}-{Id}";

  /// <inheritdoc />
  protected override void OnParametersSet()
  {
    base.OnParametersSet();

    if (string.IsNullOrWhiteSpace(Id))
    {
      throw new ArgumentException($"{nameof(Id)} must not be null or empty.");
    }

    if (Map is null)
    {
      throw new InvalidOperationException($"Expected value to be cascaded to parameter \"{nameof(Map)}\".");
    }
  }

  /// <inheritdoc />
  protected override void OnInitialized()
  {
    base.OnInitialized();
    Map.AddSubComponent(this);
  }

  /// <inheritdoc />
  public void Dispose()
  {
    Map.RemoveSubComponent(this);
  }
}
