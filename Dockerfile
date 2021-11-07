FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /source

COPY *.sln .
COPY LogProxyApi/*.csproj ./LogProxyApi/
COPY LogProxyApi.Tests/*.csproj ./LogProxyApi.Tests/
# RUN dotnet restore

# copy everything else and build app
COPY LogProxyApi/. ./LogProxyApi/
WORKDIR /source/LogProxyApi
RUN dotnet publish -c release -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "LogProxyApi.dll"]