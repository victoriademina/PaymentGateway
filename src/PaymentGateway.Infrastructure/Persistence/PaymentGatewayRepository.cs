using PaymentGateway.Application.Common.Repository;
using PaymentGateway.Domain.Entities;
using PaymentGateway.Domain.Enums;

namespace PaymentGateway.Infrastructure.Persistence;

public class PaymentGatewayRepository : IPaymentGatewayRepository
{
    private readonly PaymentGatewayDbContext _context;

    public PaymentGatewayRepository(PaymentGatewayDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Adds a new Merchant entity to the database.
    /// </summary>
    /// <returns>The new Merchant entity that was added to the database.</returns>
    public async Task<Merchant> AddMerchant()
    {
        var merchant = new Merchant { Id = Guid.NewGuid() };
        await _context.Merchants.AddAsync(merchant);
        await _context.SaveChangesAsync();
        return merchant;
    }
    
    /// <summary>
    /// Retrieves a Merchant entity from the database using the specified merchantId.
    /// </summary>
    /// <param name="merchantId">ID of the merchant to retrieve.</param>
    /// <returns>The Merchant entity with the specified merchantId.</returns>
    /// <exception cref="MerchantNotFoundException">Thrown if a Merchant entity with the specified merchantId is not found.</exception>
    public async Task<Merchant> GetMerchantById(Guid merchantId)
    {
        var merchant = await _context.Merchants.FindAsync(merchantId);
        if (merchant == null)
        {
            throw new MerchantNotFoundException($"Merchant with ID {merchantId} is not found.");
        }
        return merchant;
    }

    /// <summary>
    /// Creates a new Transaction entity in the database with the specified Merchant entity and cardNumber.
    /// </summary>
    /// <param name="merchant">The Merchant entity associated with the new Transaction entity.</param>
    /// <param name="cardNumber">The card number associated with the new Transaction entity.</param>
    /// <returns>The new Transaction entity that was created in the database.</returns>
    public async Task<Transaction> CreateTransaction(Merchant merchant, string cardNumber)
    {
        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            Merchant = merchant,
            MerchantId = merchant.Id,
            CardNumber = cardNumber,
            Status = Status.Pending
        };

        await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();
        return transaction;
    }

    /// <summary>
    /// Retrieves a Transaction entity from the database with the specified transactionId.
    /// </summary>
    /// <param name="transactionId">ID of the Transaction entity to retrieve.</param>
    /// <returns>The Transaction entity with the specified transactionId.</returns>
    /// <exception cref="TransactionNotFoundException">Thrown if a Transaction entity with the specified transactionId is not found.</exception>
    public async Task<Transaction> GetTransactionById(Guid transactionId)
    {
        var transaction = await _context.Transactions.FindAsync(transactionId);
        if (transaction == null)
        {
            throw new TransactionNotFoundException($"Transaction with ID {transactionId} is not found.");
        }
        return transaction;
    }
    
    /// <summary>
    /// Updates the Status field of the Transaction entity with the specified transactionId in the database.
    /// </summary>
    /// <param name="transactionId">ID of the Transaction entity to update.</param>
    /// <param name="status">The new Status to set on the Transaction entity.</param>
    /// <returns>The updated Transaction entity.</returns>
    public async Task<Transaction> UpdateTransactionStatus(Guid transactionId, Status status)
    {
        var transaction = await GetTransactionById(transactionId);
        transaction.Status = status;
        _context.Transactions.Update(transaction);
        await _context.SaveChangesAsync();
        return transaction;
    }
}