using Security.AccessTokenHandling;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Data;

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
      if (description.AnonymousAccess == null) {
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

    public static string AddGuifad(this ModuleDescription description, string entityName, string title) {
      string usecaseKey = title.ToLower().Replace(' ', '-');
      var instance = new UsecaseDescription() {
        UsecaseKey = usecaseKey,
        WidgetClass = "guifad",
        Title = title,
        SingletonActionkey = usecaseKey,
        UnitOfWorkDefaults = new IDynamicParamObject {
          { "entityName", entityName }
        }
      };
      description.Usecases.Add(instance);
      return usecaseKey;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="description"></param>
    /// <param name="datastoreKey"></param>
    /// <param name="url"></param>
    /// <param name="routePattern">route | body | method</param>
    /// <param name="primaryTokenSourceUid"></param>
    /// <param name="customizingMethod"></param>
    public static void AddFuseDatastore(
      this ModuleDescription description,
      string datastoreKey,
      string url,
      string routePattern,
      string primaryTokenSourceUid = null,
      Action<DatastoreDescription> customizingMethod = null
    ) {
      var instance = new DatastoreDescription();
      instance.Key = datastoreKey;
      instance.ProviderClass = "fuse";
      Dictionary<string, string> providerArguments = new Dictionary<string, string>() {
        { "url", url },
        { "routePattern", routePattern }
      };
      if (primaryTokenSourceUid != null) {
        providerArguments.Add("tokenSourceUid", primaryTokenSourceUid);
      }
      instance.ProviderArguments = providerArguments;
      if (customizingMethod != null) {
        customizingMethod.Invoke(instance);
      }
      description.Datastores.Add(instance);
    }

    /// <summary>
    /// Adds a new UsecaseDescription and returns its 'UsecaseKey'
    /// Creates a StaticUsecaseAssignment for the given 'workspaceKey'
    /// Adds a Workspace with the given 'workspaceTitle' if it does not exist yet
    /// Adds a Command to start the Workspace with the given 'workspaceTitle'
    /// </summary>
    /// <param name="description"></param>
    /// <param name="usecaseTitle"></param>
    /// <param name="workspaceTitle"></param>
    /// <param name="menuFolder"></param>
    /// <param name="customizingMethod"></param>
    /// <returns></returns>
    public static string AddUsecaseToWorkspaceWithCommand(
      this ModuleDescription description,
      string usecaseTitle,
      string workspaceTitle,
      string menuFolder,
      Action<UsecaseDescription, WorkspaceDescription, CommandDescription> customizingMethod = null
     ) {
      UsecaseDescription usecase = new UsecaseDescription();
      usecase.Title = usecaseTitle;
      usecase.UsecaseKey = usecaseTitle.ToLower().Replace(' ', '-');

      WorkspaceDescription workspace = new WorkspaceDescription();
      workspace.WorkspaceTitle = workspaceTitle;
      workspace.WorkspaceKey = workspaceTitle.ToLower().Replace(' ', '-');

      CommandDescription command = new CommandDescription();
      command.UniqueCommandKey = $"show-{workspaceTitle.ToLower().Replace(' ', '-')}";
      command.Label = workspaceTitle;
      command.CommandType = "activate-workspace";
      command.MenuFolder = menuFolder;
      command.TargetWorkspaceKey = workspace.WorkspaceKey;

      StaticUsecaseAssignment staticUsecaseAssignment = new StaticUsecaseAssignment();
      staticUsecaseAssignment.UsecaseKey = usecase.UsecaseKey;
      staticUsecaseAssignment.TargetWorkspaceKey = workspace.WorkspaceKey;

      if (customizingMethod != null) {
        customizingMethod.Invoke(usecase, workspace, command);
      }
      description.Usecases.Add(usecase);
      description.Workspaces.Add(workspace);
      description.Commands.Add(command);
      description.StaticUsecaseAssignments.Add(staticUsecaseAssignment);
      return usecase.UsecaseKey;
    }

    #endregion

  }

}
