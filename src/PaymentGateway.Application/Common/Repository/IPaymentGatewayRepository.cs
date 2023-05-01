using PaymentGateway.Domain.Entities;
using PaymentGateway.Domain.Enums;

namespace PaymentGateway.Application.Common.Repository;

public interface IPaymentGatewayRepository
{
    /// <summary>
    /// Adds a new Merchant entity to the database.
    /// </summary>
    /// <returns>The new Merchant entity that was added to the database.</returns>
    public Task<Merchant> AddMerchant();
    
    /// <summary>
    /// Retrieves a Merchant entity from the database using the specified merchantId.
    /// </summary>
    /// <param name="merchantId">ID of the merchant to retrieve.</param>
    /// <returns>The Merchant entity with the specified merchantId.</returns>
    /// <exception cref="MerchantNotFoundException">Thrown if a Merchant entity with the specified merchantId is not found.</exception>
    public Task<Merchant> GetMerchantById(Guid merchantId);
    
    /// <summary>
    /// Creates a new Transaction entity in the database with the specified Merchant entity and cardNumber.
    /// </summary>
    /// <param name="merchant">The Merchant entity associated with the new Transaction entity.</param>
    /// <param name="cardNumber">The card number associated with the new Transaction entity.</param>
    /// <returns>The new Transaction entity that was created in the database.</returns>
    public Task<Transaction> CreateTransaction(Merchant merchant, string cardNumber);
    
    /// <summary>
    /// Retrieves a Transaction entity from the database with the specified transactionId.
    /// </summary>
    /// <param name="transactionId">ID of the Transaction entity to retrieve.</param>
    /// <returns>The Transaction entity with the specified transactionId.</returns>
    /// <exception cref="TransactionNotFoundException">Thrown if a Transaction entity with the specified transactionId is not found.</exception>
    public Task<Transaction> GetTransactionById(Guid transactionId);
    
    /// <summary>
    /// Updates the Status field of the Transaction entity with the specified transactionId in the database.
    /// </summary>
    /// <param name="transactionId">ID of the Transaction entity to update.</param>
    /// <param name="status">The new Status to set on the Transaction entity.</param>
    /// <returns>The updated Transaction entity.</returns>
    public Task<Transaction> UpdateTransactionStatus(Guid transactionId, Status status);
}