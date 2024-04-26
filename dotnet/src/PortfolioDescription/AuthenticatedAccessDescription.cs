using System.Collections.Generic;

namespace UShell {

  public class AuthenticatedAccessDescription {
    public string[] PrimaryUiTokenSources { get; set; }
    public Dictionary<string, string> RuntimeTagsFromTokenScope { get; set; } = new Dictionary<string, string>();
  }

}
