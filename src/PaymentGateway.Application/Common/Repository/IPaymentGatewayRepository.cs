using PaymentGateway.Domain.Entities;

namespace PaymentGateway.Application.Common.Repository;

public interface IPaymentGatewayRepository
{
    public Task<Merchant> AddMerchant();
    public Task<Merchant> GetMerchantById(Guid merchantId);
    public Task<Transaction> CreateTransaction(Merchant merchant, string cardNumber);
    public Task<Transaction> GetTransactionById(Guid transactionId);
}