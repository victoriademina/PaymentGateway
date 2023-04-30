using PaymentGateway.Domain.Entities;

namespace PaymentGateway.Application.Common.Interfaces;

public interface IPaymentGatewayRepository
{
    public Task<Merchant> AddMerchant();
    public Task<Transaction> CreateTransaction(Merchant merchant, string cardNumber);
    public Task<Transaction> GetTransactionById(Guid transactionId);
}