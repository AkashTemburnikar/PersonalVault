# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/PersonalVault.API/PersonalVault.API.csproj", "src/PersonalVault.API/"]
COPY ["src/PersonalVault.Application/PersonalVault.Application.csproj", "src/PersonalVault.Application/"]
COPY ["src/PersonalVault.Infrastructure/PersonalVault.Infrastructure.csproj", "src/PersonalVault.Infrastructure/"]
COPY ["src/PersonalVault.Domain/PersonalVault.Domain.csproj", "src/PersonalVault.Domain/"]

RUN dotnet restore "src/PersonalVault.API/PersonalVault.API.csproj"

COPY . .
WORKDIR "/src/src/PersonalVault.API"
RUN dotnet build "PersonalVault.API.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "PersonalVault.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "PersonalVault.API.dll"]