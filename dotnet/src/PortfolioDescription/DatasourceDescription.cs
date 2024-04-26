using System.Collections.Generic;

namespace UShell {

  public class DatasourceDescription {
    public string DatasourceUid { get; set; } = "";
    public string ProviderClass { get; set; } = "";
    public List<IDynamicParamObject> ProviderArguments { get; set; } = new List<IDynamicParamObject>();
    public string EntityName { get; set; } = "";
  }

}
