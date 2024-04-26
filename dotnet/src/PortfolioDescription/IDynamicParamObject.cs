using System.Collections.Generic;

namespace UShell {

  public class IDynamicParamObject : Dictionary<string, object> {
    public IDynamicParamMappingEntry[]  MapDynamic { get; set; }
  }

}
