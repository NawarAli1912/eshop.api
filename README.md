# eshop.api

## Introduction

eshop is a web application built using .NET 7 that implements Domain-Driven Design (DDD) principles and patterns such as rich domain model, aggregate domain events, and outbox design pattern. It also utilizes Elastic Search for searching products and domain events to synchronize data with SQL Server DB. The application uses libraries like MediatR, NEST, and EF Core to help with these tasks.
note that *the project is still in progress*.

## Features

- Rich domain model: Domain objects are designed to encapsulate both behavior and data, following DDD principles.
- Aggregate domain events: Domain events are used to capture changes in the state of aggregates and broadcast them to interested parties.
- Outbox design pattern: The outbox pattern is used to ensure data consistency between Elastic Search and SQL Server DB.
- Elastic Search integration: Elastic Search is used for efficient product search and synchronization with SQL Server DB.
- API: eshop is implemented as an API for easy integration with other systems.
- MediatR: MediatR is used to implement the Command-Query Responsibility Segregation (CQRS) pattern and handle commands and queries.
- NEST: NEST is used to provide a .NET client for Elastic Search and simplify working with it in the application.
- EF Core: EF Core is used as the ORM to work with SQL Server DB and simplify database operations.

## Getting Started

To run eshop, you'll need .NET 7, docker, and sql-server installed on your system. Follow these steps to get started:

1. Clone the repository.
2. Run the docker-compose file to set up the elastic search.
3. Modify the `appsettings.json` file to configure the database.
4. Build and run the application.


## Usage

eshop provides the following endpoints:

**TODO**

## Contributing

Contributions are welcome! If you have any suggestions or bug reports, please open an issue or submit a pull request.

## License

This project is licensed under the MIT License.
