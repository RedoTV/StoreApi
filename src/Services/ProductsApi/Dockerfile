FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ProductsApi.csproj ProductsApi.csproj
RUN dotnet restore ProductsApi.csproj
COPY . .
WORKDIR /src
RUN dotnet build ProductsApi.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish ProductsApi.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProductsApi.dll"]
