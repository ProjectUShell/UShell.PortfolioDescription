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
    
    public void AddDefaultPortfolioDescription(PortfolioDescription desc, Dictionary<string, string> tags = null) {
      this.AddPortfolioDescription(desc, "default.portfolio", tags);
    }

    public void AddPortfolioDescription(PortfolioDescription desc, string nameForUrl, Dictionary<string, string> tags = null) {
      if(tags == null) {
        tags = new Dictionary<string, string>();
      }
      if (nameForUrl.EndsWith(".json", StringComparison.CurrentCultureIgnoreCase)) {
        nameForUrl = nameForUrl.Substring(0, nameForUrl.Length - 5);
      }
      lock (_PortfolioDescriptions) {
        _PortfolioDescriptions.Add(nameForUrl, desc);
        _PortfolioDescriptionTags.Add(nameForUrl, tags);
      }
    }

    public void AddModuleDescription(ModuleDescription desc, string nameForUrl) {
      if (nameForUrl.EndsWith(".json", StringComparison.CurrentCultureIgnoreCase)) {
        nameForUrl = nameForUrl.Substring(0, nameForUrl.Length - 5);
      }
      lock (_ModuleDescriptions) {
        _ModuleDescriptions.Add(nameForUrl, desc);
      }
    }

    //////////// CONSUME  ////////////

    public PortfolioEntry[] GetPortfolioIndex() {
      lock (_PortfolioDescriptions) {
        return _PortfolioDescriptions.Select(
          (pd)=> new PortfolioEntry {
            Label = pd.Value.ApplicationTitle,
            PortfolioUrl = pd.Key,
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
