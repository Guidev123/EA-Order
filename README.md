<p align="center">
  <a href="https://dotnet.microsoft.com/" target="blank"><img src="https://upload.wikimedia.org/wikipedia/commons/e/ee/.NET_Core_Logo.svg" width="120" alt=".NET Logo" /></a>
</p>

# Orders API

This is the core service of my application, responsible for managing orders. The service was developed using Event-Driven Architecture (EDA) and follows the principles of Clean Architecture, CQRS, SOLID, and Domain-Driven Design (DDD). It interacts with the Cart API by publishing events using the Publish/Subscribe pattern, leveraging the [`SharedLib.MessageBus`](https://github.com/Guidev123/EA-SharedLib) library.

## Architecture

- **Event-Driven Architecture (EDA)**: This API uses event-driven communication to decouple services and provide scalability.
- **Clean Architecture**: The application is structured in layers, ensuring separation of concerns and making it easier to maintain and test.
- **CQRS (Command Query Responsibility Segregation)**: The service separates command (write) and query (read) operations to optimize performance and scalability.
- **SOLID Principles**: The design adheres to SOLID principles, promoting code that is easy to understand, extend, and maintain.
- **Domain-Driven Design (DDD)**: The API follows DDD principles, focusing on the business domain and ensuring that the application reflects the problem space.
