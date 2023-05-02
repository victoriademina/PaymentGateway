# PaymentGateway
Coding challenge for Checkout.com

[![.NET](https://github.com/victoriademina/PaymentGateway/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/victoriademina/PaymentGateway/actions/workflows/dotnet.yml)

## Prerequisites

This project relies on the following dependencies:

* [.NET 7](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
* [MediatR](https://github.com/jbogard/MediatR)
* [Entity Framework Core 8](https://learn.microsoft.com/en-us/ef/core/providers/in-memory/?tabs=dotnet-core-cli)
* [NUnit](https://nunit.org/), [Moq](https://github.com/moq)
* [Serilog](https://github.com/serilog/serilog/wiki/Getting-Started), [Serilog.Sinks.Console](https://github.com/serilog/serilog/wiki/Getting-Started)

## Getting Started

Follow these simple steps to run the project:
1. Clone Payment gateway to your local machine.
2. To run the project, simply go to `src/PaymentGateway.Api` folder and execute `dotnet run`. By defailt, Swagger page can be opened on `http://localhost:5252/swagger/index.html`, but you can specify a URL of your choice in `launchSettings.json`.
3. To run tests, navigate to the root project directory and execute `dotnet test`. 

Enjoy! 🙌

## Project Architecture

The project was developed following the [Clean Architecture principles](https://jasontaylor.dev/clean-architecture-getting-started/). 
According to the Clean Architecture, **Domain** and **Application** layers are at the centre of the design and form the **Core** of the system.

<p align="center">
  <img src="https://github.com/victoriademina/PaymentGateway/blob/main/images/CleanArchitectureDiagram.png" alt="Image description" width="300"/>
</p>

This architecture provides multiple benefits, including but not limited to:

1. All dependencies flow inwards and **Core** that represents business logic has no dependency on any other layer.
2. **Infrastructure** and **Api** depend on Core, but not on one another.
3. Data storage concerns are being separated from the business logic, it enables an easy migration to any database of your choice in the future.
4. As a result, every application layer can be unit-tested independently from the other layers.

**Repository pattern** has been used to decouple data access from the rest of the application. It ensures an easier switch to another database in the future, since data access layer will not affect application logic. 

**Command Query Responsibility Segregation (CQRS) pattern** has been used to separate an application into two distinct parts: a command side for changing data, and a query side for reading data. This architecture improves performance and ensures that read and write operations can be scaled up and down independently from each other.

### PaymentGateway.Domain

This layer contains all entities and enums specific to the domain, such as:

- Merchant
- Transaction
- CardDetails
- PaymentAmount
- Currency
- Status

### BankSimulator
This service simulates an Aquiring Bank. I designed BankSimulator in a way that it purposely provides a different API comparing to IBankAdapter. It demonstrates flexibility and extendability of the chosen architecture. Support of different banks can be added by simply implementing IBankAdapter, and configuring it with Dependency Injection. 

### PaymentGateway.Api

This layer contains two controllers: 
- MerchantsController.cs
- TransactionsController.cs

The Payment Gateway API is a RESTful API that exposes 3 endpoints:

`POST /merchants/create`: This endpoint is used to create a merchant, who then can be used to make transactions via Payment Gateway.

**Example:**
```
Example
```

`POST /transactions/create`: This endpoint is used to create a transaction. 

**Example:**
```
Example
```

`GET /transactions/{merchantId}/{transactionId}`: This endpoint is used to retrieve the transaction details by ID of merchant who made the transaction and payment ID.

**Example:**
```
Example
```


### PaymentGateway.Application

### PaymentGateway.Infrastructure


## Assumptions
- Merchant should be registered. 
- Bank SDKs are different, and need flexibility. Need to support switch between different bank apis. 
- We need to switch between databases... repository pattern. 

## Areas for improvements
- Authentication JWT
- Further data validation - use fluent validation
- Create a separate endpoint for 
- Make createtransaction идемпотентным. 
- Luhn check
- Event driven architecture for communicating with banks.
  - create transaction endpoint should return a pending result immediately, without waiting for a redsponce from bank. it should be async.

## Recommended Choice of Cloud Technologies

**Amazon Web Services**
- EC2 - run back-end
- DynamoDB - serverless NoSQL database. 
- AWS SQS - a message queuing service queue for enabling event-driven communicaton between BankAdapter and BankSimulator.
- AWS CodePipelne/CircleCI/Jenkins - set up CI/CD

## Bonus Features

### Data Validation
### Logging using Serilog
### Github Workflow CI
This project is equipped with a continuous integration (CI) workflow that utilizes GitHub Actions to automate the building and testing of the .NET codebase with every commit. This workflow has been carefully crafted to ensure that the project's functionality and stability are maintained at all times.
### Dependabot

## License

The Payment Gateway project is licensed under the Apache-2.0 license. You can find more information about the Apache-2.0 license in the [LICENSE](https://github.com/victoriademina/PaymentGateway/blob/main/LICENSE) file.
