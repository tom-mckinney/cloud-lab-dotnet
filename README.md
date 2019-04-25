# Cloud Lab - Dotnet

```
donet new sln -n Kitchen
```
```
dotnet new web --output Kitchen.Api
```
```
dotnet new xunit --output Kitchen.Test
```
```
dotnet sln add Kitchen.Api
```
```
dotnet sln add Kitchen.Test
```

## Setup Kitchen.Test

```
dotnet add reference ../Kitchen.Api/Kitchen.Api.csproj
```

```
dotnet add package Microsoft.EntityFrameworkCore.InMemory
```

## Setup Kitchen.Api

#### Cloud Foundry Actuators
```
dotnet add package Steeltoe.Management.CloudFoundryCore
```
```
dotnet add package Steeltoe.Extensions.Configuration.CloudFoundryCore
```

#### Database Service Connectors
```
dotnet add package Steeltoe.CloudFoundry.Connector.EFCore
```
```
dotnet add package Pomelo.EntityFrameworkCore.MySql
```
