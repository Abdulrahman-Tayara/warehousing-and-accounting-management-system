# Project Title

The back-end of Warehousing and Accounting Management System, developed using .NET Core for my senior Software
Engineering graduation project.

## Description

Our Warehouse and Accounting Management System offers multiple baseline ERP features, with 80+ apis for inventory and
billing information management.

## Features implemented

* ### Warehousing
    * Invoicing with multiple types including: Sales, Purchases, Returns, Imports, Exports.
    * Making payments for invoices issued with debts.
    * Discounting on invoices and integration with payments.
    * Product conversions for splitting or aggregating multiple types of products.
    * Product minimum level notifications.
    * Inventory management, including extended set of filters, multiple groupings, and more.

* ### Accounting
    * Journal Entries for adding billing relations over accounts.
    * Account Statements for specific billing information over accounts including debts and credits.

* ### Authentication and Authorization
    * Offering different roles for each user, and policies on specified system resources.

<br>

#### See [commit history](https://github.com/Ahmad-Hamwi/warehousing-and-accounting-management-system/commits/development) for details about features implemented.

## Design and code structure

### Clean Architecture

<img align="right" width="398" height="291" src="https://blog.cleancoder.com/uncle-bob/images/2012-08-13-the-clean-architecture/CleanArchitecture.jpg" />

The system follows the principles of clean architecture, including:

- The dependency rule:

This can be witnessed with our packaging hierarchy; Domain -> Application -> Infrastructure and Presentation.

- Entities:

Our entities are objects with methods specifying the business rules, can be found under the Domain/Entities package.

- Use Cases:

Our application layer contains application specific business rules. It encapsulates and implements all of the use cases
of the system. These use cases orchestrate the flow of data to and from the entities, and direct those entities to use
their enterprise wide business rules to achieve the goals of the use case.

- Interface Adapters:

Repositories and Services under our Application layer follow this rule. The main reason for it is to converted from the
form most convenient for entities and use cases, into the form most convenient for whatever persistence framework is
being used. In our case; Entity Framework.

### CQRS Design Pattern

The system follows the CQRS pattern, all can be witnessed under our Application layer. The pattern states that:

- Reads and writes are separated into different models, using commands to update data, and queries to read data.


- Queries never modify the database. A query returns a DTO that does not encapsulate any domain knowledge.


- Commands should be task-based, rather than data centric ("Close invoice", not "set InvoiceStatus to Closed").


- Commands may be placed on a queue for asynchronous processing, rather than being processed synchronously. A mediator
  library such as MediatR helped us with this.

### Repository Design Pattern

<img align="right" width="398" height="291" src="https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/media/infrastructure-persistence-layer-design/repository-aggregate-database-table-relationships.png" />

Our system uses classes called "Repositories" that encapsulate the logic required to access data sources.

They centralize common data access functionality, providing better maintainability and decoupling the infrastructure or
technology used to access databases from the domain model layer.

This lets you focus on the data persistence logic rather than on data access plumbing.

<br>
<br>
<br>

## Technologies

* [ASP.NET Core 6](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-6.0)
* [Entity Framework Core 6](https://docs.microsoft.com/en-us/ef/core/)
* [MediatR](https://github.com/jbogard/MediatR)
* [AutoMapper](https://automapper.org/)
* [FluentValidation](https://fluentvalidation.net/)
* [Swagger](https://swagger.io/)

## License

This project is licensed under the Apache 2.0 License - see the LICENSE file for details.

## Authors

Contributors names and contact info

[@Ahmad-Hamwi](https://github.com/Ahmad-Hamwi)

[@Abdulrahman-Tayara](https://github.com/Abdulrahman-Tayara)