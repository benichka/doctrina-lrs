## Getting Started
Use these instructions to get the project up and running.

### Prerequisites
You will need the following tools:

* [Visual Studio Code or 2017](https://www.visualstudio.com/downloads/)
* [.NET Core SDK 2.2](https://www.microsoft.com/net/download/dotnet-core/2.2)

### Setup
Follow these steps to get your development environment set up:

  1. Clone the repository
  2. Initialize submodules
      ```
      git submodule
      ```

Next pick your development tool

#### Command line/VS Code
Follow these steps to get your development environment set up:

  1. At the src directory, restore required packages by running:
     ```
     dotnet restore
     ```
  2. Next, build the solution by running:
     ```
     dotnet build
     ```
  3. Next, within the `.\src\Doctrina.WebUI\ClientApp` directory, launch the front end by running:
     ```
     npm start
     ```
  4. Once the front end has started, within the `Doctrina.WebUI` directory, launch the back end by running:
     ```
	 dotnet run
	 ```
  5. Launch [http://localhost:52468/](http://localhost:52468/) in your browser to view the Web UI

  6. Launch [http://localhost:52468/api](http://localhost:52468/xapi) in your browser to view the API

#### Visual Studio
Follow these steps to get your development environment set up:
1. Open the `Doctrina.sln` inside the `src` folder.
2. Hit F5 to start from docker-compose.csproj.

#### Docker Compose

1. At the src directory, build the image by running:
   ```
   docker-compose build
   ```
2. After image has finished building, start a new container by running:
   ```
   docker run doctrina
   ```


## Technologies
* .NET Core 2.2
* ASP.NET Core 2.2
* Entity Framework Core 2.2
* NodeJS

## License
This project is licensed under the GPL-3.0 License - see the [LICENSE](https://github.com/bitflipping-solutions/doctrina-lrs/blob/develop/LICENSE) file for details.