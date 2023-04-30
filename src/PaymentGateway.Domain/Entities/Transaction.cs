using PaymentGateway.Domain.Enums;

namespace PaymentGateway.Domain.Entities;

public class Transaction
{
    public Guid Id { get; set; }
    public Merchant Merchant { get; set; }
    public Guid MerchantId { get; set; }
    public string CardNumber { get; set; }
    public Status Status { get; set; }
}