using PaymentGateway.Infrastructure.Persistence;

namespace PaymentGateway.Infrastructure.Tests.Persistence;

public class PaymentGatewayRepositoryTests
{
    private PaymentGatewayDbContext _dbContext;
    private PaymentGatewayRepository _repository;
    
    [SetUp]
    public void Setup()
    {
        _dbContext = new PaymentGatewayDbContext();
        _repository = new PaymentGatewayRepository(_dbContext);
    }
    
    [TearDown]
    public void TearDown()
    {
        _dbContext.Dispose();
    }

    [Test]
    public async Task AddMerchantToDatabase()
    {
        // Act
        var merchant = await _repository.AddMerchant();

        // Assert
        Assert.IsNotEmpty(merchant.Id.ToString());
        Assert.That(_dbContext.Merchants.Contains(merchant));
    }
}