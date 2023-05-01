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
        var merchant = new Merchant { Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6") };
        _context.Add(merchant);
        _context.SaveChanges();
        Console.WriteLine($"Merchant ID: {merchant.Id}");
    }

    public async Task<Merchant> AddMerchant()
    {
        var merchant = new Merchant { Id = Guid.NewGuid() };
        await _context.Merchants.AddAsync(merchant);
        await _context.SaveChangesAsync();
        return merchant;
    }
    
    public async Task<Merchant> GetMerchantById(Guid merchantId)
    {
        var merchant = await _context.Merchants.FindAsync(merchantId);
        if (merchant == null)
        {
            throw new MerchantNotFoundException($"Merchant with ID {merchantId} is not found.");
        }

        return merchant;
    }

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

    public async Task<Transaction> GetTransactionById(Guid transactionId)
    {
        var transaction = await _context.Transactions.FindAsync(transactionId);
        if (transaction == null)
        {
            throw new TransactionNotFoundException($"Transaction with ID {transactionId} is not found.");
        }

        return transaction;
    }
    
    public async Task<Transaction> UpdateTransactionStatus(Guid transactionId, Status status)
    {
        var transaction = await GetTransactionById(transactionId);
        transaction.Status = status;
        _context.Transactions.Update(transaction);
        await _context.SaveChangesAsync();

        return transaction;
    }
}