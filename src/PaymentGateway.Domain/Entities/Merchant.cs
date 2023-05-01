namespace PaymentGateway.Domain.Entities;

/// <summary>
/// Represents a merchant that can receive payments.
/// </summary>
public class Merchant
{
    
    /// <summary>
    /// Unique identifier of the merchant.
    /// </summary>
    public Guid Id { get; set; }
}