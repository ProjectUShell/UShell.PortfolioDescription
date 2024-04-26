using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace UShell {

  public static class SetupExtensions {

    public static void AddControllerForStaticUShellPortfolio(
      this IServiceCollection extendee,
      Action<IStaticPortfolioRegistrar> portfolioDeclarator
    ) {
      IPortfolioService portfolioService = new StaticPortfolioService(portfolioDeclarator);
      SetupExtensions.AddControllerForUShellPortfolioService(extendee, portfolioService);
    }

    /// <summary>
    /// PLEASE MAKE SURE: that youve added an IPortfolioService to the services di container by your own
    /// OR use the method overload with the additional argument to provide the instance direcectly...
    /// </summary>
    /// <param name="extendee"></param>
    /// <param name="serviceInstance"></param>
    public static void AddControllerForUShellPortfolioService(
      this IServiceCollection extendee, IPortfolioService serviceInstance
    ) {

      extendee.AddSingleton<IPortfolioService>(serviceInstance);
      SetupExtensions.AddControllerForUShellPortfolioService(extendee);
    }

    public static void AddControllerForUShellPortfolioService(this IServiceCollection extendee) {
      IMvcBuilder builder = extendee.AddMvc();
      var cfp = new UShellPortfolioFeatureProvider();
      builder.ConfigureApplicationPartManager(
        (apm) => apm.FeatureProviders.Add(cfp)
      );
    }

    private sealed class UShellPortfolioFeatureProvider : IApplicationFeatureProvider<ControllerFeature> {
      public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature) {
        Type portfolioControllerType = typeof(PortfolioController);
        feature.Controllers.Add(portfolioControllerType.GetTypeInfo());
      }
    }

  }

}
