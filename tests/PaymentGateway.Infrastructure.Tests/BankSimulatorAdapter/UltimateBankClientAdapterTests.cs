using BankSimulator.Sdk;
using PaymentGateway.Application.Common.Bank;
using PaymentGateway.Domain.Entities;
using PaymentGateway.Domain.Enums;
using PaymentGateway.Infrastructure.BankSimulatorAdapter;

namespace PaymentGateway.Infrastructure.Tests.BankSimulatorAdapter;

public class UltimateBankClientAdapterTests
{
    private UltimateBankClient _bankClient;
    private UltimateBankClientAdapter _bankClientAdapter;

    [SetUp]
    public void Setup()
    {
        _bankClient = new UltimateBankClient();
        _bankClientAdapter = new UltimateBankClientAdapter(_bankClient);
    }

    [Test]
    public async Task SuccessfulPaymentTest()
    {
        // Arrange
        var bankRequest = CreateBankRequest("1111 1111 1111 1111");
        
        // Act
        var response = await _bankClientAdapter.ProcessPayment(bankRequest);
        
        // Assert
        Assert.That(response.Status, Is.EqualTo(PaymentStatus.Success));
    }

    [Test]
    public async Task FailedPaymentTest()
    {
        // Arrange
        var bankRequest = CreateBankRequest("7777 7777 7777 7777");
        
        // Act
        var response = await _bankClientAdapter.ProcessPayment(bankRequest);
        
        // Assert
        Assert.That(response.Status, Is.EqualTo(PaymentStatus.Failure));
    }
    
    [Test]
    public async Task InternalErrorPaymentTest()
    {
        // Arrange
        var bankRequest = CreateBankRequest("0000 0000 0000 0000");
        
        // Act
        var response = await _bankClientAdapter.ProcessPayment(bankRequest);
        
        // Assert
        Assert.That(response.Status, Is.EqualTo(PaymentStatus.InternalError));
    }
    
    private static BankRequest CreateBankRequest(string cardNumber)
    {
        return new BankRequest(
            Guid.NewGuid(),
            new CardDetails
            {
                CardNumber = cardNumber,
                Cvv = "123",
                ExpiryMonth = 3,
                ExpiryYear = 2025,
                Owner = "Harry Potter"
            },
            new PaymentAmount
            {
                Amount = 28,
                Currency = Currency.Usd
            }
        );
    }
}