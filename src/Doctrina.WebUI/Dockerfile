FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build

WORKDIR /src
COPY Doctrina.WebUI.csproj .
RUN dotnet restore Doctrina.WebUI.csproj

# Copy everything to build/src
COPY ./ ./

FROM build AS publish
RUN dotnet publish Doctrina.WebUI.csproj -c Release -o /wwwroot --no-restore

# TODO: run react build before copying build output
WORKDIR /src/ClientApp
COPY ClientApp/build .

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
FROM runtime AS final
WORKDIR /wwwroot
COPY --from=publish /wwwroot .
EXPOSE 80
ENTRYPOINT ["dotnet", "Doctrina.WebUI.dll"]