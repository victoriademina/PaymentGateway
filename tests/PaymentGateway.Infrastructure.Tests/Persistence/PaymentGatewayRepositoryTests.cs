using PaymentGateway.Application.Common.Repository;
using PaymentGateway.Domain.Enums;
using PaymentGateway.Infrastructure.Persistence;

namespace PaymentGateway.Infrastructure.Tests.Persistence;

public class PaymentGatewayRepositoryTests
{
    private PaymentGatewayDbContext _dbContext;
    private PaymentGatewayRepository _repository;
    private static readonly string _cardNumber = "1111 1111 1111 1111";
    
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
    public async Task AddMerchantToDatabaseTest()
    {
        // Act
        var merchant = await _repository.AddMerchant();

        // Assert
        Assert.IsNotEmpty(merchant.Id.ToString());
        Assert.That(_dbContext.Merchants.Contains(merchant));
    }
    
    [Test]
    public async Task GetMerchantByIdWithValidMerchantIdTest()
    {
        // Arrange
        var merchant = await _repository.AddMerchant();

        // Act
        var result = await _repository.GetMerchantById(merchant.Id);

        // Assert
        Assert.That(result, Is.EqualTo(merchant));
    }
    
    [Test]
    public async Task GetMerchantByIdWithInvalidMerchantIdTest()
    {
        // Arrange
        await _repository.AddMerchant();

        // Assert
        Assert.ThrowsAsync<MerchantNotFoundException>(async () => await _repository.GetMerchantById(Guid.Empty));
    }

    [Test]
    public async Task CreateTransactionTest()
    {
        // Arrange
        var merchant = await _repository.AddMerchant();

        // Act
        var transaction = await _repository.CreateTransaction(merchant, _cardNumber);
        
        // Assert
        Assert.That(_dbContext.Transactions.Contains(transaction));
        Assert.That(transaction.Merchant, Is.EqualTo(merchant));
        Assert.That(transaction.Status, Is.EqualTo(Status.Pending));
        Assert.That(transaction.CardNumber, Is.EqualTo(_cardNumber));
    }

    [Test]
    public async Task GetTransactionByIdWithValidTransactionIdTest()
    {
        // Arrange
        var merchant = await _repository.AddMerchant();
        var transaction = await _repository.CreateTransaction(merchant, _cardNumber);

        // Act
        var result = await _repository.GetTransactionById(transaction.Id);

        // Assert
        Assert.IsNotNull(result);
        Assert.That(result.Merchant, Is.EqualTo(merchant));
        Assert.That(result.CardNumber, Is.EqualTo(_cardNumber));
    }

    [Test]
    public async Task GetTransactionByIdWithInvalidTransactionIdTest()
    {
        // Arrange
        var merchant = await _repository.AddMerchant();
        await _repository.CreateTransaction(merchant, _cardNumber);
        
        // Assert
        Assert.ThrowsAsync<TransactionNotFoundException>(async () => await _repository.GetTransactionById(Guid.Empty));
    }
}