#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NotificationsService/NotificationsService.csproj", "NotificationsService/"]
COPY ["MessageBroker.Infrastructure/MessageBroker.Infrastructure.csproj", "MessageBroker.Infrastructure/"]
COPY ["MapEntitiesService.Core/MapEntitiesService.Core.csproj", "MapEntitiesService.Core/"]
COPY ["MessageBroker.Core/MessageBroker.Core.csproj", "MessageBroker.Core/"]
RUN dotnet restore "NotificationsService/NotificationsService.csproj"
COPY . .
WORKDIR "/src/NotificationsService"
RUN dotnet build "NotificationsService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NotificationsService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotificationsService.dll"]