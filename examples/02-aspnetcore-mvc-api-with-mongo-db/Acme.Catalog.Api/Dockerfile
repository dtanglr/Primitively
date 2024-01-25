#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0.1-alpine3.19-amd64 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0.1-alpine3.19-amd64 AS build
WORKDIR /src
COPY ["Acme.Catalog.Api/Acme.Catalog.Api.csproj", "Acme.Catalog.Api/"]
RUN dotnet restore "Acme.Catalog.Api/Acme.Catalog.Api.csproj"
COPY . .
WORKDIR "/src/Acme.Catalog.Api"
RUN dotnet build "Acme.Catalog.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Acme.Catalog.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Acme.Catalog.Api.dll"]