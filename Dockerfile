# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS restore
WORKDIR /src
COPY Uuid.slnx ./
COPY src/Uuid.Core/Uuid.Core.csproj src/Uuid.Core/
COPY src/Uuid.Console/Uuid.Console.csproj src/Uuid.Console/
COPY tests/Uuid.Tests/Uuid.Tests.csproj tests/Uuid.Tests/
RUN dotnet restore Uuid.slnx

FROM restore AS build
COPY src/ src/
COPY tests/ tests/
RUN dotnet build Uuid.slnx -c Release --no-restore

# Optional: docker build --target test .
FROM build AS test
RUN dotnet test Uuid.slnx -c Release --no-build --no-restore

FROM build AS publish
RUN dotnet publish src/Uuid.Console/Uuid.Console.csproj \
    -c Release \
    --no-build \
    --no-restore \
    -o /app \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/runtime:10.0 AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Uuid.Console.dll"]
