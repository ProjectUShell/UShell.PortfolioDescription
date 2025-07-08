using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using UShell;

namespace UShell.Tests {
  [TestClass]
  public sealed class PortfolioExtensionsTests {

    private class MockHttpMessageHandler : HttpMessageHandler {
      private readonly Dictionary<string, string> _Responses;

      public MockHttpMessageHandler(Dictionary<string, string> responses) {
        _Responses = responses;
      }

      protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
        string url = request.RequestUri.ToString();
        if (_Responses.TryGetValue(url, out string json)) {
          return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK) {
            Content = new StringContent(json)
          });
        }
        return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound));
      }
    }

    private static string SerializeModule(ModuleDescription module) {
      return JsonSerializer.Serialize(module, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
    }

    [TestInitialize]
    public void TestInit() {
      // Reset to default before each test
      UShell.PortfolioExtensions.HttpClientFactory = () => new HttpClient();
    }

    [TestCleanup]
    public void TestCleanup() {
      // Reset to default after each test to ensure isolation
      UShell.PortfolioExtensions.HttpClientFactory = () => new HttpClient();
    }

    [TestMethod]
    public void LoadAggregatedModuleDescription_MergesAllCollections() {
      // Arrange
      string url1 = "http://test/module1.json";
      string url2 = "http://test/module2.json";

      ModuleDescription mod1 = new ModuleDescription {
        Workspaces = new List<WorkspaceDescription> { new WorkspaceDescription { WorkspaceKey = "ws1", WorkspaceTitle = "WS1" } },
        Usecases = new List<UsecaseDescription> { new UsecaseDescription { UsecaseKey = "uc1", Title = "UC1" } },
        StaticUsecaseAssignments = new List<StaticUsecaseAssignment> { new StaticUsecaseAssignment { UsecaseKey = "uc1", TargetWorkspaceKey = "ws1" } },
        Datasources = new List<DatasourceDescription> { new DatasourceDescription { DatasourceUid = "ds1", ProviderClass = "prov1" } },
        Services = new List<ServiceDescription> { new ServiceDescription { ServiceUid = "svc1", ServiceName = "Service1" } },
        Datastores = new List<DatastoreDescription> { new DatastoreDescription { Key = "store1", ProviderClass = "fuse" } },
        Commands = new List<CommandDescription> { new CommandDescription { UniqueCommandKey = "cmd1", Label = "Cmd1" } }
      };
      ModuleDescription mod2 = new ModuleDescription {
        Workspaces = new List<WorkspaceDescription> { new WorkspaceDescription { WorkspaceKey = "ws2", WorkspaceTitle = "WS2" } },
        Usecases = new List<UsecaseDescription> { new UsecaseDescription { UsecaseKey = "uc2", Title = "UC2" } },
        StaticUsecaseAssignments = new List<StaticUsecaseAssignment> { new StaticUsecaseAssignment { UsecaseKey = "uc2", TargetWorkspaceKey = "ws2" } },
        Datasources = new List<DatasourceDescription> { new DatasourceDescription { DatasourceUid = "ds2", ProviderClass = "prov2" } },
        Services = new List<ServiceDescription> { new ServiceDescription { ServiceUid = "svc2", ServiceName = "Service2" } },
        Datastores = new List<DatastoreDescription> { new DatastoreDescription { Key = "store2", ProviderClass = "fuse" } },
        Commands = new List<CommandDescription> { new CommandDescription { UniqueCommandKey = "cmd2", Label = "Cmd2" } }
      };

      Dictionary<string, string> responses = new()
      {
        { url1, SerializeModule(mod1) },
        { url2, SerializeModule(mod2) }
      };

      // Use the mock handler for all HTTP requests in PortfolioExtensions
      UShell.PortfolioExtensions.HttpClientFactory = () => new HttpClient(new MockHttpMessageHandler(responses));

      PortfolioDescription portfolio = new PortfolioDescription {
        ApplicationTitle = "Test Portfolio",
        ModuleDescriptionUrls = new[] { url1, url2 }
      };

      // Act
      ModuleDescription aggregated = portfolio.LoadAggregatedModuleDescription();

      // Assert
      Assert.AreEqual("Test Portfolio", aggregated.ModuleTitle);
      Assert.AreEqual("test-portfolio", aggregated.ModuleUid);
      Assert.AreEqual(2, aggregated.Workspaces.Count);
      Assert.AreEqual(2, aggregated.Usecases.Count);
      Assert.AreEqual(2, aggregated.StaticUsecaseAssignments.Count);
      Assert.AreEqual(2, aggregated.Datasources.Count);
      Assert.AreEqual(2, aggregated.Services.Count);
      Assert.AreEqual(2, aggregated.Datastores.Count);
      Assert.AreEqual(2, aggregated.Commands.Count);
    }

    [TestMethod]
    public void LoadAggregatedModuleDescription_HandlesEmptyUrls() {

      PortfolioDescription portfolio = new PortfolioDescription {
        ApplicationTitle = "EmptyUrls",
        ModuleDescriptionUrls = Array.Empty<string>()
      };

      ModuleDescription aggregated = portfolio.LoadAggregatedModuleDescription();

      Assert.AreEqual("EmptyUrls", aggregated.ModuleTitle);
      Assert.AreEqual("emptyurls", aggregated.ModuleUid);
      Assert.AreEqual(0, aggregated.Workspaces.Count);
      Assert.AreEqual(0, aggregated.Usecases.Count);
      Assert.AreEqual(0, aggregated.StaticUsecaseAssignments.Count);
      Assert.AreEqual(0, aggregated.Datasources.Count);
      Assert.AreEqual(0, aggregated.Services.Count);
      Assert.AreEqual(0, aggregated.Datastores.Count);
      Assert.AreEqual(0, aggregated.Commands.Count);
    }

    [TestMethod]
    public void LoadAggregatedModuleDescription_HandlesNullPortfolio_Throws() {
      Assert.ThrowsException<ArgumentNullException>(() => PortfolioExtensions.LoadAggregatedModuleDescription(null));
    }

    [TestMethod]
    public void LoadAggregatedModuleDescription_HandlesHttpErrorAndInvalidJson() {
      string url1 = "http://test/module1.json";
      string url2 = "http://test/module2.json";
      string url3 = "http://test/module3.json";

      ModuleDescription mod1 = new ModuleDescription {
        Workspaces = new List<WorkspaceDescription> { new WorkspaceDescription { WorkspaceKey = "ws1", WorkspaceTitle = "WS1" } }
      };

      Dictionary<string, string> responses = new()
      {
        { url1, SerializeModule(mod1) },
        { url2, "INVALID_JSON" }
        // url3 is missing to simulate 404
      };

      UShell.PortfolioExtensions.HttpClientFactory = () => new HttpClient(new MockHttpMessageHandler(responses));

      PortfolioDescription portfolio = new PortfolioDescription {
        ApplicationTitle = "Partial",
        ModuleDescriptionUrls = new[] { url1, url2, url3 }
      };

      ModuleDescription aggregated = portfolio.LoadAggregatedModuleDescription();

      Assert.AreEqual(1, aggregated.Workspaces.Count);
    }
  }
}
