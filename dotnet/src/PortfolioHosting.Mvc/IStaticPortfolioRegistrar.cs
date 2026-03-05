using System;
using System.Collections.Generic;

namespace UShell {

  public interface IStaticPortfolioRegistrar {

    /// <summary>
    /// (will automatically be hosted as "{APP-ROOT}/default.portfolio.json")
    /// </summary>
    /// <param name="desc"></param>
    /// <param name="tags"></param>
    void AddDefaultPortfolioDescription(PortfolioDescription desc, Dictionary<string, string> tags = null);

    /// <summary>
    /// </summary>
    /// <param name="desc"></param>
    /// <param name="urlCompatibleName">
    /// Just a name like "myproduct" without any suffix...
    /// (will automatically be hosted as "{APP-ROOT}/myproduct.portfolio.json")
    /// </param>
    /// <param name="tags"></param>
    void AddPortfolioDescription(PortfolioDescription desc, string urlCompatibleName, Dictionary<string, string> tags = null); 

    /// <summary>
    /// </summary>
    /// <param name="desc"></param>
    /// <param name="urlCompatibleName">
    /// Just a name like "mymodule" without any suffix...
    /// (will automatically be hosted as "{APP-ROOT}/mymodule/module.json")
    /// </param>
    void AddModuleDescription(ModuleDescription desc, string urlCompatibleName);

  }

}
