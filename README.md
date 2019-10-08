## Getting Started
Use these instructions to get the project up and running.

### Prerequisites
You will need the following tools:

* [Visual Studio Code or 2017](https://www.visualstudio.com/downloads/)
* [.NET Core SDK 2.2](https://www.microsoft.com/net/download/dotnet-core/2.2)

### Setup
Follow these steps to get your development environment set up:

   1. Clone the repository
   2. Initialize submodule in the local configuration
      ```
      git submodule init
      ```
   3. Fetch all data for each submodule
      ```
      git submodule update
      ```

Next pick your development tool

#### Command line/VS Code (RECOMMENDED)
Follow these steps to get your development environment set up:

  1. Within the `.\src\` directory, restore required packages by running:
     ```
     dotnet restore .\Doctrina.sln
     ```
  2. Next, build the solution by running:
     ```
     dotnet build .\Doctrina.sln --no-restore
     ```
  4. Next, within the `.\src\Doctrina.WebUI\` start the server by running:
     ```
	  dotnet run
	  ```
  5. Launch [http://localhost:5000/](http://localhost:5000/) in your browser to view the React SPA

  6. Launch [https://localhost:5001/xapi/about](https://localhost:5001/xapi/about) in your browser to view the xAPI about resource

#### Visual Studio (WIP)
Follow these steps to get your development environment set up:
1. Open the `Doctrina.sln` inside the `src` folder.
2. Hit F5 to start from docker-compose.csproj.

#### Docker Compose (WIP)

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