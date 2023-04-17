export class TokenSourceDescription {

  // ISSUEING /////////////////////////////////////////////////////////////////////

  /**
   * <NAME_OF_THE_STRATEGY>
   * RAW-INPUT
   * HTTP-GET
   * LOCAL_BASICAUTH_GENERATION
   * LOCAL_JWT_GENERATION
   * OAUTH_CIBA_CODEGRAND
   */
  public issueMode: string = "RAW-INPUT";

  /**
   * when using issue mode *HTTP-GET*, then it could be: ```"assets/demoAccessToken.txt"```
   * or when using issue mode *OAUTH_CIBA_CODEGRAND*, then it could be: ```"https://theOAuthServer/token"```.
   * The fixpoint when resolving a relative URL provided for this value is the
   * (portfolio.json)-url where the current PortfolioDescription was loaded from.
   */
  public retrieveEndpointUrl: string =  "";

  /**
   * basic %232432-23452-234234234%
   */
  public retrieveEndpointAuthorization: string|null = null;

  /**
   * 
   */
  public localLogonNameToLower?: boolean = false;

  /**
   * "NEVER" | "OPT-IN" | "OPT-OUT" | "ALWAYS"
   * (default, if not provided: "OPT-IN")
   */
  public localLogonNamePersistation?: string;

  /**
   * LOCAL_JWT_GENERATION
   * Regular expression to validate a username pattern
   */
  public localLogonNameSyntax: string|null = null;

  /**
   * LOCAL_JWT_GENERATION
   * Employee number
   */
  public localLogonNameInputLabel: string|null = "Username";

  /**
   * LOCAL_JWT_GENERATION
   * Portal password
   */
  public localLogonPassInputLabel: string|null = "Password";

  /**
   * LOCAL_JWT_GENERATION
   */
  public localLogonSaltDisplayLabel: string| null = null;

  /**
   * LOCAL_JWT_GENERATION
   */
  public jwtExpMinutes: number = 1440;

  /**
   * 
   */
  public jwtSelfSignKey: string| null = null;

  /**
   * default SHA265
   */
  public jwtSelfSignAlg: string| null = null;

  /**
   * OAUTH_CIBA_CODEGRAND
   */
  public clientId: string| null = null;
  /**
   * OAUTH_CIBA_CODEGRAND
   */
  public clientSecret: string| null = null;

  /**
   * OAUTH_CIBA_CODEGRAND
   * "https://theOAuthServer/authorize"
   */
  public authEndpointUrl: string| null = null;

  /**
   * 
   */
  public additionalAuthArgs: { [argName:string]: [value:string]}| null = null;

  /**
   * 
   */
  public additionalRetrieveArgs: { [argName:string]: [value:string]}| null = null;

  /**
   * 
   */
  public retrieveViaGet: boolean = false;


  // VALIDATION /////////////////////////////////////////////////////////////////////

  /**
   *  IMPLICIT_WHEN_USED
   *  LOCAL_JWT_VALIDATION
   *  OAUTH_INTROSPECTION_ENDPOINT
   *  GITHUB_VALIDATION_ENDPOINT
   */
  public validationMode: string = "IMPLICIT_WHEN_USED";

  /**
   * 
   */
  public validationOutcomeCacheMins: number = 15;

  /**
   * LOCAL_JWT_VALIDATION
   */
  public jwtValidationKey?: string|null;

  /**
   *  not completible to IMPLICIT_WHEN_USED
   */
  public claimValidationIgnoresCasing: boolean = true;

  /**
   * Only requrired, when using a service endpoint to validate the token.
   * "https://theOAuthServer/introspect"
   */
  public validationEndpointUrl?: string|null ;

  /**
   * Only available, when using a service endpoint to validate the token.
   * Specifies content for thethe HTTP-Authorization header like this:
   * ```"basic %232432-23452-234234234%"``` or ```"bearer %232432-23452-234234234%"```
   * where any *tokenSourceUid* can be used as placeholder.
   */
  public validationEndpointAuthorization?: string|null ;

  // CLAIMS /////////////////////////////////////////////////////////////////////

  /**
   * Claims, used for local JWT issuing and/or token validation.
   * Sample:
   * ```{ "sub":"user-%logonName%", "aud": "CompanyX", "scope":"foo bar:%tenant% baz" }```
   */
  public claims?: { [claim:string]: [value:string]}|null;

}

export class PortfolioDescription {

  public applicationTitle : string = "Web-Frontend";

  public landingWorkspaceName : string = "dashboard";

  /**
   * '00000000-0000-0000-0000-000000000000' means DISABLED - NO LOGIN REQUIRED!!!
   */
  public primaryUiTokenSourceUid: string = "00000000-0000-0000-0000-000000000000";

  public tokenSources: { [tokenSourceUid:string]: TokenSourceDescription}|null = {};

  /**
   * The fixpoint when resolving a relative URL provided for this value is the
   * (portfolio.json)-url where the current PortfolioDescription was loaded from.
   */
  public ciDescriptionUrl : string|null = null;

  /**
   * The fixpoint when resolving a relative URL provided for this value is the
   * (portfolio.json)-url where the current PortfolioDescription was loaded from.
   */
  public legalContactMdUrl : string|null = null;

  /**
   * The fixpoint when resolving a relative URL provided for this value is the
   * (portfolio.json)-url where the current PortfolioDescription was loaded from.
   */
  public userAgreementMdUrl : string|null = null;

  /**
   * The Version string is used when persisting the agreement state.
   * If it is not equal to the persisted agreement state, then the UI
   * will force the user to re-agree!
   */
  public userAgreementVersion : string|null = null;

  /**
   * can be used like this: ``` { * "tenant": 4711, "region": "DE" * } ```
   */
  public applicationScope : { [dimension:string]: any}|null = {};

  public intialRuntimeTags : string[]|null = null;

  /**
  * Can be used to couple runtimeTags with entries the of the 'scope' claim from the *primaryUiToken*.
  * For example: ``` {"showPowerFeatures": "LIC:PremiumSubscription"}```
  */
  public runtimeTagToTokenBinding: {[runtimeTagToBind: string]: [tokenScopeEntry:string]}|null = null;

    /**
   * The fixpoint when resolving relative URLs provided for this array is the
   * (portfolio.json)-url where the current PortfolioDescription was loaded from.
   */
  public moduleDescriptionUrls : string[] = [];

}

    //TODO: für die datasrocues muss das UDAS.ModelDescrioption refrerenziert werden

export class CiDescription {

  public loadedFrom : string|null = null;

  public logoUrlLight: string = "assets/layout/images/logo-cte.png";
  public logoUrlDark: string = "assets/layout/images/logo-cte-dark.png";

  public resolvedLogoUrlLight: string|null = null; // << will be set by the PortfolioLoader!
  public resolvedLogoUrlDark: string|null = null; // << will be set by the PortfolioLoader!

  public menuMode: string = "static";
  public pageColorMode: string =  "light";
  public menuColorMode: string = "light";
  public topBarColorMode: string = "dim";
  public inputBgFilled: boolean = true;
  public ripple: boolean = false;
  public rtl: boolean = false;
  public componentTheme: string =  "cyan";
  public allowUserCustomizing: boolean = true;

}

export class ModuleDescription {

    public moduleUid: string|null = null;
    public moduleTitle: string|null = null;
    public moduleScopingKey: string|null = null;

    public datasources : DatasourceDescription[] = [];

    public workspaces : WorkspaceDescription[] = [];
    public usecases : UsecaseDescription[] = [];
    public commands : CommandDescription[] = [];

}

export class DatasourceDescription {
    public datasourceUid: string = "";
    public providerClass: string = "";
    public providerArguments: object = {};
    public entityName?: string = "";
}

export class UsecaseDescription {
    public useCaseKey: string = "myUC";
    public title: string = "my UC";
    public singletonActionkey: string = "";
    public iconName: string = "";
    public widgetClass: string = "";
    public unitOfWorkDefaults: object = {};
}

export class StaticUseCaseAssignment {
    public useCaseKey: string = "";
    public targetWorkspaceKey: string = "";
    public initUnitOfWork?: object;
}

export class WorkspaceDescription {
    public workspaceKey: string = "";
    public workspaceTitle: string = "";
    public iconName?: string;
    //public defaultStaticUseCaseKeys: string[] = [];
    public isSidebar: boolean = false;
}

export class CommandDescription {

    //"{M}sms-sxxxxxxxxxxxx"
    public uniqueCommandKey: string = "";;

    public label: string = "";

    //"primary|secondary|success|info|warning|help|danger",
    public semantic: string = "primary";

    public description?: string;

    //"pi pi-tag"
    public iconKey?: string;

    //"pi pi-tag"
    public iconKeyChecked?: string;

    //can be null
    public WarningToConfirm?: string;

    //": "{M}sms/institutes   KANN AUCH NULL SEIN, dann ist es modal (aber auch mit tabs!! submit ist dann gemeinsam!), oder der maigic-value '<CURRENT>'",
    public targetWorkspacePath?: string;

    //":{ "idToEdit": "{selectedId} (PropAmQuellUc), für alle anderen vom widget geforderten args geht ein modaler dialog auf"},
    public initUnitOfWork?: object;

    //ENABLED!
    public requiredRuntimeTagsForAvailability?: string[];

    //VISIBLE               for example ["devmode"]
    public requiredRuntimeTagsForVisibility?: string[];

    //CHECKED (WHEN...)     for example "devmode"
    public checkedRepresentingRuntimeTag?: string;

    /**
     * "{M}sms-sxxxxxxxxxxxx",
     */
    public locateAfterCommand?: string;
    public locationPriority?: number = 100;

    //if not set, than the global primary application menu is addressed",
    public menuOwnerUseCaseKey?: string;

    /** empty is not allowed (null or '' will automatically replaced to '...' (the misc-menu))
     *  additional to that where are the following magic-values for wellknown othe menüs:
     *  'MY' -> the 'USER-MENU' on the right upper side'
     */
    public menuFolder: string = "";

    /** ":"start-usecase     nur für dynmische!!!!, weil statische sind immer in einem workpace oder enderen UC drin",
     */
    public commandType: string = "";

     // For commandType=="set-runtime-tag" ##################################

    // ["tagtoSet", "!tagToToggle", "-tagToRemove"]
    public tagsToSet?: string[];

    // For commandType=="navigate" ##################################
    public routerLink?: any;

    // For commandType=="activate-workspace" ##################################
    public targetWorkspaceKey?: string;

    // For commandType=="start-usecase" ##################################
    public targetUsecaseKey?: string;

    // For commandType=="usecase-action" ##################################
    public actionName?: string;

       // "useCaseArgumentMapping":{ "idToEdit": "selectedId (PropAmQuellUc), für alle anderen vom widget geforderten args geht ein modaler dialog auf"},


}
