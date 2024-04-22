# Project Overview

## API (.NET Core 7)

The API is developed in .NET Core 7 and does not use databases; instead, it uses a list of objects to store data configured as a singleton in the `Program.cs` file. All services are located in the `PremiumCalculatorController` controller. The API has an `appsettings.json` file where the URL of the view server (`index.html`) is configured. CORS is configured to prevent issues. The API runs in visual studio 2022.

## View Server (Node.js)

The view server is implemented in Node.js and is started with the command `node Server.js` in the `PremiumCalculator` project folder. The project structure consists of a folder named `PremiumCalculator`, which contains both the `Server.js` and `Index.html` files. Please note that the project uses Windows folder paths, which may cause issues if not running on a Windows environment.

The view includes a button to load initial data. The project provides a service for inserting records and a POST service for initializing test data if desired by the user.

## Port Configuration

- API Port: 5282
- View Server Port: 3000

Please ensure that these ports are available and not in use by other applications.

## Additional Notes

- The API does not use a database; instead, it stores data in memory using a list of objects.
- All API services are located in the `PremiumCalculatorController`.
- CORS is configured in the API to allow requests from the view server.
- The view server is implemented in Node.js and serves the `Index.html` file.
- The `Server.js` file is located in the `PremiumCalculator` project folder.
- The view includes a button to load initial data and provides services for inserting records and initializing test data.
- Ensure that the specified ports (5282 for the API and 3000 for the view server) are available and not in use by other applications.
