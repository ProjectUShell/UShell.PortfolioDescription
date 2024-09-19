namespace UShell {
  public class ApplicationScopeEntry {
    public string Label { get; set; } = string.Empty;
    public object Value { get; set; } = null;
    public bool IsVisible { get; set; } = false;
    public string SwitchScopeCommand { get; set; } = null;
  }
}
