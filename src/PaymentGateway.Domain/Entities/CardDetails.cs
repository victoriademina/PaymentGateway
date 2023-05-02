using System.ComponentModel.DataAnnotations;

namespace PaymentGateway.Domain.Entities;

/// <summary>
/// Represents the details of a payment card.
/// </summary>
public class CardDetails
{
    /// <summary>
    /// Card number.
    /// </summary>
    /// <remarks>
    /// The card number is a string of digits, whitespace is allowed.
    /// </remarks>
    [Required(ErrorMessage = "CardNumber is required.")]
    [RegularExpression(@"^[0-9\s]+$", ErrorMessage = "The value must be a string containing only digits and whitespace.")]
    public string CardNumber { get; set; }
    
    /// <summary>
    /// Card verification value (CVV).
    /// </summary>
    /// <remarks>
    /// The CVV is a three- or four-digit code printed on the back of the card, used for security purposes.
    /// </remarks>
    [Required(ErrorMessage = "Cvv is required.")]
    [RegularExpression(@"^\d{3,4}$", ErrorMessage = "The value must be a string containing 3 or 4 digits.")]
    public string Cvv { get; set; }
    
    /// <summary>
    /// Expiry month of the card.
    /// </summary>
    /// <remarks>
    /// The expiry month is a number between 1 and 12.
    /// </remarks>
    [Required(ErrorMessage = "ExpiryMonth is required.")]
    [RegularExpression(@"^(0?[1-9]|1[0-2])$", ErrorMessage = "The value must be a string containing a digit from 1 to 12.")]
    public int ExpiryMonth { get; set; }
    
    /// <summary>
    /// Expiry year of the card.
    /// </summary>
    /// <remarks>
    /// The expiry year is a four-digit number representing the year the card expires.
    /// </remarks>
    [Required(ErrorMessage = "ExpiryYear is required.")]
    [RegularExpression(@"^20[2-9][3-9]|[3-9]\d{3}$", ErrorMessage = "The value must be a string containing 4 digits. Year should be equal or greater than 2023.")]
    public int ExpiryYear { get; set; }
    
    /// <summary>
    /// Name of the card owner.
    /// </summary>
    [Required(ErrorMessage = "Owner is required.")]
    public string Owner { get; set; }
}