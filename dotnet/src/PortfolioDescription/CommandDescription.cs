
namespace UShell {

  public class CommandDescription {
    public string UniqueCommandKey { get; set; } = "";

    public string Label { get; set; } = "";

    //"primary|secondary|success|info|warning|help|danger",
    public string Semantic { get; set; } = "primary";

    public string Description { get; set; }

    //"pi pi-tag"
    public string IconKey { get; set; }

    //"pi pi-tag"
    public string IconKeyChecked { get; set; }

    //can be null
    public string WarningToConfirm { get; set; }

    //": "{M}sms/institutes   KANN AUCH NULL SEIN, dann ist es modal (aber auch mit tabs!! submit ist dann gemeinsam!), oder der maigic-value '<CURRENT>'",
    public string TargetWorkspacePath { get; set; }

    //":{ "idToEdit": "{selectedId} (PropAmQuellUc), für alle anderen vom widget geforderten args geht ein modaler dialog auf"},
    public IDynamicParamObject InitUnitOfWork { get; set; }

    //ENABLED!
    public string[] RequiredRuntimeTagsForAvailability { get; set; }

    //VISIBLE               for example ["devmode"]
    public string[] RequiredRuntimeTagsForVisibility { get; set; }

    //CHECKED (WHEN...)     for example "devmode"
    public string CheckedRepresentingRuntimeTag { get; set; }

    /**
     * "{M}sms-sxxxxxxxxxxxx",
     */
    public string LocateAfterCommand { get; set; }
    public int? LocationPriority { get; set; } = 100;

    //if not set, than the global primary application menu is addressed",
    public string MenuOwnerUsecaseKey { get; set; }

    /** empty is not allowed (null or '' will automatically replaced to '...' (the misc-menu))
     *  additional to that where are the following magic-values for wellknown othe menüs:
     *  'MY' -> the 'USER-MENU' on the right upper side'
     */
    public string MenuFolder { get; set; } = "";

    /// <summary>
    /// "activate-workspace"|"start-usecase"|"usecase-action"|"set-runtime-tag"|"navigate" = "activate-workspace";
    /// </summary>
    public string CommandType { get; set; }

     // For commandType=="set-runtime-tag" ##################################

    // ["tagtoSet", "!tagToToggle", "-tagToRemove"]
    public string[] TagsToSet { get; set; }

    // For commandType=="navigate" ##################################
    public string RouterLink { get; set; }

    // For commandType=="activate-workspace" ##################################
    public string TargetWorkspaceKey { get; set; }

    // For commandType=="start-usecase" ##################################
    public string TargetUsecaseKey { get; set; }

    // For commandType=="usecase-action" ##################################
    public string ActionName { get; set; }

  }

}
