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
* [Docker](https://www.docker.com/) (optional, required for running the project in Docker).

## Getting Started

Follow these simple steps to run the project:
1. Clone Payment gateway to your local machine.
2. To run the project, simply go to `src/PaymentGateway.Api` folder and execute `dotnet run`. By defailt, Swagger page can be opened on `http://localhost:5252/swagger/index.html`, but you can specify a URL of your choice in `launchSettings.json`.
3. To run tests, navigate to the root project directory and execute `dotnet test`.

> Optional: to run the project using Docker, navigate to the root project directory and run `docker-compose up`.

Enjoy! 🙌

## Project Architecture

The project was developed following the [Clean Architecture principles](https://jasontaylor.dev/clean-architecture-getting-started/). 
According to the Clean Architecture, **Domain** and **Application** layers are at the centre of the design and form the **Core** of the system.

<p align="center">
  <img src="https://github.com/victoriademina/PaymentGateway/blob/main/images/CleanArchitectureDiagram.png" alt="Clean Architecture" width="300"/>
</p>

This architecture provides multiple benefits, including but not limited to:

1. All dependencies flow inwards and **Core** that represents business logic has no dependency on any other layer.
2. **Infrastructure** and **Api** depend on Core, but not on one another.
3. Data storage concerns are being separated from the business logic, it enables an easy migration to any database of your choice in the future.
4. As a result, every application layer can be unit-tested independently from the other layers.

For illustration purposes, the diagram below shows the data flow when a user creates a new transaction.

> Note: ideally, the endpoint for creating transactions should be asynchronous, but was not implemented due to the time constraints. See [Areas for Improvements](https://github.com/victoriademina/PaymentGateway#areas-for-improvements) section below for an overview of how this can be achieved with event-driven architecture.

<p align="center">
  <img src="https://github.com/victoriademina/PaymentGateway/blob/main/images/DataFlowDiagram.jpg" alt="Data Flow Diagram" width="1000"/>
</p>

### PaymentGateway.Domain

This layer contains all entities and enums specific to the domain, such as:

- Merchant
- Transaction
- CardDetails
- PaymentAmount
- Currency
- Status

### PaymentGateway.Application

This layer contains all the business logic of the application, organized into three main groups:
- Common classes and interfaces, e.g. IBankAdapter and IPaymentGatewayRepository.
- Commands - MediatR handlers for changing data.
- Queries - MediatR handlers for retrieving data.

**Repository pattern** has been used to decouple data access from the rest of the application. It ensures an easier switch to another database in the future, since data access layer will not affect application logic.

The explicit separation between Commands and Queries follows the **Command Query Responsibility Segregation (CQRS) pattern**. This architecture improves performance and ensures that read and write operations can be scaled up and down independently from each other.

### PaymentGateway.Api

This layer contains two controllers: 
- MerchantsController.cs
- TransactionsController.cs

The Payment Gateway API is a RESTful API that exposes 3 endpoints:
1. `POST /merchants/create`: This endpoint is used to create a merchant, who then can be used to make transactions via Payment Gateway.
2. `POST /transactions/create`: This endpoint is used to create a transaction.
3. `GET /transactions/{merchantId}/{transactionId}`: This endpoint is used to retrieve the transaction details by ID of merchant who made the transaction and payment ID.

> Note: ideally, the merchantId should be passed as a part of JWT Bearer token. Because of the time constraints, it was not implemented but added to [Areas for Improvements](https://github.com/victoriademina/PaymentGateway#areas-for-improvements).

🚀 **POST /merchants/create**

**Request:**
```console
curl -X POST http://localhost:5252/merchants/create
```
**Response:**
```json
{
  "merchantId": "d211b00e-40b9-4662-a948-eb29fc79e95c"
}
```

🚀 **POST /transactions/create**

**Request:**
```console
curl -X 'POST' \
'http://localhost:5252/transactions/create' \
-H 'Content-Type: application/json' \
-d '{
  "merchantId": "d211b00e-40b9-4662-a948-eb29fc79e95c",
  "cardDetails": {
    "cardNumber": "1111 1111 1111 1111",
    "cvv": "123",
    "expiryMonth": 12,
    "expiryYear": 2050,
    "owner": "Harry Potter"
  },
  "paymentAmount": {
    "amount": 50,
    "currency": 0
  }
}'
```
**Response:**
```json
{
  "transactionId": "0ecdbd83-0ca4-4069-ab90-3230521e42e8",
  "status": 1
}
```

🚀 **GET /transactions/{merchantId}/{transactionId}**

**Request:**
```console
curl http://localhost:5252/transactions/d211b00e-40b9-4662-a948-eb29fc79e95c/0ecdbd83-0ca4-4069-ab90-3230521e42e8
```
**Response:**
```json
{
  "transactionId": "0ecdbd83-0ca4-4069-ab90-3230521e42e8",
  "status": 1
}
```

> Note: the values of codes for transaction statuses and currencies can be found in the [Status](https://github.com/victoriademina/PaymentGateway/blob/main/src/PaymentGateway.Domain/Enums/Status.cs) and [Currency](https://github.com/victoriademina/PaymentGateway/blob/main/src/PaymentGateway.Domain/Enums/Currency.cs) enums.

### PaymentGateway.Infrastructure

This layer implements communication with all external systems:
- UltimateBankClientAdapter implements IBankAdapter and can be used to communicate with BankSimulator.
- PaymentGatewayRepository implements IPaymentGatewayRepository and uses an in-memory database for storing merchants and transactions details.

### BankSimulator

This service simulates an Aquiring Bank. I designed BankSimulator in a way that it purposely provides a different API comparing to IBankAdapter. It demonstrates flexibility and extendability of the chosen architecture. Support of different banks can be added by simply implementing IBankAdapter, and configuring it with Dependency Injection.

## Testing

**Unit tests**
- [PaymentGateway.Api.Tests](https://github.com/victoriademina/PaymentGateway/tree/main/tests/PaymentGateway.Api.Tests)
- [PaymentGateway.Application.Tests](https://github.com/victoriademina/PaymentGateway/tree/main/tests/PaymentGateway.Application.Tests)
- [PaymentGateway.Infrastructure.Tests](https://github.com/victoriademina/PaymentGateway/tree/main/tests/PaymentGateway.Infrastructure.Tests)


## Assumptions

- Merchant should be registered first to obtain MerchantId. It can be done by calling `POST merchants/create` endpoint. 
- Different banks have different SDKs, and it is important to have an ability to switch between different APIs. To introduce some flexibility, I developed IBankAdapter. If Checkout.com would like to partner with a new bank, developers just need to create a corresponding IBankAdappter implementation. 
- Ability to switch to a different database is important. To make sure it can be done without affecting application logic, I separated data access concerns by using Repository Pattern.

## Areas for Improvements

- Introduce JWT Authentication to harden the application security. Currently, I implemented authorization only: only the merchant who created a transaction can retrieve additional details about it.
- Improve data validation across application. Consider using [FluentValidation library](https://docs.fluentvalidation.net/en/latest/index.html) for building strongly-typed validation rules.
- Ensure idempotency of the transaction creation process by creating a separate endpoint `POST transactions/reserve` for reserving a transactionId, which should then be used to create the actual transaction. 
- Implement the [Luhn algorithm](https://en.wikipedia.org/wiki/Luhn_algorithm) for card number validation to prevent errors and fraudulent activities.
- Consider event-driven architecture for asyncronous communication with banks. This way `POST transactions/create` endpoint should immediately return a pending status, without waiting for a response from bank.
- Keep track of all transactions' status updates and corresponding timestamps.

## Recommended Choice of Cloud Technologies

For non-production workloads and futher development, the project can be deployed to any major cloud provider. Assuming that the organisation works on **Awazon Web Services (AWS)**, the cloud architecture can inclide multiple services, including but not limited to:

- [Amazon EC2](https://aws.amazon.com/ec2/) - it provides secure, resizable compute capacity in the cloud. It can be used to host Payment Gateway back-end.
- [DynamoDB](https://aws.amazon.com/dynamodb/) - fast, flexible NoSQL database service. It can be used to store merchants and transactions details.
- [AWS CodePipelne](https://aws.amazon.com/codepipeline/), [CircleCI](https://circleci.com/), or [Jenkins](https://www.jenkins.io/) - set up Continious Integration and Continious Deployment (CI/CD) pipelines.
- [AWS SQS](https://aws.amazon.com/sqs/) - fully managed message queuing that can be used for enabling event-driven communicaton with banks, as advised in the [Areas for Improvements](https://github.com/victoriademina/PaymentGateway#areas-for-improvements).

These services will be sufficient to deploy the project while it is in the MVP stage. When it grows further, it would be advisable to consider introducing [AWS Load Balancer](https://aws.amazon.com/elasticloadbalancing/), as well as follow best practices of cross-regional replication, multi-AZ deployment, and strategies for disaster recovery.

It is recomended to use Infrastructure as Code tools, such as [AWS CloudFormation](https://aws.amazon.com/cloudformation/) or [Terraform](https://www.terraform.io/). It will ensure automation, repeatability, and scalability of infrastructure deployment and management in a consistent and efficient manner.

## Bonus Features

1. **Run the project using Docker.** Please refer to [Getting Started](https://github.com/victoriademina/PaymentGateway#getting-started) section for instructions.
2. **Logging using Serilog.** All critically-important events were logger using Serilog to improve traceability across application.
3. **Github Workflow CI.** This project is equipped with a continuous integration (CI) workflow that utilizes GitHub Actions to automate the building and testing of the .NET codebase with every commit.
4. **Dependabot.** The usage of Dependabot in GitHub helps to automate dependency management and keeps the project up-to-date with the latest security patches and bug fixes.

## License

The Payment Gateway project is licensed under the Apache-2.0 license. You can find more information about the Apache-2.0 license in the [LICENSE](https://github.com/victoriademina/PaymentGateway/blob/main/LICENSE) file.
