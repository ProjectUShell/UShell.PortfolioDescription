using System.Collections.Generic;

namespace UShell {

  public class ServiceDescription {
    public string ServiceUid { get; set; } = "";
    public string ProviderClass { get; set; } = "";
    public Dictionary<string, string> ProviderArguments { get; set; } = new Dictionary<string, string>();
    public string ServiceName { get; set; } = "";
  }

}
