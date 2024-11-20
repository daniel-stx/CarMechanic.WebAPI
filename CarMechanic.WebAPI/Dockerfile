#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CarMechanic.WebAPI/CarMechanic.WebAPI.csproj", "CarMechanic.WebAPI/"]
COPY ["CarMechanic.Application/CarMechanic.Application.csproj", "CarMechanic.Application/"]
COPY ["CarMechanic.Domain/CarMechanic.Domain.csproj", "CarMechanic.Domain/"]
COPY ["CarMechanic.Infrastructure/CarMechanic.Infrastructure.csproj", "CarMechanic.Infrastructure/"]
RUN dotnet restore "./CarMechanic.WebAPI/CarMechanic.WebAPI.csproj"
COPY . .
WORKDIR "/src/CarMechanic.WebAPI"
RUN dotnet build "./CarMechanic.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CarMechanic.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarMechanic.WebAPI.dll"]