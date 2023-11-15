namespace System {
  public class StaticUsecaseAssignment {
    public string UsecaseKey { get; set; } = "";
    public string TargetWorkspaceKey { get; set; } = "";
    public IDynamicParamObject InitUnitOfWork { get; set; }
  }
}