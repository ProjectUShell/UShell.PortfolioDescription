using System;

namespace UShell {

  public interface IStaticPortfolioRegistrar {

    void AddDefaultPortfolioDescription(PortfolioDescription desc);
    void AddPortfolioDescription(PortfolioDescription desc, string nameForUrl);
    void AddModuleDescription(ModuleDescription desc, string nameForUrl);

  }

}
