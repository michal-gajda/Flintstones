FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY src/WebApi/Wilma.WebApi.csproj WebApi/Wilma.WebApi.csproj
RUN dotnet restore WebApi/Wilma.WebApi.csproj

COPY ./src .

RUN dotnet build WebApi/Wilma.WebApi.csproj --configuration Release
RUN dotnet publish WebApi/Wilma.WebApi.csproj --configuration Release --output /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0-azurelinux3.0
WORKDIR /app

USER $APP_UID

COPY --chown=$APP_UID --from=build /app/publish .

EXPOSE 5080
ENV ASPNETCORE_HTTP_PORTS=5080

HEALTHCHECK --interval=5s --timeout=10s --retries=3 CMD curl --fail http://localhost:5080/healthz || exit 1

ENTRYPOINT ["dotnet", "Wilma.WebApi.dll"]