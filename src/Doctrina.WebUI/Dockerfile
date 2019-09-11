FROM microsoft/aspnetcore:2.2 AS base
WORKDIR /wwwroot
EXPOSE 80

FROM microsoft/aspnetcore-build:2.1-nanoserver-1709 AS build
WORKDIR /src
COPY Doctrina.WebUI/Doctrina.WebUI.csproj ..\build\Doctrina.WebUI/
RUN dotnet restore Doctrina.WebUI/Doctrina.WebUI.csproj
COPY . .
WORKDIR /src/Doctrina.WebUI
RUN dotnet build Doctrina.WebUI.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Doctrina.WebUI.csproj -c Release -o /app

FROM base AS final
WORKDIR /wwwroot
COPY --from=publish /wwwroot .
ENTRYPOINT ["dotnet", "Doctrina.WebUI.dll"]
