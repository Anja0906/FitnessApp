FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FitnessApp/FitnessApp.WebApi.csproj", "FitnessApp/"]
COPY ["FitnessApp.Application/FitnessApp.Application.csproj", "FitnessApp.Application/"]
COPY ["FitnessApp.Domain/FitnessApp.Domain.csproj", "FitnessApp.Domain/"]
COPY ["FitnessApp.Infrastructure/FitnessApp.Infrastructure.csproj", "FitnessApp.Infrastructure/"]
RUN dotnet restore "./FitnessApp/FitnessApp.WebApi.csproj"
COPY . .
WORKDIR "/src/FitnessApp"
RUN dotnet build "./FitnessApp.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FitnessApp.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "FitnessApp.WebApi.dll"]