using Moq;
using PaymentGateway.Application.Common.Repository;
using PaymentGateway.Application.Queries.GetTransaction;
using PaymentGateway.Domain.Entities;
using PaymentGateway.Domain.Enums;

namespace PaymentGateway.Application.Tests.Queries.GetTransaction;

public class GetTransactionHandlerTests
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
    
    private Mock<IPaymentGatewayRepository> _repository;
    private GetTransactionHandler _handler;
    
    [SetUp]
    public void Setup()
    {
        _repository = new Mock<IPaymentGatewayRepository>();
        _handler = new GetTransactionHandler(_repository.Object);
    }
    
    [Test]
    public async Task HandleSuccessfulRequestTest()
    {
        // Arrange
        _repository
            .Setup(_ => _.GetTransactionById(_transaction.Id))
            .ReturnsAsync(_transaction);
        
        // Act
        var result = await _handler.Handle(new GetTransactionRequest
        {
            MerchantId = _merchant.Id,
            TransactionId = _transaction.Id
        }, new CancellationToken());
        
        // Assert
        Assert.That(result.TransactionId, Is.EqualTo(_transaction.Id));
        Assert.That(result.Status, Is.EqualTo(Status.Pending));
        Assert.That(result.MaskedCardNumber, Is.EqualTo("**** **** **** 1111"));
    }
    
    [Test]
    public void AuthFailureTest()
    {
        // Arrange
        _repository
            .Setup(_ => _.GetTransactionById(_transaction.Id))
            .ReturnsAsync(_transaction);

        // Assert
        Assert.ThrowsAsync<GetTransactionAuthException>(async () => await _handler.Handle(new GetTransactionRequest
        {
            MerchantId = Guid.Empty,
            TransactionId = _transaction.Id
        }, new CancellationToken()));
    }
}