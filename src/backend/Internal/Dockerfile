FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln .
COPY Internal.WebApi/*.csproj ./Internal.WebApi/
COPY Internal.Domain/*.csproj ./Internal.Domain/
RUN dotnet restore

# Copy everything else and build
COPY Internal.WebApi/ ./Internal.WebApi/
COPY Internal.Domain/ ./Internal.Domain/
RUN dotnet publish -c Release -o out

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 3000
ENTRYPOINT ["dotnet", "Internal.WebApi.dll"]