using System;
using System.Collections.Generic;
using System.Text;

namespace System {
  public class ModuleDescription {
    public string moduleUid = null;
    public string moduleTitle = null;
    public string moduleScopingKey = null;
    public List<WorkspaceDescription> workspaces = new List<WorkspaceDescription>();
    public List<UsecaseDescription> usecases = new List<UsecaseDescription>();
    public List<StaticUsecaseAssignment> staticUsecaseAssignments = new List<StaticUsecaseAssignment>();
    public List<DatasourceDescription> datasources = new List<DatasourceDescription>();
    public List<DatastoreDescription> datastores = new List<DatastoreDescription>();
    public List<CommandDescription> commands = new List<CommandDescription>();
  }
}
