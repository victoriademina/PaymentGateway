using PaymentGateway.Domain.Enums;

namespace PaymentGateway.Domain.Entities;

public class Transaction
{
    public int Id { get; set; }
    public Merchant Merchant { get; set; }
    public string CardNumber { get; set; }
    public Status Status { get; set; }
}