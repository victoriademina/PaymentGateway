using MediatR;
using Moq;
using PaymentGateway.Api.Controllers;
using PaymentGateway.Application.Commands.CreateMerchant;

namespace PaymentGateway.Api.Tests.Controllers;

public class MerchantsControllerTests
{
    private MerchantsController _merchantsController;
    private Mock<IMediator> _mediator;

    [SetUp]
    public void Setup()
    {
        _mediator = new Mock<IMediator>();
        _merchantsController = new MerchantsController(_mediator.Object);
    }

    [Test]
    public async Task CreateMerchantTest()
    {
        //Arrange
        var merchantId = Guid.NewGuid();
        _mediator
            .Setup(_ => _.Send(It.IsAny<CreateMerchantRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new CreateMerchantResponse
            {
                MerchantId = merchantId
            });

        // Act
        var result = await _merchantsController.CreateMerchant();

        //Assert
        Assert.That(result.Value.MerchantId, Is.EqualTo(merchantId));
    }
}