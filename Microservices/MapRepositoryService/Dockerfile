#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MapRepositoryService/MapRepositoryService.csproj", "MapRepositoryService/"]
COPY ["MapRepositoryService.Infrastructure/MapRepositoryService.Infrastructure.csproj", "MapRepositoryService.Infrastructure/"]
COPY ["MapRepositoryService.Core/MapRepositoryService.Core.csproj", "MapRepositoryService.Core/"]
COPY ["MessageBroker.Core/MessageBroker.Core.csproj", "MessageBroker.Core/"]
RUN dotnet restore "MapRepositoryService/MapRepositoryService.csproj"
COPY . .
WORKDIR "/src/MapRepositoryService"
RUN dotnet build "MapRepositoryService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MapRepositoryService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MapRepositoryService.dll"]