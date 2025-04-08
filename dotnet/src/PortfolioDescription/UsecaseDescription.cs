
using System.Linq;

namespace UShell {

  public class UsecaseDescription {

    public string UsecaseKey { get; set; } = "myUC";
    public string Title { get; set; } = "my UC";
    public string SingletonActionkey { get; set; } = "";
    public string IconName { get; set; } = "";

    /** class-name of an out-of-the-box widget or
     *  a special url to address external hosted widgets
     *  via 'WebComponent'-Standard or
     *  via react 'federation'-framework'
     */
    public string WidgetClass { get; set; } = "";

    public IDynamicParamObject UnitOfWorkDefaults { get; set; } = null;

    public void SetRemoteWidget(string scope, string module, string url) {
      this.WidgetClass = UsecaseDescription.BuildWidgetJson(scope, module, url);
    }

    public static string BuildWidgetJson(string scope, string module, string url) {
      return $"{{\"scope\": \"{scope}\", \"module\": \"{module}\", \"url\": \"{url}\"}}";
    }

    public void AddUowDefault(string key, object value) {
      if (this.UnitOfWorkDefaults == null) {
        this.UnitOfWorkDefaults = new IDynamicParamObject();
      }
      this.UnitOfWorkDefaults.Add(key, value);
    }

    public void AddUowDefaultDynamic(string use, string propertyName) {
      if (this.UnitOfWorkDefaults == null) {
        this.UnitOfWorkDefaults = new IDynamicParamObject();
      }
      if (!this.UnitOfWorkDefaults.ContainsKey("mapDynamic")) {
        this.UnitOfWorkDefaults.Add("mapDynamic", new IDynamicParamMappingEntry[] { });
      }
      IDynamicParamMappingEntry[] dynamicParamMappingEntries = (IDynamicParamMappingEntry[])this.UnitOfWorkDefaults["mapDynamic"];
      dynamicParamMappingEntries = dynamicParamMappingEntries.Concat(new IDynamicParamMappingEntry[] { new IDynamicParamMappingEntry() {
        Use = use,
        For = propertyName
      } }).ToArray();
      this.UnitOfWorkDefaults["mapDynamic"] = dynamicParamMappingEntries;
    }
        
  }

}
