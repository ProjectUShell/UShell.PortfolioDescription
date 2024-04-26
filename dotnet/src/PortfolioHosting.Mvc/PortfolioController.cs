using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace UShell {

  [ApiController]
  [ApiExplorerSettings(GroupName = "UShellPorfolio")]
  internal sealed class PortfolioController : ControllerBase {

    private readonly ILogger<PortfolioController> _Logger;
    private readonly IPortfolioService _PortfolioService;

    public PortfolioController(ILogger<PortfolioController> logger, IPortfolioService portfolioService) {
      _Logger = logger;
      _PortfolioService = portfolioService;
    }

    [HttpGet()]
    [Route("Portfolio/{portfolioName}.json")]
    public ActionResult<PortfolioDescription> GetPortfolio([FromRoute] string portfolioName) {
      try {
        PortfolioDescription defaultPortfolio = this._PortfolioService.GetPortfolioDescription(portfolioName);
        return this.Ok(defaultPortfolio);
      }
      catch (Exception ex) {
        _Logger.LogCritical(ex, ex.Message);
        return null;
      }
    }

    [HttpGet()]
    [Route("Portfolio/Module/{moduleName}.json")]
    public ActionResult<ModuleDescription> GetModule([FromRoute] string moduleName) {
      try {
        return this._PortfolioService.GetModuleDescription(moduleName);
      }
      catch (Exception ex) {
        _Logger.LogCritical(ex, ex.Message);
        return null;
      }
    }

    [HttpGet()]
    [Route("Portfolio/PortfolioIndex.json")]
    public ActionResult<List<PortfolioEntry>> PortfolioIndex() {
      try {
        return this.Ok(this._PortfolioService.GetPortfolioIndex());
      }
      catch (Exception ex) {
        _Logger.LogCritical(ex, ex.Message);
        return null;
      }
    }

  }

}
