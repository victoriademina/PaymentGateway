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
1. Clone Payment gateway to your local machine. You can download the project files, or clone by SSH: `git clone git@github.com:victoriademina/PaymentGateway.git`
2. To run the project, simply execute `dotnet run` from `src/PaymentGateway.Api` folder. By defailt, Swagger page will be opened on `http://localhost:5252/swagger/index.html`, but you can specify a localhost of your choice in `launchSettings.json`.
3. To run tests, execure `dotnet test` from the root directory. 

Enjoy! 🙌

## Project Structure

The Payment Gateway project is structured as follows:

### PaymentGateway.Domain
This contains all entities and enums specific to the domain layer.

### BankSimulator
This service simulates an Aquiring Bank.

### PaymentGateway.Api

### PaymentGateway.Application

### PaymentGateway.Infrastructure


## Usage

The Payment Gateway API is a RESTful API that exposes the following endpoints:

* `POST /merchants/create`: This endpoint is used to create a merchant, who then can be used to make transactions via Payment Gateway.
* `POST /transactions/create`: This endpoint is used to create a transaction. 
* `GET /transactions/get/{merchantId}/{transactionId}`: This endpoint is used to retrieve the transaction details by ID of merchant who made the transaction and payment ID.

To use the Payment Gateway API, you can send HTTP requests to these endpoints using a tool such as Postman.

## Assumptions

## Areas for improvements

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
