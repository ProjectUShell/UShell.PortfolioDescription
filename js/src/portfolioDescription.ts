export class AuthTokenConfig {
  // ISSUEING /////////////////////////////////////////////////////////////////////

  /**
   * <NAME_OF_THE_STRATEGY>
   * RAW-INPUT
   * HTTP-GET
   * LOCAL_BASICAUTH_GENERATION
   * LOCAL_JWT_GENERATION
   * OAUTH_CIBA_CODEGRAND
   */
  public issueMode:
    | 'RAW-INPUT'
    | 'HTTP-GET'
    | 'LOCAL_BASICAUTH_GENERATION'
    | 'LOCAL_JWT_GENERATION'
    | 'OAUTH_CIBA_CODEGRAND' = 'RAW-INPUT';

  /**
   * when using issue mode *HTTP-GET*, then it could be: ```"assets/demoAccessToken.txt"```
   * or when using issue mode *OAUTH_CIBA_CODEGRAND*, then it could be: ```"https://theOAuthServer/token"```.
   * The fixpoint when resolving a relative URL provided for this value is the
   * (portfolio.json)-url where the current PortfolioDescription was loaded from.
   */
  public retrieveEndpointUrl: string = '';

  /**
   * basic %232432-23452-234234234%
   */
  public retrieveEndpointAuthorization: string | null = null;

  /**
   *
   */
  public localLogonNameToLower?: boolean = false;

  /**
   * "NEVER" | "OPT-IN" | "OPT-OUT" | "ALWAYS"
   * (default, if not provided: "OPT-IN")
   */
  public localLogonNamePersistation?: 'NEVER' | 'OPT-IN' | 'OPT-OUT' | 'ALWAYS';

  /**
   * LOCAL_JWT_GENERATION
   * Regular expression to validate a username pattern
   */
  public localLogonNameSyntax: string | null = null;

  /**
   * LOCAL_JWT_GENERATION
   * Employee number
   */
  public localLogonNameInputLabel: string | null = 'Username';

  /**
   * LOCAL_JWT_GENERATION
   * Portal password
   */
  public localLogonPassInputLabel: string | null = 'Password';

  /**
   * LOCAL_JWT_GENERATION
   */
  public localLogonSaltDisplayLabel: string | null = null;

  /**
   * LOCAL_JWT_GENERATION
   */
  public jwtExpMinutes: number = 1440;

  /**
   *
   */
  public jwtSelfSignKey: string | null = null;

  /**
   * default SHA265
   */
  public jwtSelfSignAlg: string | null = null;

  /**
   * OAUTH_CIBA_CODEGRAND
   */
  public clientId: string | null = null;
  /**
   * OAUTH_CIBA_CODEGRAND
   */
  public clientSecret: string | null = null;

  /**
   * OAUTH_CIBA_CODEGRAND
   * "https://theOAuthServer/authorize"
   */
  public authEndpointUrl: string | null = null;

  /**
   *
   */
  public additionalAuthArgs: { [argName: string]: [value: string] } | null = null;

  /**
   *
   */
  public additionalRetrieveArgs: { [argName: string]: [value: string] } | null = null;

  /**
   *
   */
  public retrieveViaGet: boolean = false;

  /**
   * this can be set to true to inform about the fact, that the
   * oauth-server will reject any logon within a iframe or is just not able
   * to handle its session-cookies correctly.
   * based on this, the ushell will skip the convenience of serving the logon
   * page within an iframe (instead of this the user will need to click on a hyperlink)
   */
  public authEndpointRejectsIframe: boolean = false;

  // VALIDATION /////////////////////////////////////////////////////////////////////

  /**
   *  IMPLICIT_WHEN_USED
   *  LOCAL_JWT_VALIDATION
   *  OAUTH_INTROSPECTION_ENDPOINT
   *  GITHUB_VALIDATION_ENDPOINT
   */
  public validationMode:
    | 'IMPLICIT_WHEN_USED'
    | 'LOCAL_JWT_VALIDATION'
    | 'OAUTH_INTROSPECTION_ENDPOINT'
    | 'GITHUB_VALIDATION_ENDPOINT' = 'IMPLICIT_WHEN_USED';

  /**
   *
   */
  public validationOutcomeCacheMins: number = 15;

  /**
   * LOCAL_JWT_VALIDATION
   */
  public jwtValidationKey?: string | null;

  /**
   * LOCAL_JWT_VALIDATION
   */
  public jwtAlg: string | null = null;

  /**
   *  not compatible to IMPLICIT_WHEN_USED
   */
  public claimValidationIgnoresCasing: boolean = true;

  /**
   * Only requrired, when using a service endpoint to validate the token.
   * "https://theOAuthServer/introspect"
   */
  public validationEndpointUrl?: string | null;

  /**
   * Only available, when using a service endpoint to validate the token.
   * Specifies content for thethe HTTP-Authorization header like this:
   * ```"basic %232432-23452-234234234%"``` or ```"bearer %232432-23452-234234234%"```
   * where any *tokenSourceUid* can be used as placeholder.
   */
  public validationEndpointAuthorization?: string | null;

  // CLAIMS /////////////////////////////////////////////////////////////////////

  /**
   * Claims, used for JWT self issuing (local only) and/or token validation (local or endpoint-based).
   * Sample:
   * ```{ "sub":"user-%logonName%", "aud": "CompanyX", "scope":"foo bar:%tenant% baz" }```
   */
  public claims?: { [claim: string]: [value: string] } | null;
}

export class AuthenticatedAccessDescription {
  public primaryUiTokenSources: string[] = [];

  /**
   * Can be used to couple runtimeTags with entries the of the 'scope' claim from the *primaryUiToken*.
   * EXAMPLE-1: {"showPowerFeatures": "LIC:PremiumSubscription"} will set the Runtime-Tag 'showPowerFeatures' when
   * then token contains a wellknown License scope for a premium subscription.
   * EXAMPLE-2: {"*Role": "Role:*"} via wildcard-matching every token-scope within the 'Role:' dimension will be
   * set as runtime-tag, concatinated with the suffix "Role" (so 'Role:Admin' will result in 'AdminRole')
   */
  public runtimeTagsFromTokenScope: { [tag: string]: string } = {};
}

export class AnonymousAccessDescription {
  public authIndependentWorkspaces: string[] = [];
  public authIndependentUsecases: string[] = [];
  public authIndependentCommands: string[] = [];
  public runtimeTagsIfAnonymous: string[] = [];
}

export class PortfolioDescription {
  public applicationTitle: string = 'Web-Frontend';

  public landingWorkspaceName: string = 'dashboard';

  /**
   * '00000000-0000-0000-0000-000000000000' means DISABLED - NO LOGIN REQUIRED!!!
   */
  public primaryUiTokenSourceUid: string = '00000000-0000-0000-0000-000000000000';

  public authTokenConfigs: { [tokenSourceUid: string]: AuthTokenConfig } | null = {};

  public authenticatedAccess: AuthenticatedAccessDescription = {
    primaryUiTokenSources: [],
    runtimeTagsFromTokenScope: {},
  };

  public anonymousAccess: AnonymousAccessDescription = {
    authIndependentCommands: [],
    authIndependentUsecases: [],
    authIndependentWorkspaces: [],
    runtimeTagsIfAnonymous: [],
  };

  /**
   * The fixpoint when resolving a relative URL provided for this value is the
   * (portfolio.json)-url where the current PortfolioDescription was loaded from.
   */
  public ciDescriptionUrl: string | null = null;

  /**
   * The fixpoint when resolving a relative URL provided for this value is the
   * (portfolio.json)-url where the current PortfolioDescription was loaded from.
   */
  public legalContactMdUrl: string | null = null;

  /**
   * The fixpoint when resolving a relative URL provided for this value is the
   * (portfolio.json)-url where the current PortfolioDescription was loaded from.
   */
  public userAgreementMdUrl: string | null = null;

  /**
   * The Version string is used when persisting the agreement state.
   * If it is not equal to the persisted agreement state, then the UI
   * will force the user to re-agree!
   */
  public userAgreementVersion: string | null = null;

  /**
   * can be used like this: ``` { * "tenant": 4711, "region": "DE" * } ```
   */
  public applicationScope: { [dimension: string]: ApplicationScopeEntry } | null = {};

  public intialRuntimeTags: string[] | null = null;

  /**
   * The fixpoint when resolving relative URLs provided for this array is the
   * (portfolio.json)-url where the current PortfolioDescription was loaded from.
   */
  public moduleDescriptionUrls: string[] = [];
}

export class ApplicationScopeEntry {
  public label: string = '';
  public value: any = null;
  public isVisible: boolean = false;
  public switchScopeCommand: string | null = null;
}

//TODO: für die datasrocues muss das UDAS.ModelDescrioption refrerenziert werden

export class CiDescription {
  public loadedFrom: string | null = null;

  public logoUrlLight: string = 'assets/layout/images/logo-cte.png';
  public logoUrlDark: string = 'assets/layout/images/logo-cte-dark.png';

  public resolvedLogoUrlLight: string | null = null; // << will be set by the PortfolioLoader!
  public resolvedLogoUrlDark: string | null = null; // << will be set by the PortfolioLoader!

  public menuMode: string = 'static';
  public pageColorMode: string = 'light';
  public menuColorMode: string = 'light';
  public topBarColorMode: string = 'dim';
  public inputBgFilled: boolean = true;
  public ripple: boolean = false;
  public rtl: boolean = false;
  public componentTheme: string = 'cyan';
  public allowUserCustomizing: boolean = true;
}

/**
 * dynamic parameter object which
 * optionally can contain a 'mapDynamic'-structure
 */
export interface IDynamicParamObject {
  /**
   * mapping-entries, requesting dynamically provided values to be mapped
   * over properties on the current object
   */
  mapDynamic?: IDynamicParamMappingEntry[];

  [key: string]: any;
}

export interface IDynamicParamMappingEntry {
  /**
   * based on this [EPIC](https://github.com/ProjectUShell/UShell.Docs/blob/master/epics/mapDynamic-Approach.md)
   * there can be the following:
   *  factory://UUID
   *  factory://UID64
   *  factory://now
   *  setting://GROUP.FIELD
   *   commandArgs://FIELD[.PROP[...]]
   *   unitOfWork://FIELD[.PROP[...]]
   *   applicationScope://FIELD[.PROP[...]]
   *   userInput://FIELD[.PROP[...]]
   */
  use: string;

  /**
   * the name of the target property on the paremter object,
   * that should be overwritten
   */
  for: string;
}

export class ModuleDescription {
  public moduleUid: string | null = null;
  public moduleTitle: string | null = null;
  public moduleScopingKey: string | null = null;

  public workspaces: WorkspaceDescription[] = [];

  public usecases: UsecaseDescription[] = [];

  /**
   * assign's long living usecases (that should be permanently available) to workspaces
   */
  public staticUsecaseAssignments: StaticUsecaseAssignment[] = [];

  public datasources: DatasourceDescription[] = [];

  public datastores: DatastoreDescription[] = [];

  public commands: CommandDescription[] = [];
}

export class DatasourceDescription {
  public datasourceUid: string = '';
  public providerClass: string = '';
  public providerArguments: IDynamicParamObject = {};
  public entityName?: string = '';
}

export class DatastoreDescription {
  public key: string = '';
  public providerClass: 'localstore' | 'fuse' = 'fuse';
  public providerArguments?: any;
}

export class UsecaseDescription {
  public usecaseKey: string = 'myUC';
  public title: string = 'my UC';
  public singletonActionkey: string = '';
  public iconName: string = '';

  /** class-name of an out-of-the-box widget or
   *  a special url to address external hosted widgets
   *  via 'WebComponent'-Standard or
   *  via react 'federation'-framework'
   */
  public widgetClass: string = '';

  public unitOfWorkDefaults: IDynamicParamObject = {};
}

export class StaticUsecaseAssignment {
  public usecaseKey: string = '';
  public targetWorkspaceKey: string = '';
  public initUnitOfWork?: IDynamicParamObject;
}

export class WorkspaceDescription {
  public workspaceKey: string = '';
  public workspaceTitle: string = '';
  public iconName?: string;
  public isSidebar: boolean = false;
}

export class CommandDescription {
  //"{M}sms-sxxxxxxxxxxxx"
  public uniqueCommandKey: string = '';

  public label: string = '';

  //"primary|secondary|success|info|warning|help|danger",
  public semantic: 'primary' | 'secondary' | 'success' | 'info' | 'warning' | 'help' | 'danger' = 'primary';

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
  public initUnitOfWork?: IDynamicParamObject;

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
  public menuOwnerUsecaseKey?: string;

  /** empty is not allowed (null or '' will automatically replaced to '...' (the misc-menu))
   *  additional to that where are the following magic-values for wellknown othe menüs:
   *  'MY' -> the 'USER-MENU' on the right upper side'
   */
  public menuFolder: string = '';

  public commandType:
    | 'activate-workspace'
    | 'start-usecase'
    | 'usecase-action'
    | 'set-runtime-tag'
    | 'navigate'
    | 'switch-scope' = 'activate-workspace';

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

  // "usecaseArgumentMapping":{ "idToEdit": "selectedId (PropAmQuellUc), für alle anderen vom widget geforderten args geht ein modaler dialog auf"},
}
