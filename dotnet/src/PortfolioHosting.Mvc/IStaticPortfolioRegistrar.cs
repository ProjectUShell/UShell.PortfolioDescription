using System;
using System.Collections.Generic;
using System.Security.Policy;
using static System.Net.Mime.MediaTypeNames;

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
    /// <param name="portfolioName">
    /// Just a name like "myproduct" without any suffix...
    /// (will automatically be hosted as "{APP-ROOT}/myproduct.portfolio.json")
    /// </param>
    /// <param name="tags"></param>
    void AddPortfolioDescription(PortfolioDescription desc, string portfolioName, Dictionary<string, string> tags = null); 

    /// <summary>
    /// </summary>
    /// <param name="desc"></param>
    /// <param name = "moduleScopingKey" >
    /// An technical name(URL-SAFE!) to discriminate application modules from each other.
    /// (will automatically be hosted as "{APP-ROOT}/mymodule/module.json")
    /// </param>
    void AddModuleDescription(ModuleDescription desc, string moduleScopingKey);

  }

}
