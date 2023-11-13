using System.Collections.Generic;

namespace System {
  public class IDynamicParamObject : Dictionary<string, object> {
    public IDynamicParamMappingEntry[]  MapDynamic { get; set; }
  }
}