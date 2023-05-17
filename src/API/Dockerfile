FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /
COPY ["src/API/API.csproj", "src/API/"]
COPY ["src/Shared/Shared.Infrastructure/Shared.Infrastructure.csproj", "src/Shared/Shared.Infrastructure/"]
COPY ["src/Shared/Shared.Core/Shared.Core.csproj", "src/Shared/Shared.Core/"]
COPY ["src/Shared/Shared.Model/Shared.Model.csproj", "src/Shared/Shared.Model/"]
COPY ["src/Modules/Catalog/Modules.Catalog/Modules.Catalog.csproj", "src/Modules/Catalog/Modules.Catalog/"]
COPY ["src/Modules/Catalog/Modules.Catalog.Core/Modules.Catalog.Core.csproj", "src/Modules/Catalog/Modules.Catalog.Core/"]
COPY ["src/Modules/Catalog/Modules.Catalog.Infrastructure/Modules.Catalog.Infrastructure.csproj", "src/Modules/Catalog/Modules.Catalog.Infrastructure/"]
COPY ["src/Modules/Identity/Modules.Identity/Modules.Identity.csproj", "src/Modules/Identity/Modules.Identity/"]
COPY ["src/Modules/Identity/Modules.Identity.Infrastructure/Modules.Identity.Infrastructure.csproj", "src/Modules/Identity/Modules.Identity.Infrastructure/"]
COPY ["src/Modules/Identity/Modules.Identity.Core/Modules.Identity.Core.csproj", "src/Modules/Identity/Modules.Identity.Core/"]
RUN dotnet restore "src/API/API.csproj"
COPY . .
WORKDIR "/src/API"
RUN dotnet build "API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]
