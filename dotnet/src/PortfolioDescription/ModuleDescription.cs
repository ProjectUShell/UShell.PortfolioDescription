using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace UShell {

  [DebuggerDisplay("{ModuleTitle} [{ModuleScopingKey}]")]
  public class ModuleDescription {

    public string ModuleUid { get; set; }= null;
    public string ModuleTitle { get; set; }= null;
    public string ModuleScopingKey { get; set; } = null;
    public List<WorkspaceDescription> Workspaces { get; set; }= new List<WorkspaceDescription>();
    public List<UsecaseDescription> Usecases { get; set; }= new List<UsecaseDescription>();
    public List<StaticUsecaseAssignment> StaticUsecaseAssignments { get; set; }= new List<StaticUsecaseAssignment>();
    public List<DatasourceDescription> Datasources { get; set; }= new List<DatasourceDescription>();
    public List<ServiceDescription> Services { get; set; }= new List<ServiceDescription>();
    public List<DatastoreDescription> Datastores { get; set; }= new List<DatastoreDescription>();
    public List<CommandDescription> Commands { get; set; } = new List<CommandDescription>();

    public static ModuleDescription Build(string moduleTitle, Action<ModuleDescription> customizingMethod = null) {
      var instance = new ModuleDescription { ModuleTitle = moduleTitle };
      if (customizingMethod != null) customizingMethod.Invoke(instance);
      return instance;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static ModuleDescription FromEmbeddedResFile(string fileNameWithNamespace, Assembly assembly = null) {
      if (assembly == null) assembly = Assembly.GetCallingAssembly();
      using (Stream stream = assembly.GetManifestResourceStream(fileNameWithNamespace)) {
        if (stream == null) {
          throw new Exception($"Embedded resource '{fileNameWithNamespace}' not found.");
        }
        using (StreamReader reader = new StreamReader(stream)) {
          string rawJson = reader.ReadToEnd();
          return Newtonsoft.Json.JsonConvert.DeserializeObject<ModuleDescription>(rawJson);
        }
      }
    }

  }

}
