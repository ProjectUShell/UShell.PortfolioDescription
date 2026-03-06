using System;
using System.Collections.Generic;
using System.Linq;

namespace UShell {

  public sealed class StaticPortfolioService : IPortfolioService, IStaticPortfolioRegistrar {

    private Dictionary<string,PortfolioDescription> _PortfolioDescriptions = new Dictionary<string, PortfolioDescription>();
    private Dictionary<string, Dictionary<string, string>> _PortfolioDescriptionTags = new Dictionary<string, Dictionary<string, string>>();
    private Dictionary<string, ModuleDescription> _ModuleDescriptions = new Dictionary<string, ModuleDescription>();

    public StaticPortfolioService(
      Action<IStaticPortfolioRegistrar> portfolioDeclarator
    ) {
      portfolioDeclarator.Invoke(this);
    }

    //////////// SETUP  ////////////

    /// <summary>
    /// (will automatically be hosted as "{APP-ROOT}/default.portfolio.json")
    /// </summary>
    /// <param name="desc"></param>
    /// <param name="tags"></param>
    public void AddDefaultPortfolioDescription(PortfolioDescription desc, Dictionary<string, string> tags = null) {
      this.AddPortfolioDescription(desc, "default", tags);
    }

    /// <summary>
    /// </summary>
    /// <param name="desc"></param>
    /// <param name="portfolioName">
    /// Just a name like "myproduct" without any suffix...
    /// (will automatically be hosted as "{APP-ROOT}/myproduct.portfolio.json")
    /// </param>
    /// <param name="tags"></param>
    public void AddPortfolioDescription(PortfolioDescription desc, string portfolioName, Dictionary<string, string> tags = null) {
      if(tags == null) {
        tags = new Dictionary<string, string>();
      }
      if (portfolioName.EndsWith(".json", StringComparison.CurrentCultureIgnoreCase)) {
        portfolioName = portfolioName.Substring(0, portfolioName.Length - 5);
      }
      if (portfolioName.EndsWith(".portfolio", StringComparison.CurrentCultureIgnoreCase)) {
        portfolioName = portfolioName.Substring(0, portfolioName.Length - 10);
      }
      lock (_PortfolioDescriptions) {
        _PortfolioDescriptions.Add(portfolioName, desc);
        _PortfolioDescriptionTags.Add(portfolioName, tags);
      }
    }

    /// <summary>
    /// </summary>
    /// <param name="desc"></param>
    /// <param name="urlCompatibleName">
    /// Just a name like "mymodule" without any suffix...
    /// (will automatically be hosted as "{APP-ROOT}/mymodule/module.json")
    /// </param>
    public void AddModuleDescription(ModuleDescription desc, string urlCompatibleName) {
      if (urlCompatibleName.EndsWith(".json", StringComparison.CurrentCultureIgnoreCase)) {
        urlCompatibleName = urlCompatibleName.Substring(0, urlCompatibleName.Length - 5);
      }
      lock (_ModuleDescriptions) {
        _ModuleDescriptions.Add(urlCompatibleName, desc);
      }
    }

    //////////// CONSUME  ////////////

    public PortfolioEntry[] GetPortfolioIndex() {
      lock (_PortfolioDescriptions) {
        return _PortfolioDescriptions.Select(
          (pd)=> new PortfolioEntry {
            Label = pd.Value.ApplicationTitle,
            PortfolioUrl = pd.Key + ".json",
            Tags = _PortfolioDescriptionTags[pd.Key]
          }
        ).ToArray();
      }
    }

    /// <summary> </summary>
    /// <param name="portfolioName">Only the technical name (URL-SAFE!) of a portfolio. NOT a full name like 'foo.portfolio.json'.</param>
    /// <returns></returns>
    public PortfolioDescription GetPortfolioDescription(string portfolioName) {
      if (portfolioName.EndsWith(".json", StringComparison.CurrentCultureIgnoreCase)) {
        portfolioName = portfolioName.Substring(0, portfolioName.Length - 5);
      }
      lock (_PortfolioDescriptions) {
        if(_PortfolioDescriptions.TryGetValue(portfolioName, out PortfolioDescription found)) {
          return found;
        }
        return null;
      }
    }

    /// <summary> </summary>
    /// <param name="moduleScopingKey">An technical name (URL-SAFE!) to discriminate application modules from each other.</param>
    /// <returns></returns>
    public ModuleDescription GetModuleDescription(string moduleScopingKey) {
      if (moduleScopingKey.EndsWith(".json", StringComparison.CurrentCultureIgnoreCase)) {
        moduleScopingKey = moduleScopingKey.Substring(0, moduleScopingKey.Length - 5);
      }
      lock (_ModuleDescriptions) {
        if (_ModuleDescriptions.TryGetValue(moduleScopingKey, out ModuleDescription found)) {
          return found;
        }
        return null;
      }
    }

  }

}
