using Logging.SmartStandards.CopyForUShellPortfolioHosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace UShell {

  [ApiController]
  [ApiExplorerSettings(GroupName = "UShellPortfolio")]
  [AllowAnonymous()]
  [Route("/")]
  internal sealed class PortfolioController : ControllerBase {

    private readonly ILogger<PortfolioController> _Logger;
    private readonly IPortfolioService _PortfolioService;

    public PortfolioController(ILogger<PortfolioController> logger, IPortfolioService portfolioService) {
      _Logger = logger;
      _PortfolioService = portfolioService;
    }

    [HttpGet()]
    [Route("{portfolioName}.portfolio.json")]
    public ActionResult<PortfolioDescription> GetPortfolio([FromRoute] string portfolioName) {
      try {
        PortfolioDescription defaultPortfolio = _PortfolioService.GetPortfolioDescription(portfolioName);

        if(defaultPortfolio == null) {
          DevLogger.LogError($"{_PortfolioService.GetType().Name} could not find a portfolio named '{portfolioName}'");
          return this.NotFound($"No portfolio named '{portfolioName}' found.");
        }

        return this.Ok(defaultPortfolio);
      }
      catch (Exception ex) {
        _Logger.LogCritical(ex, ex.Message);
        return null;
      }
    }

    [HttpGet()]
    [Route("{moduleScopingKey}/module.json")]
    public ActionResult<ModuleDescription> GetModule([FromRoute] string moduleScopingKey) {
      try {
        ModuleDescription moduleDesc = _PortfolioService.GetModuleDescription(moduleScopingKey);

        if (moduleDesc == null) {
          DevLogger.LogError($"{_PortfolioService.GetType().Name} could not find a module.json under '{moduleScopingKey}'");
          return this.NotFound($"No module named '{moduleScopingKey}' found.");
        }

        return this.Ok(moduleDesc);
      }
      catch (Exception ex) {
        _Logger.LogCritical(ex, ex.Message);
        return null;
      }
    }

    [HttpGet()]
    [Route("portfolioindex.json")]
    public ActionResult<List<PortfolioEntry>> PortfolioIndex() {
      try {
        PortfolioEntry[] idx = _PortfolioService.GetPortfolioIndex();
        return this.Ok(idx);
      }
      catch (Exception ex) {
        _Logger.LogCritical(ex, ex.Message);
        return null;
      }
    }

  }

}
