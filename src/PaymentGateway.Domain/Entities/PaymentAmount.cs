using System.ComponentModel.DataAnnotations;
using PaymentGateway.Domain.Enums;

namespace PaymentGateway.Domain.Entities;

/// <summary>
/// Represents the amount and currency of a payment.
/// </summary>
public class PaymentAmount
{
    /// <summary>
    /// The payment amount.
    /// </summary>
    [Range(0, double.MaxValue, ErrorMessage = "Price must be non-negative.")]
    public decimal Amount { get; set; }
    
    /// <summary>
    /// The payment currency.
    /// </summary>
    public Currency Currency { get; set; }
}