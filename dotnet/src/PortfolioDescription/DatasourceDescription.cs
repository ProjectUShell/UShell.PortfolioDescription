using System.Collections.Generic;

namespace System {
  public class DatasourceDescription {
    public string DatasourceUid { get; set; } = "";
    public string ProviderClass { get; set; } = "";
    public List<IDynamicParamObject> ProviderArguments { get; set; } = new List<IDynamicParamObject>();
    public string EntityName { get; set; } = "";
  }
}