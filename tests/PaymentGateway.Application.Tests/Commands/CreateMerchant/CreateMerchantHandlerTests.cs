using Moq;
using PaymentGateway.Application.Commands.CreateMerchant;
using PaymentGateway.Application.Common.Repository;
using PaymentGateway.Domain.Entities;

namespace PaymentGateway.Application.Tests.Commands.CreateMerchant;

public class CreateMerchantHandlerTests
{
    private Mock<IPaymentGatewayRepository> _repository;
    private CreateMerchantHandler _handler;
    private static readonly Guid _merchantId = Guid.NewGuid(); 
    
    [SetUp]
    public void Setup()
    {
        _repository = new Mock<IPaymentGatewayRepository>();
        _handler = new CreateMerchantHandler(_repository.Object);
    }

    [Test]
    public async Task HandleSuccessfulRequest()
    {
        // Arrange
        _repository
            .Setup(_ => _.AddMerchant()).ReturnsAsync(new Merchant{Id = _merchantId});
        
        // Act
        var result = await _handler.Handle(new CreateMerchantRequest(), new CancellationToken());
        
        // Assert
        Assert.That(result.MerchantId, Is.EqualTo(_merchantId));
    }
}