#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["multi-tenant.Api/multi-tenant.Api/multi-tenant.Api.csproj", "multi-tenant.Api/multi-tenant.Api/"]
COPY ["multi-tenant.Api/multi-tenant.Managers/multi-tenant.Managers.csproj", "multi-tenant.Api/multi-tenant.Managers/"]
COPY ["multi-tenant.Api/multi-tenant.Repositories/multi-tenant.Repositories.csproj", "multi-tenant.Api/multi-tenant.Repositories/"]
COPY ["multi-tenant.Api/multi-tenant.Models/multi-tenant.Models.csproj", "multi-tenant.Api/multi-tenant.Models/"]
RUN dotnet restore "multi-tenant.Api/multi-tenant.Api/multi-tenant.Api.csproj"
COPY . .
WORKDIR "/src/multi-tenant.Api/multi-tenant.Api"
RUN dotnet build "multi-tenant.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "multi-tenant.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "multi-tenant.Api.dll"]