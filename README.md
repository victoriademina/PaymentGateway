# PaymentGateway
Coding challenge for Checkout.com

[![.NET](https://github.com/victoriademina/PaymentGateway/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/victoriademina/PaymentGateway/actions/workflows/dotnet.yml)

## Getting Started

To get started with the Payment Gateway API, you need to first clone or download the project files to your local machine. 

Clone by SSH: `git clone git@github.com:victoriademina/PaymentGateway.git`

Once you have the project files, you can open the solution in IDE of your choice and run the project by running `dotnet run` from PaymentGateway.Api service.

## Dependencies

This project relies on the following dependencies:

* .NET 7
* NUnit
* Moq
* MediatR

## Project Structure

The Payment Gateway project is structured as follows:

* 
* 
* 
* 
* 

## Usage

The Payment Gateway API is a RESTful API that exposes the following endpoints:

* `POST /api/merchants/create`: This endpoint is used to create a merchant, who then can be used to make transactions via Payment Gateway.
* `POST /api/transactions/create`: This endpoint is used to create a transaction. 
* `GET /api/transactions/get/{merchantId}/{transactionId}`: This endpoint is used to retrieve the transaction details by ID of merchant who made the transaction and payment ID.

To use the Payment Gateway API, you can send HTTP requests to these endpoints using a tool such as Postman.

## Acknowledgements

## Contributing

If you want to contribute to the Payment Gateway project, you can create a pull request with your changes. Please make sure that your changes follow the project's coding standards and are thoroughly tested.

## License

The Payment Gateway project is licensed under the Apache-2.0 license. You can find more information about the Apache-2.0 license in the [LICENSE](https://github.com/victoriademina/PaymentGateway/blob/main/LICENSE) file.
