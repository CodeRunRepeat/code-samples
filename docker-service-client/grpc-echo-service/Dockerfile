# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM base AS debug
ENV DOTNET_ENVIRONMENT=Development

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["grpc-echo-service/grpc-echo-service.csproj", "grpc-echo-service/"]
COPY ["data-models/data-models.csproj", "data-models/"]
COPY ["echo-service/echo-service.csproj", "echo-service/"]
RUN dotnet restore "./grpc-echo-service/grpc-echo-service.csproj"
COPY . .
WORKDIR "/src/grpc-echo-service"
RUN dotnet build "./grpc-echo-service.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./grpc-echo-service.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "grpc-echo-service.dll"]