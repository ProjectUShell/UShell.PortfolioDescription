using System;
using System.Collections.Generic;

namespace UShell {

  public interface IStaticPortfolioRegistrar {

    void AddDefaultPortfolioDescription(PortfolioDescription desc, Dictionary<string, string> tags = null);

    /// <summary>
    /// </summary>
    /// <param name="desc"></param>
    /// <param name="nameForUrl">Just the name - WITHOUT .json suffix!</param>
    /// <param name="tags"></param>
    void AddPortfolioDescription(PortfolioDescription desc, string nameForUrl, Dictionary<string, string> tags = null);

    /// <summary>
    /// </summary>
    /// <param name="desc"></param>
    /// <param name="nameForUrl">Just the name - WITHOUT .json suffix!</param>
    void AddModuleDescription(ModuleDescription desc, string nameForUrl);

  }

}
