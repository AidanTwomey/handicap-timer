FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY src/finisher-service.api/*.csproj ./src/finisher-service.api/
COPY src/finisher-service.lib/*.csproj ./src/finisher-service.lib/
COPY tests/finisher-service.tests/*.csproj ./tests/finisher-service.tests/
COPY tests/finisher-service.unit.tests/*.csproj ./tests/finisher-service.unit.tests/
RUN dotnet restore

# copy everything else and build app
COPY . .

WORKDIR /source/tests/finisher-service.tests
RUN dotnet test

WORKDIR /source/src/finisher-service.api
RUN dotnet publish -c release -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "finisher-service.api.dll"]