using Moq;
using PaymentGateway.Application.Commands.CreateTransaction;
using PaymentGateway.Application.Common.Bank;
using PaymentGateway.Application.Common.Repository;
using PaymentGateway.Domain.Entities;
using PaymentGateway.Domain.Enums;

namespace PaymentGateway.Application.Tests.Commands.CreateTransaction;

public class CreateTransactionHandlerTests
{
    private static readonly Merchant _merchant = new Merchant { Id = Guid.NewGuid() };

    private static readonly Transaction _transaction = new Transaction
    {
        CardNumber = "1111 1111 1111 1111",
        Id = Guid.NewGuid(),
        Merchant = _merchant,
        MerchantId = _merchant.Id,
        Status = Status.Pending
    };
    
    private static readonly CardDetails _cardDetails = new CardDetails
    {
        CardNumber = _transaction.CardNumber,
        Cvv = "123",
        ExpiryMonth = 4,
        ExpiryYear = 2028,
        Owner = "Severus Snape"
    };
    private static readonly PaymentAmount _paymentAmount = new PaymentAmount
    {
        Amount = 345,
        Currency = "GBP"
    };

    private Mock<IPaymentGatewayRepository> _repository;
    private Mock<IBankAdapter> _bankAdapter;
    private CreateTransactionHandler _handler;

    [SetUp]
    public void Setup()
    {
        _repository = new Mock<IPaymentGatewayRepository>();
        _bankAdapter = new Mock<IBankAdapter>();
        _handler = new CreateTransactionHandler(_repository.Object, _bankAdapter.Object);
    }

    [Test]
    public async Task HandleSuccessfulPaymentTest()
    {
        // Arrange
        _repository
            .Setup(_ => _.GetMerchantById(_merchant.Id))
            .ReturnsAsync(_merchant);
        _repository
            .Setup(_ => _.CreateTransaction(_merchant, _transaction.CardNumber))
            .ReturnsAsync(_transaction);
        _bankAdapter
            .Setup(_ => _.ProcessPayment(new BankRequest(_transaction.Id, _cardDetails, _paymentAmount)))
            .ReturnsAsync(new BankResponse(_transaction.Id, PaymentStatus.Success, null));

        // Act
        var result = await _handler.Handle(new CreateTransactionRequest
        {
            CardDetails = _cardDetails,
            MerchantId = _merchant.Id,
            PaymentAmount = _paymentAmount,
            TransactionId = _transaction.Id
        }, new CancellationToken());
        
        // Assert
        Assert.That(result.TransactionId, Is.EqualTo(_transaction.Id));
        Assert.That(result.Status, Is.EqualTo(Status.Success));
        _repository.Verify(_ => _.UpdateTransactionStatus(_transaction.Id, Status.Success), Times.Once);
    }
    
    [Test]
    public async Task HandleFailedPaymentTest()
    {
        // Arrange
        _repository
            .Setup(_ => _.GetMerchantById(_merchant.Id))
            .ReturnsAsync(_merchant);
        _repository
            .Setup(_ => _.CreateTransaction(_merchant, _transaction.CardNumber))
            .ReturnsAsync(_transaction);
        _bankAdapter
            .Setup(_ => _.ProcessPayment(new BankRequest(_transaction.Id, _cardDetails, _paymentAmount)))
            .ReturnsAsync(new BankResponse(_transaction.Id, PaymentStatus.Failure, "Some reason"));

        // Act
        var result = await _handler.Handle(new CreateTransactionRequest
        {
            CardDetails = _cardDetails,
            MerchantId = _merchant.Id,
            PaymentAmount = _paymentAmount,
            TransactionId = _transaction.Id
        }, new CancellationToken());
        
        // Assert
        Assert.That(result.TransactionId, Is.EqualTo(_transaction.Id));
        Assert.That(result.Status, Is.EqualTo(Status.Failed));
        _repository.Verify(_ => _.UpdateTransactionStatus(_transaction.Id, Status.Failed), Times.Once);
    }
}