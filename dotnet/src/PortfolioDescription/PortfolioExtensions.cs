using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;

namespace UShell {
  public static class PortfolioExtensions {

    // Public for testability
    public static Func<HttpClient> HttpClientFactory { get; set; } = () => new HttpClient();

    public static ModuleDescription LoadAggregatedModuleDescription(this PortfolioDescription portfolio) {
      if (portfolio == null) {
        throw new ArgumentNullException(nameof(portfolio), "Portfolio cannot be null.");
      }

      ModuleDescription aggregated = new ModuleDescription() {
        ModuleTitle = portfolio.ApplicationTitle ?? "Aggregated Module",
        ModuleUid = portfolio.ApplicationTitle?.Replace(" ", "-").ToLowerInvariant() ?? "aggregated-module",        
      };

      foreach (string url in portfolio.ModuleDescriptionUrls ?? Array.Empty<string>()) {
        if (string.IsNullOrWhiteSpace(url)) continue;

        ModuleDescription loaded = null;
        try {
          using (HttpClient http = HttpClientFactory()) {
            HttpResponseMessage response = http.GetAsync(url).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();
            string json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            loaded = JsonSerializer.Deserialize<ModuleDescription>(json, new JsonSerializerOptions {
              PropertyNameCaseInsensitive = true
            });
          }
        }
        catch (Exception) {
        }
        if (loaded == null) continue;
        // Merge all collections
        aggregated.Workspaces.AddRange(loaded.Workspaces ?? new List<WorkspaceDescription>());
        aggregated.Usecases.AddRange(loaded.Usecases ?? new List<UsecaseDescription>());
        aggregated.StaticUsecaseAssignments.AddRange(loaded.StaticUsecaseAssignments ?? new List<StaticUsecaseAssignment>());
        aggregated.Datasources.AddRange(loaded.Datasources ?? new List<DatasourceDescription>());
        aggregated.Services.AddRange(loaded.Services ?? new List<ServiceDescription>());
        aggregated.Datastores.AddRange(loaded.Datastores ?? new List<DatastoreDescription>());
        aggregated.Commands.AddRange(loaded.Commands ?? new List<CommandDescription>());
      }

      return aggregated;
    }
  }
}
