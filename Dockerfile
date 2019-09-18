FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build

ARG BUILDCONFIG=Release
ARG VERSION=1.0.0

WORKDIR /src
COPY Doctrina.WebUI/Doctrina.WebUI.csproj ..\build\Doctrina.WebUI/
RUN dotnet restore Doctrina.WebUI/Doctrina.WebUI.csproj
COPY . .
WORKDIR /src/Doctrina.WebUI
RUN dotnet build Doctrina.WebUI.csproj -c Release -o /wwwroot

FROM build AS publish
RUN dotnet publish Doctrina.WebUI.csproj -c Release -o /wwwroot

FROM runtime AS final
WORKDIR /wwwroot
COPY --from=publish /wwwroot .

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /wwwroot
EXPOSE 80
ENTRYPOINT ["dotnet", "Doctrina.WebUI.dll"]