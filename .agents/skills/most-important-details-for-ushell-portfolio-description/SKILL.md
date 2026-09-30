---
name: most-important-details-for-ushell-portfolio-description
description: Mandatory compact entry skill for UShell.PortfolioDescription, the U-Shell portfolio/module/workspace/use-case description contract repository. Use before changing code, project files or docs in this repository.
---

# Most important details for UShell.PortfolioDescription

## Repository purpose

UShell.PortfolioDescription is the central contract/model repository for declarative U-Shell application descriptions. It defines portfolio, module, workspace, use-case, command, service, datasource, datastore, authentication, ApplicationScope, and dynamic parameter mapping structures.

## Mandatory related knowledge

- Always read this compact skill first for repository-specific orientation.
- For global U-Shell desired-state architecture, read `UShell.ReactFrontend/.agents/skills/ushell/SKILL.md` when available locally.
- The architecture skill is leading for desired-state U-Shell concepts; this repository is leading for productive .NET/JS description contract shape.

## Code structure

- Solution: `dotnet/UShell.PortfolioDescription.sln`.
- .NET shared-project source: `dotnet/src/PortfolioDescription` and `dotnet/src/PortfolioHosting.Mvc`.
- Target projects observed: `net48`, `net8.0`, and `net10.0` for description contracts; `net8.0` and `net10.0` for MVC hosting.
- JS/TS package: `js/package.json` with package name `ushell-portfoliodescription`.
- Tests: `dotnet/test/UShell.PortfolioDescription.Tests`.
- Important types include `PortfolioDescription`, `PortfolioEntry`, `ModuleDescription`, `WorkspaceDescription`, `UsecaseDescription`, `CommandDescription`, `ApplicationScopeEntry`, `ApplicationScopeValueConstraint`, `DatasourceDescription`, `DatastoreDescription`, `ServiceDescription`, `AuthTokenConfig`, `StaticPortfolioService`, and `FluentBuildupExtensions`.

## Rules and pitfalls

- Treat public description objects as cross-repository contract surface used by ReactFrontend, ModuleBase, CommonComponents, hosting, demos, and generators.
- Keep .NET and JS/TS contract representations aligned.
- Source lives in shared projects; when adding `.cs` files, update matching `.projitems` files.
- Do not invent parallel schema/portfolio concepts without checking the global `ushell` architecture skill.

## Cross-repository map

Primary local U-Shell repositories and clone URLs:

| Repository | Role | Clone URL |
| --- | --- | --- |
| `UShell.PortfolioDescription` | Productive description contracts for portfolios, modules, workspaces, use cases, commands, ApplicationScope, dynamic mappings, and hosting helpers. | `https://github.com/ProjectUShell/UShell.PortfolioDescription.git` |
| `UShell.ReactFrontend` | Productive leading React runtime for portfolio loading, workspace/use-case runtime, mapDynamic, federation, menu, scope, and authentication integration. | `https://github.com/ProjectUShell/UShell.ReactFrontend.git` |
| `UShell.ModuleBase.JS` | JavaScript/TypeScript module integration contracts, widget contracts, WidgetHost contracts, datasource/datastore abstractions, and runtime concepts. | `https://github.com/ProjectUShell/UShell.ModuleBase.JS.git` |
| `UShell.CommonComponents.JS` | Generic reusable UI components/widgets, CRUD/data UI, datasource helpers, and standard building blocks for Configuration First. | `https://github.com/ProjectUShell/UShell.CommonComponents.JS.git` |
| `UShell.ServerCommands.Contract` | Contracts and ASP.NET Core integration for server-executed commands/backend actions. | `https://github.com/ProjectUShell/UShell.ServerCommands.Contract.git` |
| `UShell.FrontendBundle` | .NET/ASP.NET Core packaging and hosting for the frontend bundle. | `https://github.com/ProjectUShell/UShell.FrontendBundle.git` |
| `UShell.DemoApp` | Sample module/application and backend; useful as usage evidence, not as architecture authority. | `https://github.com/ProjectUShell/UShell.DemoApp.git` |
| `UShell.ReactFrontend.CefWrapper` | Windows/CEF offline wrapper concept for hosting the React frontend as a desktop application. | `https://github.com/ProjectUShell/UShell.ReactFrontend.CefWrapper` |
| `UShell.Resources` | Internal resources and branding assets; not a runtime architecture source of truth. | `https://github.com/ProjectUShell/--internal-resources--.git` |

When a task references another U-Shell package, first check whether the sibling repository exists locally, then read its `most-important-details-for-ushell-*` skill before inspecting README, docs, solution, project, or package files. Suggest cloning a missing U-Shell repository only when its source details are necessary for the task.
