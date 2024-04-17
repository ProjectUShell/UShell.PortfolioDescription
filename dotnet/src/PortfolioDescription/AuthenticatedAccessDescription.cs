using System.Collections.Generic;

namespace System {

  public class AuthenticatedAccessDescription {
    public string[] PrimaryUiTokenSources;
    public Dictionary<string, string> RuntimeTagsFromTokenScope = new Dictionary<string, string>();
  }
}