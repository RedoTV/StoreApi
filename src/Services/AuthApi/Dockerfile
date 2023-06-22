FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY AuthApi.csproj AuthApi.csproj
RUN dotnet restore AuthApi.csproj
COPY . .
WORKDIR /src
RUN dotnet build AuthApi.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish AuthApi.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT [ "dotnet" , "AuthApi.dll"]