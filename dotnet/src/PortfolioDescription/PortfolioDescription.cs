using Security.AccessTokenHandling;
using System;
using System.Linq;
using System.Collections.Generic;

namespace UShell {

  public class PortfolioDescription {

    /// <summary>
    /// Application Title
    /// </summary>
    public string ApplicationTitle { get; set; } = "Web-Frontend";

    /// <summary>
    /// the workspace, where the ushell will tread as landing location
    /// </summary>
    public string LandingWorkspaceName { get; set; } = "dashboard";

    /// <summary>
    /// '00000000-0000-0000-0000-000000000000' means DISABLED - NO LOGIN REQUIRED!!!
    /// </summary>
    public string PrimaryUiTokenSourceUid { get; set; } = "00000000-0000-0000-0000-000000000000";

    /// <summary>
    /// OAuth configuration structure
    /// </summary>
    public Dictionary<string, AuthTokenConfig> AuthTokenConfigs { get; set; } = null;

    /// <summary>
    /// The fixpoint when resolving a relative URL provided for this value is the
    /// (portfolio.json)-url where the current PortfolioDescription was loaded from.
    /// </summary>
    public string CiDescriptionUrl { get; set; } = null;

    /// <summary>
    /// The fixpoint when resolving a relative URL provided for this value is the
    /// (portfolio.json)-url where the current PortfolioDescription was loaded from.
    /// </summary>
    public string LegalContactMdUrl  { get; set; } = null;

    /// <summary>
    /// The fixpoint when resolving a relative URL provided for this value is the
    /// (portfolio.json)-url where the current PortfolioDescription was loaded from.
    /// </summary>
    public string UserAgreementMdUrl  { get; set; } = null;

    /// <summary>
    /// The Version string is used when persisting the agreement state.
    /// If it is not equal to the persisted agreement state, then the UI
    /// will force the user to re-agree!
    /// </summary>
    public string UserAgreementVersion { get; set; } = null;

    /// <summary>
    /// can be used like this: ``` { * "tenant": 4711, "region": "DE" * } ```
    /// </summary>
    public Dictionary<string, object> ApplicationScope { get; set; } = null;

    /// <summary>
    /// inital runtimtags -> !Tag ist also possible
    /// </summary>
    public string[] IntialRuntimeTags { get; set; } = null;

    /// <summary>
    /// Can be used to couple runtimeTags with entries the of the 'scope' claim from the *primaryUiToken*.
    /// For example: ``` {"showPowerFeatures": "LIC:PremiumSubscription"}```
    /// </summary>
    public Dictionary<string, string> RuntimeTagToTokenBinding { get; set; } = null;

    /// <summary>
    /// The fixpoint when resolving relative URLs provided for this array is the
    /// (portfolio.json)-url where the current PortfolioDescription was loaded from.
    /// </summary>
    public string[] ModuleDescriptionUrls { get; set; } = new string[] { };

  }

}
