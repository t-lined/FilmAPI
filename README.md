# FilmAPI

## Introduction
Welcome to the Film API project! This assignment involves creating a Web API to manage information about movies, characters, and franchises using ASP.NET Core and Entity Framework Code First. <br>
This README will guide you through setting up the development environment and running the Film API.

Before you begin, make sure you have the following tools installed on your system:

- Visual Studio 2022 with .NET 6.
- SQL Server Management Studio.

## Getting Started
Follow these steps to set up and run the Film API:

### Database Setup
Open SQL Server Management Studio and connect to your SQL Server instance.
Create a new database or use an existing one to store the Film API data.

### Running the API
Clone the repository: <br>
- ```https://github.com/t-lined/FilmAPI.git```

Open the project in Visual Studio 2022. <br>
Locate the appsettings.json file in the project and update the ConnectionStrings section to point to your SQL Server database. <br>
Build the solution. <br>
Open the Package Manager Console and run the following command to apply database migrations: <br>
- ```update-database``` <br>
This will create the necessary tables in your database.

Start the API by pressing F5 or using the Debug menu.

Now the Film API is running.

## API Documentation
The API is documented using Swagger/OpenAPI. To access the documentation:

With the API running, open a web browser.
Navigate to ```https://localhost:<port>/swagger/index.html```, where <port> is the port number specified in your launch settings.
You can also explore the available endpoints and test the API directly from the Swagger interface.

## Project Structure
The project is organized as follows:

- Controllers: Contains the API controllers.
- Data: Contains Entity Framework DbContext and database models.
- Mappers: Contains classes for mapping between domain models and DTOs.
- Migrations: Contains database migration files.
- Properties: Contains project properties and settings.
- Services: Contains services that encapsulate business logic.
- appsettings.json: Configuration settings, including the database connection string.

## Deployment (Optional)
You can deploy the Film API to a production environment. Consider using Docker and CI/CD pipelines for automated deployment. Refer to the provided GitLab CI template for Docker deployment.

## Contributors
- [Tommy Jåvold](https://github.com/t-lined)<br>
- [Noah Høgstøl](https://github.com/Nuuah)
