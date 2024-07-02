using System.Collections.Generic;

namespace UShell {

  public class AuthenticatedAccessDescription {

    public string[] PrimaryUiTokenSources { get; set; }

    /// <summary>
    /// Can be used to couple runtimeTags with entries the of the 'scope' claim from the *primaryUiToken*.
    /// EXAMPLE-1: {"showPowerFeatures": "LIC:PremiumSubscription"} will set the Runtime-Tag 'showPowerFeatures' when 
    /// then token contains a wellknown License scope for a premium subscription.
    /// EXAMPLE-2: {"*Role": "Role:*"} via wildcard-matching every token-scope within the 'Role:' dimension will be
    /// set as runtime-tag, concatinated with the suffix "Role" (so 'Role:Admin' will result in 'AdminRole')
    /// </summary>
    public Dictionary<string, string> RuntimeTagsFromTokenScope { get; set; } = new Dictionary<string, string>();

  }

}
