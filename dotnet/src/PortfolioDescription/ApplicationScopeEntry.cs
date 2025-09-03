using System.Collections.Generic;

namespace UShell {

  public class ApplicationScopeEntry : ApplicationScopeDefinition {

    //public string Label { get; set; } = string.Empty;
    //public object Value { get; set; } = null;

    public bool IsVisible { get; set; } = false;
    public string SwitchScopeCommand { get; set; } = null;

  }

  public class ApplicationScopeDefinition {

    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// if null, then Name will be used as label.
    /// </summary>
    public string Label { get; set; } = null;

    public string InitialValue { get; set; } = null;

    /// <summary>
    /// KnownValue(Key) with Display-Label (if null, then key will be used as label).
    /// If this is null, then any value is allowed.
    /// </summary>
    public Dictionary<string, string> KnownValues { get; set; } = null;

    /// <summary>
    /// If this is set, then the values of this scope depends on current values of other scopes.
    /// In this case, the 'DependentScopeConstraints' will be evaluated.
    /// </summary>    
    public string[] DependentScopeNames { get; set; } = null;

    /// <summary>
    /// Contains constraint-rules for the values of this scope, depending on the current values of the 'DependentScopeNames'.
    /// </summary>
    public ApplicationScopeValueConstraint[] DependentScopeConstraints { get; set; } = null;

  }

  /// <summary>
  /// Represents a ruleset which is declairing a possible combination of dependent scopes (dimenensions).
  /// NOTE: if there is more than one constraint, having the same KnownValue, then this is an OR condition!
  /// </summary>
  public class ApplicationScopeValueConstraint {

    /// <summary>
    /// NOTE: if there is more than one constraint, having the same KnownValue, then this is an OR condition!
    /// </summary>
    public string KnownValue { get; set; }

    /// <summary>
    /// The array length must be initialized equal to the length of DependentScopeNames.
    /// Every Array entry may be null (means to be valid for all values of the dependent scope) or
    /// an exact value (of the dependent scope), which is required for .
    /// </summary>
    public string[] DependentScopeValues { get; set; } = null;

  }





}
