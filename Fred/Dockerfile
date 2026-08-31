FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY src/Application/Fred.Application.csproj Application/Fred.Application.csproj
COPY src/Infrastructure/Fred.Infrastructure.csproj Infrastructure/Fred.Infrastructure.csproj
COPY src/WebUI/Fred.WebUI.csproj WebUI/Fred.WebUI.csproj
RUN dotnet restore WebUI/Fred.WebUI.csproj

COPY ./src .

RUN dotnet build WebUI/Fred.WebUI.csproj --configuration Release
RUN dotnet publish WebUI/Fred.WebUI.csproj --configuration Release --output /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0-azurelinux3.0
WORKDIR /app

USER $APP_UID

COPY --chown=$APP_UID --from=build /app/publish .

EXPOSE 5080
ENV ASPNETCORE_HTTP_PORTS=5080

HEALTHCHECK --interval=5s --timeout=10s --retries=3 CMD curl --fail http://localhost:5080/healthz || exit 1

ENTRYPOINT ["dotnet", "Fred.WebUI.dll"]