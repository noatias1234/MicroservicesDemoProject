#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MapEntitiesService/MapEntitiesService.csproj", "MapEntitiesService/"]
COPY ["MapEntitiesService.Core/MapEntitiesService.Core.csproj", "MapEntitiesService.Core/"]
COPY ["MessageBroker.Core/MessageBroker.Core.csproj", "MessageBroker.Core/"]
COPY ["MapEntitiesService.Infrastructure/MapEntitiesService.Infrastructure.csproj", "MapEntitiesService.Infrastructure/"]
COPY ["MessageBroker.Infrastructure/MessageBroker.Infrastructure.csproj", "MessageBroker.Infrastructure/"]
RUN dotnet restore "MapEntitiesService/MapEntitiesService.csproj"
COPY . .
WORKDIR "/src/MapEntitiesService"
RUN dotnet build "MapEntitiesService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MapEntitiesService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MapEntitiesService.dll"]