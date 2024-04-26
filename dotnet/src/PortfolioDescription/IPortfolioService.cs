using System;

namespace UShell {

  public interface IPortfolioService {

    PortfolioEntry[] GetPortfolioIndex();
    PortfolioDescription GetPortfolioDescription(string nameInUrl);
    ModuleDescription GetModuleDescription(string nameInUrl);

  }

}
