using Microsoft.EntityFrameworkCore;
using PaymentGateway.Domain.Entities;

namespace PaymentGateway.Infrastructure.Persistence;

/// <summary>
/// Represents the database context for the Payment Gateway using Entity Framework.
/// </summary>
public class PaymentGatewayDbContext : DbContext
{
    /// <summary>
    /// Configures the database options.
    /// </summary>
    protected override void OnConfiguring
        (DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "PaymentGatewayDb");
    }
    
    /// <summary>
    /// Merchant entities in the database.
    /// </summary>
    public DbSet<Merchant> Merchants { get; set; }
    
    /// <summary>
    /// Transaction entities in the database.
    /// </summary>
    public DbSet<Transaction> Transactions { get; set; }
}