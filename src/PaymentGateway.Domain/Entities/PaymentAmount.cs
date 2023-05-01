namespace PaymentGateway.Domain.Entities;

/// <summary>
/// Represents the amount and currency of a payment.
/// </summary>
public class PaymentAmount
{
    /// <summary>
    /// The payment amount.
    /// </summary>
    public decimal Amount { get; set; }
    
    /// <summary>
    /// The payment currency.
    /// </summary>
    public string Currency { get; set; }
}