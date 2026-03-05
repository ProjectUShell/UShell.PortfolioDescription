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
    /// <param name="urlCompatibleName">
    /// Just a name like "myproduct" without any suffix...
    /// (will automatically be hosted as "{APP-ROOT}/myproduct.portfolio.json")
    /// </param>
    /// <param name="tags"></param>
    public void AddPortfolioDescription(PortfolioDescription desc, string urlCompatibleName, Dictionary<string, string> tags = null) {
      if(tags == null) {
        tags = new Dictionary<string, string>();
      }
      if (urlCompatibleName.EndsWith(".json", StringComparison.CurrentCultureIgnoreCase)) {
        urlCompatibleName = urlCompatibleName.Substring(0, urlCompatibleName.Length - 5);
      }
      if (!urlCompatibleName.EndsWith(".portfolio", StringComparison.CurrentCultureIgnoreCase)) {
        urlCompatibleName = urlCompatibleName + ".portfolio";
      }
      lock (_PortfolioDescriptions) {
        _PortfolioDescriptions.Add(urlCompatibleName, desc);
        _PortfolioDescriptionTags.Add(urlCompatibleName, tags);
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

    public PortfolioDescription GetPortfolioDescription(string nameInUrl) {
      if (nameInUrl.EndsWith(".json", StringComparison.CurrentCultureIgnoreCase)) {
        nameInUrl = nameInUrl.Substring(0, nameInUrl.Length - 5);
      }
      lock (_PortfolioDescriptions) {
        if(_PortfolioDescriptions.TryGetValue(nameInUrl, out PortfolioDescription found)) {
          return found;
        }
        return null;
      }
    }

    public ModuleDescription GetModuleDescription(string nameInUrl) {
      if (nameInUrl.EndsWith(".json", StringComparison.CurrentCultureIgnoreCase)) {
        nameInUrl = nameInUrl.Substring(0, nameInUrl.Length - 5);
      }
      lock (_ModuleDescriptions) {
        if (_ModuleDescriptions.TryGetValue(nameInUrl, out ModuleDescription found)) {
          return found;
        }
        return null;
      }
    }

  }

}
