using System;

namespace UShell {

  public interface IPortfolioService {

    PortfolioEntry[] GetPortfolioIndex();

    /// <summary> </summary>
    /// <param name="portfolioName">Only the technical name (URL-SAFE!) of a portfolio. NOT a full name like 'foo.portfolio.json'.</param>
    /// <returns></returns>
    PortfolioDescription GetPortfolioDescription(string portfolioName);

    /// <summary> </summary>
    /// <param name="moduleScopingKey">An technical name (URL-SAFE!) to discriminate application modules from each other.</param>
    /// <returns></returns>
    ModuleDescription GetModuleDescription(string moduleScopingKey);

  }

}
