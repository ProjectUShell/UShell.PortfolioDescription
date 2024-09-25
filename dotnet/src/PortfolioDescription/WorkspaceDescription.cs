
using System;

namespace UShell {

  public class WorkspaceDescription {
    public string WorkspaceKey { get; set; } = "";
    public string WorkspaceTitle { get; set; } = "";
    public string IconName { get; set; }
    public bool IsSidebar { get; set; } = false;

    /// <summary>
    /// "default" (in center area) | "sidebar" | "modal"
    /// </summary>
    public string WorkspaceAppearance { get; set; } = "default";

    /// <summary>
    /// Keys of Application-Scopes that have to be set to nonempty value in order to make this workspace available.
    /// </summary>
    public string[] RequiredApplicationScopes { get; set; } = Array.Empty<string>();
  }

}
