using Security.AccessTokenHandling;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UShell {

  public static class FluentBuildupExtensions {

    #region " PortfolioDescription "

    public static void AddModuleDescriptionUrl(this PortfolioDescription description, string moduleDescriptionUrl) {
      description.ModuleDescriptionUrls = description.ModuleDescriptionUrls.Union(
        new[] { moduleDescriptionUrl }
      ).ToArray();
    }

    /// <summary>
    /// </summary>
    /// <param name="description"></param>
    /// <param name="customizingMethod"></param>
    /// <returns></returns>
    public static void EnableAnonymousAccess(
      this PortfolioDescription description,
      Action<AnonymousAccessDescription> customizingMethod = null
     ) {
      if(description.AnonymousAccess == null) {
        description.AnonymousAccess = new AnonymousAccessDescription();
      }
      if (customizingMethod != null) {
        customizingMethod.Invoke(description.AnonymousAccess);
      }
    }

    #endregion


    #region " ModuleDescription "

    /// <summary>
    /// Adds a new WorkspaceDescription and returns its 'WorkspaceKey'
    /// </summary>
    /// <param name="description"></param>
    /// <param name="workspaceTitle"></param>
    /// <param name="customizingMethod"></param>
    /// <returns></returns>
    public static string AddWorkspace(
      this ModuleDescription description,
      string workspaceTitle,
      Action<WorkspaceDescription> customizingMethod = null
     ) {
      var instance = new WorkspaceDescription();
      instance.WorkspaceTitle = workspaceTitle;
      instance.WorkspaceKey = workspaceTitle.ToLower().Replace(' ', '-');
      if (customizingMethod != null) {
        customizingMethod.Invoke(instance);
      }
      description.Workspaces.Add(instance);
      return instance.WorkspaceKey;
    }

    /// <summary>
    /// Adds a new UsecaseDescription and returns its 'UsecaseKey'
    /// </summary>
    /// <param name="description"></param>
    /// <param name="title"></param>
    /// <param name="customizingMethod"></param>
    /// <returns></returns>
    public static string AddUsecase(
      this ModuleDescription description,
      string title,
      Action<UsecaseDescription> customizingMethod = null
     ) {
      var instance = new UsecaseDescription();
      instance.Title = title;
      instance.UsecaseKey = title.ToLower().Replace(' ', '-');
      if (customizingMethod != null) {
        customizingMethod.Invoke(instance);
      }
      description.Usecases.Add(instance);
      return instance.UsecaseKey;
    }

    #endregion

  }

}
