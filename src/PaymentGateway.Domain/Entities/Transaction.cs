using System.ComponentModel.DataAnnotations;
using PaymentGateway.Domain.Enums;

namespace PaymentGateway.Domain.Entities;

/// <summary>
/// Represents a payment transaction.
/// </summary>
public class Transaction
{
    /// <summary>
    /// Unique identifier of the transaction.
    /// </summary>
    [Required(ErrorMessage = "Id is required")]
    public Guid Id { get; set; }
    
    /// <summary>
    /// The merchant associated with the transaction.
    /// </summary>
    public Merchant Merchant { get; set; }
    
    /// <summary>
    /// Unique identifier of the merchant associated with the transaction.
    /// </summary>
    [Required(ErrorMessage = "MerchantId is required")]
    public Guid MerchantId { get; set; }
    
    /// <summary>
    /// Card number used for the transaction.
    /// </summary>
    [Required(ErrorMessage = "CardNumber is required")]
    public string CardNumber { get; set; }
    
    /// <summary>
    /// Status of the transaction.
    /// </summary>
    [Required]
    public Status Status { get; set; }
}