FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln .
COPY Broker.WebApi/*.csproj ./Broker.WebApi/
COPY Broker.Domain/*.csproj ./Broker.Domain/
RUN dotnet restore

# Copy everything else and build
COPY Broker.WebApi/ ./Broker.WebApi/
COPY Broker.Domain/ ./Broker.Domain/
RUN dotnet publish -c Release -o out

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 3000
ENTRYPOINT ["dotnet", "Broker.WebApi.dll"]