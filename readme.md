# Project Architecture and Setup

## Architecture Overview

This project follows a layered architecture pattern, which helps in separating concerns and organizing the codebase into distinct layers. The main layers in this project are:

1. **Core Layer**: Contains the business logic and domain models.
2. **Infrastructure Layer**: Handles data access and persistence.
3. **Api Layer**: Contains the API controllers, contracts and configurations.

### Core Layer

The Core Layer in this project is structured per feature, meaning that each feature has its own set of models, services, and use cases. This helps in organizing the codebase in a way that is easy to maintain.

The Common folder contains shared models and utilities that are used across different features.

The key components in this layer are:
* **Features**: Contains folders for each feature in the application. In this case single one Products.
* **Common**: Contains shared models and utilities.
* **Exceptions**: Contains custom exceptions that are thrown in the application.

Using MediatR for CQRS pattern, you can find the command and query classes in the Features folder under the 
**UseCases** folder.

### Infrastructure Layer

The Infrastructure Layer is responsible for data access and persistence. It contains repositories that interact with the database and other external services.

Some of the key components in this layer are:

* **Persistence**: Contains the database context, repositories, ef configurations and db seeder.
* **Services**: Contains services that interact with external services like the **discord service**.
* **Cache**: Contains the cache service that interacts with the in memory cache.

### Api Layer

The Api Layer is responsible for handling HTTP requests and responses. It contains the API controllers, contracts, and configurations.

## How to Run the Project

To run the project, you need to have the following installed on your machine:

1. [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
2. [Docker](https://www.docker.com/products/docker-desktop)

To run the project, follow these steps:
1. Clone the repository.
2. Navigate to the project directory.
3. Run the following commands to start the project:

If you do not have SQL Server installed on your machine, you can use Docker to run a SQL Server container. To do this, run the following command:
```bash
docker-compose up -d
```
Once the container is up and running you can start tge application.

**Important**:
If you have SQL Server installed on your machine, you can skip this step. But you need to update the connection string in the `appsettings.json` file.
with your connection string.

Next, run the following command to start the project:
```bash
dotnet run --project src/Api/Api.csproj
```

The project will start and create and seed the database with some initial data.

## Ulid as Primary Key
I used ULID for the keys of the products. ULID is a 128-bit compatible with UUID/GUID. It is designed to be used as a primary key in databases.
It is sortable and has a timestamp component that can be used to sort the keys by creation date.

You can find more information about ULID [here](https://dev.to/nejos97/what-is-ulid-and-why-should-you-start-using-it-14j9)

## Logging
This project use Serilog for logging. The logs are written to the console and to a file. You can find the logs in the `logs` folder in the root directory of the project.
I didn't have time to log the execution time of the requests, but this can be easily added by adding a middleware that logs the request time.

### Additional Information
The project is using in memory cache to cache products discounts. The cache is set to expire after 5 minutes. 
This can be changed in the `CacheService` class. In a production app this would have been stored in a distributed cache like Redis and the expiration time would have been configurable using environment variables or configuration.

I didn't implement the cache for product status since in my opinion it's not necessary to cache this information since it is stored in the database with the name already. I considered that 
having a cache for discount is a more real use case.

Unit tests were not written for this project due to time constraints. But I would have written unit tests the use cases and services in the Core and Infrastructure layers.


## Libraries Used
* [MediatR]
* [Entity Framework Core]
* [Serilog]
