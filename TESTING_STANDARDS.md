# Testing Standards & Architecture Guide

This document defines the testing standards, patterns, and architectural decisions used in this test automation framework. It serves as a reference for maintaining consistency and as scaffolding guidance when adapting this project for testing other applications.

---

## Table of Contents

1. [Project Structure](#project-structure)
2. [Architecture Overview](#architecture-overview)
3. [Dependency Injection (BoDi)](#dependency-injection-bodi)
4. [Page Object Model](#page-object-model)
5. [Component-Based Design](#component-based-design)
6. [API Client Layer](#api-client-layer)
7. [Data Management](#data-management)
8. [Gherkin & Step Definitions](#gherkin--step-definitions)
9. [Hooks & Lifecycle](#hooks--lifecycle)
10. [Configuration Management](#configuration-management)
11. [Extensions & Utilities](#extensions--utilities)
12. [Locator Strategy](#locator-strategy)
13. [Assertions](#assertions)
14. [Tracing & Debugging](#tracing--debugging)
15. [Naming Conventions](#naming-conventions)

---

## Project Structure

```
Solution/
├── TAEssentials.BE/                    # Backend/API layer
│   ├── Clients/                        # API clients (RestSharp-based)
│   ├── DTOs/
│   │   ├── Requests/                   # Request payloads
│   │   └── Responses/                  # Response models
│   ├── Enums/                          # Shared enumerations
│   └── Models/                         # Domain models
│
├── TAEssentials.UI/                    # UI test layer
│   ├── Constants/                      # Static constants (e.g., ScenarioContext keys)
│   ├── DataClasses/
│   │   ├── BusinessModels/             # Test data models
│   │   ├── Configuration/              # Configuration POCOs
│   │   └── RepresentativeData/         # Enums for test data
│   ├── Extensions/                     # Extension methods
│   ├── Features/                       # Gherkin .feature files
│   ├── Hooks/                          # Reqnroll hooks (lifecycle)
│   ├── PageObjects/
│   │   ├── Components/                 # Reusable UI components
│   │   └── Pages/                      # Page objects
│   ├── StepDefinitions/
│   │   └── DataPreparation/            # API-based setup steps
│   └── Utils/                          # Utility classes
│
└── appsettings.json                    # Configuration file
```

**Key Principle:** Separation of concerns between BE (API communication) and UI (browser automation) layers.

---

## Architecture Overview

```
┌─────────────────────────────────────────────────────────────────┐
│                      Feature Files (.feature)                    │
├─────────────────────────────────────────────────────────────────┤
│                      Step Definitions                            │
│   ┌─────────────────┐    ┌─────────────────────────────────┐   │
│   │ DataPreparation │    │     UI Interaction Steps        │   │
│   │   (API Steps)   │    │                                 │   │
│   └────────┬────────┘    └────────────────┬────────────────┘   │
├────────────┼──────────────────────────────┼────────────────────┤
│            ▼                              ▼                     │
│   ┌─────────────────┐           ┌─────────────────┐            │
│   │   API Clients   │           │   Page Objects  │            │
│   │   (RestSharp)   │           │   (Playwright)  │            │
│   └────────┬────────┘           └────────┬────────┘            │
│            │                             │                      │
│            │                    ┌────────┴────────┐            │
│            │                    │   Components    │            │
│            │                    │  (Reusable UI)  │            │
│            │                    └─────────────────┘            │
├────────────┴─────────────────────────────┴─────────────────────┤
│                    BoDi (Dependency Injection)                  │
├─────────────────────────────────────────────────────────────────┤
│                    Hooks (Lifecycle Management)                 │
└─────────────────────────────────────────────────────────────────┘
```

---

## Dependency Injection (BoDi)

BoDi is the DI container used by Reqnroll. All dependencies are resolved through constructor injection.

### Registration Patterns

```csharp
// In GlobalHooks.cs [BeforeScenario]

// 1. Instance registration (singleton per scenario)
objectContainer.RegisterInstanceAs(page);

// 2. Factory registration (for scoped components)
objectContainer.RegisterInstanceAs<Func<ILocator, ProductCard>>(loc => new ProductCard(loc));

// 3. Type registration
objectContainer.RegisterTypeAs<Implementation, IInterface>();
```

### Factory Pattern for Scoped Components

When a component needs to be created multiple times with different contexts (e.g., multiple product cards on a page), use factory delegates:

```csharp
// Registration
objectContainer.RegisterInstanceAs<Func<ILocator, ProductCard>>(loc => new ProductCard(loc));

// Usage in Page/Component
public class ProductsGrid
{
    private readonly Func<ILocator, ProductCard> _productCardFactory;
    
    public ProductsGrid(IPage page, Func<ILocator, ProductCard> productCardFactory)
    {
        _productCardFactory = productCardFactory;
    }
    
    public ProductCard GetProductCardByName(string name)
    {
        var container = _page.Locator("[data-testid^='product-card']")
            .Filter(new LocatorFilterOptions { HasText = name })
            .First;
        return _productCardFactory(container);
    }
}
```

### Benefits
- No `new` keyword in production code
- Easier mocking/stubbing for unit tests
- Clear dependency graph visible in constructors
- Centralized object lifecycle management

---

## Page Object Model

### Structure

```csharp
public class ProductsPage
{
    private readonly IPage _page;

    // Compose with components
    public FiltersBar FiltersBar { get; private set; }
    public ProductsGrid ProductsGrid { get; private set; }

    public ProductsPage(IPage page, FiltersBar filtersBar, ProductsGrid productsGrid)
    {
        _page = page;
        FiltersBar = filtersBar;
        ProductsGrid = productsGrid;
    }
}
```

### Rules
1. **Constructor Injection** - All dependencies via constructor
2. **Composition over Inheritance** - Pages compose components
3. **No Business Logic** - Pages only expose locators and simple actions
4. **Readonly IPage** - Store `IPage` as private readonly field

---

## Component-Based Design

Components represent reusable UI elements that appear across multiple pages.

### Page-Scoped Components

Components that use `IPage` and can locate themselves:

```csharp
public class HeaderBar
{
    private readonly IPage _page;

    public ILocator HeaderLogo => _page.GetByTestId("header-logo");
    public ILocator ProductsNavButton => _page.GetByTestId("nav-products");

    public HeaderBar(IPage page)
    {
        _page = page;
    }
}
```

### Container-Scoped Components

Components that work within a specific container (for lists/grids):

```csharp
public class ProductCard
{
    private readonly ILocator _container;

    public ILocator ProductName => _container.GetByTestId("product-name");
    public ILocator ProductPrice => _container.GetByTestId("product-price");
    public ILocator ViewDetailsButton => _container.GetByTestId("product-view-button");

    public ProductCard(ILocator container)
    {
        _container = container;
    }
}
```

### Filtering Pattern

When finding a specific item in a list:

```csharp
public ProductCard GetProductCardByName(string productName)
{
    var cardContainer = _page
        .Locator("[data-testid^='product-card']")
        .Filter(new LocatorFilterOptions 
        { 
            Has = _page.GetByTestId("product-name")
                       .Filter(new LocatorFilterOptions { HasText = productName })
        })
        .First;

    return _productCardFactory(cardContainer);
}
```

---

## API Client Layer

### BaseClient Pattern

```csharp
public abstract class BaseClient
{
    protected RestClient _restClient;
    
    protected abstract Task Authorize();
    
    protected async Task<RestResponse<T>> GetAsync<T>(RestRequest request) { ... }
    protected async Task<RestResponse<T>> PostAsync<T>(RestRequest request) { ... }
    protected async Task<RestResponse<T>> PutAsync<T>(RestRequest request) { ... }
    protected async Task<RestResponse<T>> DeleteAsync<T>(RestRequest request) { ... }
    protected async Task<RestResponse<T>> PatchAsync<T>(RestRequest request) { ... }
}
```

### Domain-Specific Clients

```csharp
public class ProductsClient : BaseClient
{
    public ProductsClient()
    {
        _restClient = new RestClient(BaseUrl);
    }

    public void SetAuthToken(string token)
    {
        _authToken = token;
    }

    public async Task<RestResponse<Product>> CreateProductAsync(Product product)
    {
        var request = new RestRequest("/api/Products", Method.Post);
        AddAuthHeader(request);
        request.AddJsonBody(product);
        return await PostAsync<Product>(request);
    }
}
```

### DTOs Organization

```
DTOs/
├── Requests/       # Input payloads (CreateProductRequest, LoginRequest)
└── Responses/      # API responses (LoginResponse, RegisterResponse)
```

---

## Data Management

### DataGenerator (Bogus)

Centralized fake data generation using Bogus library:

```csharp
public class DataGenerator
{
    public User FakeUser => GenerateFakeUsers(1).Single();
    public Book FakeBook => GenerateFakeBooks(1).Single();
    
    private List<User> GenerateFakeUsers(int count)
    {
        return new Faker<User>()
            .RuleFor(u => u.Username, f => f.Internet.UserName())
            .RuleFor(u => u.Password, f => f.Internet.Password())
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .Generate(count);
    }
}
```

### ScenarioContext for Data Sharing

Use typed constants for ScenarioContext keys:

```csharp
public static class ScenarioContextConstants
{
    public const string User = nameof(User);
    public const string Book = nameof(Book);
}

// Setting
_scenarioContext.Add(ScenarioContextConstants.User, user);

// Getting
var user = _scenarioContext.Get<User>(ScenarioContextConstants.User);
```

---

## Gherkin & Step Definitions

### Feature File Structure

```gherkin
Feature: Book Reviews

    Background:
        Given User navigates to the application
        And API: New Standard User is exists in the system
        And API: New Book is exists in the system
        And User is logged in as Standard User
    
    Scenario: Standard User can add a book review
        When User clicks Products Nav Button in order to navigate to Products Page
        And User searches New Book by its title
        ...
```

### Step Definition Categories

| Category | Purpose | Location |
|----------|---------|----------|
| DataPreparation | API-based test setup | `StepDefinitions/DataPreparation/` |
| Navigation | Page navigation actions | `StepDefinitions/NavigationSteps.cs` |
| Page-specific | Page interactions/assertions | `StepDefinitions/{Page}Steps.cs` |

### Naming Convention

- **API Steps**: Prefix with `API:` → `Given API: New Standard User is exists in the system`
- **UI Steps**: Descriptive action → `When User clicks on View Details button`

---

## Hooks & Lifecycle

### Hook Order

```csharp
[BeforeTestRun(Order = 0)]  // Get admin token
[BeforeTestRun(Order = 1)]  // Initialize Playwright, Browser

[BeforeScenario(Order = 1)] // Register components/factories
[BeforeScenario(Order = 2)] // Create context, page
[BeforeScenario(Order = 3)] // Start tracing

[AfterScenario(Order = 1)]  // Screenshot on failure, close page
[AfterScenario(Order = 2)]  // Stop tracing

[AfterTestRun]              // Close browser
```

### Browser Lifecycle

```
TestRun
├── Browser (shared)
└── Scenario
    ├── Context (isolated)
    └── Page (isolated)
```

---

## Configuration Management

### appsettings.json

```json
{
    "Configuration": {
        "BrowserSettings": {
            "Headless": "false",
            "Channel": "chrome",
            "SlowMo": "100",
            "DefaultExpectedTimeout": "5000"
        },
        "TraceSettings": {
            "EnableTracing": "true",
            "Screenshots": "true",
            "Snapshots": "true",
            "Sources": "true"
        }
    }
}
```

### ConfigurationProvider

```csharp
public static class ConfigurationProvider
{
    public static Configuration GetConfiguration()
    {
        return GetConfigurationFile<Configuration>("appsettings.json", "Configuration");
    }
}
```

---

## Extensions & Utilities

### RestResponse Assertions (FluentAssertions)

```csharp
public class RestResponseAssertions : ReferenceTypeAssertions<RestResponse, RestResponseAssertions>
{
    public AndConstraint<RestResponseAssertions> HaveHttpStatusCode(HttpStatusCode expected)
    {
        Execute.Assertion
            .Given(() => Subject)
            .ForCondition(response => response.StatusCode.Equals(expected))
            .FailWith("Expected {0}, found {1}. URI: {2}, Content: {3}",
                _ => expected,
                r => r.StatusCode,
                r => r.ResponseUri,
                r => r.Content);
        return new AndConstraint<RestResponseAssertions>(this);
    }
}

// Usage
response.Should().HaveHttpStatusCode(HttpStatusCode.Created);
```

### Page Extensions

```csharp
public static class PageExtensions
{
    public static async Task WaitUntilIsLoadedAsync(this IPage page, ILocator? container = null)
    {
        var spinner = container == null 
            ? page.GetByTestId("spinner-icon")
            : container.GetByTestId("spinner-icon");
        
        await spinner.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Detached
        });
    }
}
```

---

## Locator Strategy

### Primary: data-testid

Always prefer `data-testid` attributes:

```csharp
// Good
_page.GetByTestId("login-button")
_container.GetByTestId("product-name")

// For dynamic IDs
_page.Locator("[data-testid^='product-card-']")  // starts with
```

### Filtering Pattern

```csharp
var card = _page
    .Locator("[data-testid^='product-card']")
    .Filter(new LocatorFilterOptions 
    { 
        Has = _page.GetByTestId("product-name")
                   .Filter(new LocatorFilterOptions { HasText = searchText })
    })
    .First;
```

### Scoped Locators

When working within a container, all locators should be relative:

```csharp
public class ProductCard
{
    private readonly ILocator _container;
    
    // Relative to container
    public ILocator Name => _container.GetByTestId("product-name");
}
```

---

## Assertions

### UI Assertions (Playwright)

```csharp
await Assertions.Expect(locator).ToBeVisibleAsync();
await Assertions.Expect(locator).ToHaveTextAsync("expected text");
await Assertions.Expect(locator).ToBeEnabledAsync();
```

### API Assertions (FluentAssertions)

```csharp
response.Should().HaveHttpStatusCode(HttpStatusCode.OK);
response.Data.Should().NotBeNull();
actualValue.Should().Be(expectedValue);
```

---

## Tracing & Debugging

### Automatic Tracing on Failure

```csharp
[BeforeScenario]
public async Task StartTracingAsync(IPage page)
{
    await page.Context.Tracing.StartAsync(new TracingStartOptions
    {
        Title = _scenarioContext.ScenarioInfo.Title,
        Screenshots = true,
        Snapshots = true,
        Sources = true
    });
}

[AfterScenario]
public async Task StopTracingAsync(IPage page)
{
    if (isFailure)
    {
        var tracePath = $"playwright-traces/Trace_{scenarioTitle}.zip";
        await page.Context.Tracing.StopAsync(new TracingStopOptions { Path = tracePath });
        TestContext.AddTestAttachment(tracePath);
    }
}
```

### Screenshot on Failure

```csharp
private async Task ScreenshotAsync(IPage page)
{
    if (!isSuccess)
    {
        var path = $"{scenarioTitle}.png";
        await page.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = path,
            FullPage = true
        });
        TestContext.AddTestAttachment(path);
    }
}
```

---

## Naming Conventions

| Element | Convention | Example |
|---------|------------|---------|
| Feature file | PascalCase | `BookReviews.feature` |
| Step definition class | `{Domain}Steps` | `ProductPageSteps.cs` |
| Page object | `{Page}Page` | `ProductsPage.cs` |
| Component | Descriptive noun | `FiltersBar.cs`, `ProductCard.cs` |
| API client | `{Domain}Client` | `ProductsClient.cs` |
| DTO Request | `{Action}{Domain}Request` | `CreateCouponRequest.cs` |
| DTO Response | `{Action}Response` | `LoginResponse.cs` |
| Extension class | `{Type}Extensions` | `PageExtensions.cs` |
| Constants class | `{Domain}Constants` | `ScenarioContextConstants.cs` |

---

## Quick Reference Checklist

When adapting this framework for a new application:

- [ ] Update `appsettings.json` with target URLs and browser settings
- [ ] Create DTOs matching the new API contracts
- [ ] Create API clients extending `BaseClient`
- [ ] Define Page Objects for each page
- [ ] Extract reusable Components
- [ ] Register factories in `GlobalHooks.RegisterFactories()`
- [ ] Update `DataGenerator` with domain-specific fake data
- [ ] Define `ScenarioContextConstants` for shared data keys
- [ ] Create feature files following Background + Scenario pattern
- [ ] Implement DataPreparation steps for API-based setup

---

*Last updated: December 2025*
