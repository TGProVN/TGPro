# Commands

## Build

```bash
dotnet restore
dotnet build
```

## Migration

```bash
dotnet tool install --global dotnet-ef
dotnet restore
cd src
dotnet ef migrations add "Initial Catalog" -s ./API -p ./Modules/Catalog/Modules.Catalog.Infrastructure -c YOUR_DBCONTEXT
dotnet ef migrations add "Initial Identity" -s ./API -p ./Modules/Identity/Modules.Identity.Infrastructure -c YOUR_DBCONTEXT
```

## Docker

```bash
dotnet dev-certs https -ep PATH
dotnet dev-certs https --trust
docker-compose up --build -d
```
