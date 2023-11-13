namespace System {
  public class UsecaseDescription {

    public string usecaseKey = "myUC";
    public string title = "my UC";
    public string singletonActionkey = "";
    public string iconName = "";

    /** class-name of an out-of-the-box widget or
     *  a special url to address external hosted widgets
     *  via 'WebComponent'-Standard or
     *  via react 'federation'-framework'
     */
    public string widgetClass  = "";

    public IDynamicParamObject unitOfWorkDefaults  = null;

  }
}