using PaymentGateway.Domain.Entities;
using PaymentGateway.Domain.Enums;

namespace PaymentGateway.Application.Common.Repository;

public interface IPaymentGatewayRepository
{
    public Task<Merchant> AddMerchant();
    public Task<Merchant> GetMerchantById(Guid merchantId);
    public Task<Transaction> CreateTransaction(Merchant merchant, string cardNumber);
    public Task<Transaction> GetTransactionById(Guid transactionId);
    public Task<Transaction> UpdateTransactionStatus(Guid transactionId, Status status);
}