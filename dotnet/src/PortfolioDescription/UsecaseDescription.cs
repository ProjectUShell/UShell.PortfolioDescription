namespace System {
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
    public string WidgetClass { get; set; }  = "";

    public IDynamicParamObject UnitOfWorkDefaults { get; set; } = null;

  }
}