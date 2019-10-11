FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build

# Install node and npm on the build image
ENV NODE_VERSION 8.12.0
ENV NODE_DOWNLOAD_SHA 3df19b748ee2b6dfe3a03448ebc6186a3a86aeab557018d77a0f7f3314594ef6
ENV NODE_DOWNLOAD_URL https://nodejs.org/dist/v$NODE_VERSION/node-v$NODE_VERSION-linux-x64.tar.gz

RUN wget "$NODE_DOWNLOAD_URL" -O nodejs.tar.gz \
	&& echo "$NODE_DOWNLOAD_SHA  nodejs.tar.gz" | sha256sum -c - \
	&& tar -xzf "nodejs.tar.gz" -C /usr/local --strip-components=1 \
	&& rm nodejs.tar.gz \
	&& ln -s /usr/local/bin/node /usr/local/bin/nodejs

RUN curl -sL https://deb.nodesource.com/setup_8.x | bash -
RUN apt-get install -y nodejs

# Install npm packages first, this is slow so we want to minimise the number of un-cached runs
WORKDIR /src/Doctrina.WebUI/ClientApp/
COPY Doctrina.WebUI/ClientApp/package.json .
COPY Doctrina.WebUI/ClientApp/package-lock.json .

RUN npm install

# Restore dotnet before build to allow for caching
WORKDIR /
# Copy *.csproj
# Copy experience api for now, until stable version
COPY experience-api/src/Data/Data.csproj /src/experience-api/src/Data/
COPY experience-api/src/Client/Client.csproj /src/experience-api/src/Client/
COPY Doctrina.Application/Doctrina.Application.csproj /src/Doctrina.Application/
COPY Doctrina.Common/Doctrina.Common.csproj /src/Doctrina.Common/
COPY Doctrina.Domain/Doctrina.Domain.csproj /src/Doctrina.Domain/
COPY Doctrina.Infrastructure/Doctrina.Infrastructure.csproj /src/Doctrina.Infrastructure/
COPY Doctrina.Persistence/Doctrina.Persistence.csproj /src/Doctrina.Persistence/
COPY Doctrina.WebUI/Doctrina.WebUI.csproj /src/Doctrina.WebUI/

RUN dotnet restore /src/Doctrina.WebUI/Doctrina.WebUI.csproj

# Copy source files and build
COPY . /src

RUN dotnet build /src/Doctrina.WebUI/Doctrina.WebUI.csproj --no-restore -c Release
RUN dotnet publish /src/Doctrina.WebUI/Doctrina.WebUI.csproj --no-restore -c Release -o /app

# Copy compiled app to runtime container
FROM runtime AS final
COPY --from=build /app .
CMD ["dotnet", "Doctrina.WebUI.dll"]