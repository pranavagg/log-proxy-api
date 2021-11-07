## Features

- Swagger Supported
- Basic Authentication Supported
- Production Ready Project
- Test Coverage and Report Generation supported 
- Environment based config

## Development Run

If Dotnet is installed

```
cd LogProxyApi && dotnet run
```

If Docker is installed

```
docker build -t log-proxy-api .

docker run --rm -it -p 5000:80 log-proxy-api
```

## Unit Testing and Code Coverage Report Generation

```
cd LogProxyApi.Tests && dotnet test --collect:"XPlat Code Coverage"

dotnet tool install -g dotnet-reportgenerator-globaltool

reportgenerator -reports:"./TestResults/{guid}/coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html
```

Check the report in `coveragereport/index.html`

## Development Process

Started new Project with

```
dotnet new sln -o log-proxy-api
cd log-proxy-api/
dotnet new webapi -o LogProxyApi
dotnet sln add ./LogProxyApi/LogProxyApi.csproj
dotnet new xunit -o LogProxyApi.Tests
dotnet sln add ./LogProxyApi.Tests/LogProxyApi.Tests.csproj
dotnet add ./LogProxyApi.Tests/LogProxyApi.Tests.csproj reference ./LogProxyApi/LogProxyApi.csproj
```

## Fixme/Improvements

- Store Hashed Password
- Store User Data in Config File or DB
- Structure of project Documentation
- Retry Logic for External Api
- Circuit Breaker for external Api
- Unit Tests
    - Whether third party API mock getting called
    - Whether Get request converting in correct output class
- Pagination Support
    - Using Offset to convert into Page number equivalent API for all messages
    - No Change in Contract of Get API. Extra Param to be sent
    - If Contract can be changes then response should not be a list. It should be a object response with offset as provided by upstream.
