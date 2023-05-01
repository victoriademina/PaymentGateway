using MediatR;
using Moq;
using PaymentGateway.Api.Controllers;
using PaymentGateway.Api.Dtos;
using PaymentGateway.Application.Commands.CreateTransaction;
using PaymentGateway.Domain.Entities;
using PaymentGateway.Domain.Enums;

namespace PaymnentGateway.Api.Tests.Controllers;

public class TransactionsControllerTests
{
    private TransactionsController _transactionsController;
    private Mock<IMediator> _mediator;

    [SetUp]
    public void Setup()
    {
        _mediator = new Mock<IMediator>();
        _transactionsController = new TransactionsController(_mediator.Object);
    }

    [Test]
    public async Task CreateTransactionTest()
    {
        //Arrange
        var merchantId = Guid.NewGuid();
        var transactionId = Guid.NewGuid();
        var cardDetails = new CardDetails
        {
            CardNumber = "1111 1111 1111 1111",
            Cvv = "123",
            ExpiryMonth = 3,
            ExpiryYear = 2032,
            Owner = "Harry Potter"
        };
        var paymentAmount = new PaymentAmount
        {
            Amount = 44,
            Currency = "GBP"
        };
        _mediator.Setup(_ => _.Send(It.IsAny<CreateTransactionRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(new CreateTransactionResponse
        {
            Status = Status.Pending,
            TransactionId = transactionId
        });

        // Act
        var result = await _transactionsController.CreateTransaction(new CreateTransactionDto(
            merchantId, cardDetails, paymentAmount));

        // Assert
        Assert.That(result.Value.TransactionId, Is.EqualTo(transactionId));
        Assert.That(result.Value.Status, Is.EqualTo(Status.Pending));
    }
}